using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.Models;
using WebAPIs.Models.Requests;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IMessage _IMessage;
        private readonly IServiceMessage _IServiceMessage;

        public MessagesController(IMapper IMapper, IMessage IMessage, IServiceMessage IServiceMessage)
        {
            _IMapper = IMapper;
            _IMessage = IMessage;
            _IServiceMessage = IServiceMessage;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost]
        public async Task<List<Notifies>> Add(AddMessageRequest message)
        {
            var messageMap = _IMapper.Map<Message>(message);
            messageMap.UserId = await ReturnLoggedUserId();

            await _IServiceMessage.Add(messageMap);

            return messageMap.Notifications;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPut]
        public async Task<List<Notifies>> Update(UpdateMessageRequest message)
        {
            var messageMap = _IMapper.Map<Message>(message);
            messageMap.UserId = await ReturnLoggedUserId();

            await _IServiceMessage.Update(messageMap);

            return messageMap.Notifications;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpDelete]
        public async Task<List<Notifies>> Delete(DeleteMessageRequest message)
        {
            var messageMap = _IMapper.Map<Message>(message);
            messageMap.UserId = await ReturnLoggedUserId();

            await _IMessage.Delete(messageMap);

            return messageMap.Notifications;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet]
        public async Task<List<MessageViewModel>> Get([FromQuery] bool? isActive = null)
        {
            var messages = await _IMessage.List();

            if (isActive != null)
            {
                var filteredMessages = isActive.Value ? messages.Where(m => m.Active) : messages.Where(m => !m.Active);
                messages = filteredMessages.ToList();
            }

            var messageMap = _IMapper.Map<List<MessageViewModel>>(messages);

            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<MessageViewModel> GetById(int id)
        {
            var message = await _IMessage.GetEntityById(id);
            var messageMap = _IMapper.Map<MessageViewModel>(message);

            return messageMap;
        }

        private async Task<string> ReturnLoggedUserId()
        {
            if (User != null)
            {
                var idUsuario = User.FindFirst("idUser");
                return idUsuario.Value;
            }

            return string.Empty;
        }
    }
}