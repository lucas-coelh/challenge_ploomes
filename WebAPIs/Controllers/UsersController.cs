using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPIs.Models;
using WebAPIs.Token;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("login/")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
            {
                return Unauthorized("Email ou senha não informados");
            }

            var resultado = await
                _signInManager.PasswordSignInAsync(login.Email, login.Senha, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var userCurrent = await _userManager.FindByEmailAsync(login.Email);
                var idUser = userCurrent.Id;

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("ApprovalSecretKey-2023"))
                    .AddSubject("Ploomes Challenge")
                    .AddIssuer("Junior Software Developer at Ploomes (Amém kkk)")
                    .AddAudience("Ploomes")
                    .AddClaim("idUser", idUser)
                    .AddExpiry(8)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost]
        public async Task<IActionResult> AddIdentityUser([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Ok("Falta alguns dados");

            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                CPF = login.Cpf,
                Role = UserRole.Common
            };

            var resultUser = await _userManager.CreateAsync(user, login.Senha);

            if (resultUser.Errors.Any())
            {
                return Ok(resultUser.Errors);
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultEmail = await _userManager.ConfirmEmailAsync(user, code);

            if (resultEmail.Succeeded)
                return Ok("Usuário adicionado com Sucesso!");
            else
                return Ok("Erro ao confirmar usuários");
        }
    }
}