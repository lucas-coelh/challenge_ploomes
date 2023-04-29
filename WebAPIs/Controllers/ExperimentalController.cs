using Microsoft.AspNetCore.Mvc;
using WebAPIs.Utils.ExternalAPI;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentalController : ControllerBase
    {

        [Produces("application/json")]
        [HttpGet("get")]
        public async Task<IActionResult> GetCnpj([FromQuery] string cnpj)
        {
            var validCnpj = CnpjHelper.ValidCnpj(cnpj);

            if (!validCnpj.valido)
            {
                return BadRequest($"O Cnpj: {validCnpj.cnpj} é invalido! " +
                    $"\nConfira a quantidade de caracteres e se o CNPJ foi digitado corretamente.");
            }
            var result = await CnpjHelper.GetCnpj(validCnpj.cnpj);

            return Ok(result);
        }
    }
}
