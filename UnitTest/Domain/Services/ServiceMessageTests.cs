using Domain.Interfaces;
using Domain.Services;
using Entities.Entities;
using Moq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UnitTest.Domain.Services
{
    public class ServiceMessageTests
    {
        private Mock<IMessage> _IMessage;
        private ServiceMessage _serviceMessage;

        public ServiceMessageTests()
        {
            _IMessage = new Mock<IMessage>();
            _serviceMessage = new ServiceMessage(_IMessage.Object);
        }

        [Fact(DisplayName = "Should add message when called")]
        public async Task ShouldAddMessageWhenCalled()
        {
            //Arrange
            var message = new Message
            {
                Title = "Testando tudo",
            };

            // Act
            await _serviceMessage.Add(message);

            // Assert
            _IMessage.Verify(m => m.Add(message), Times.Once);
        }

        [Fact(DisplayName = "Shouldn't add message when called")]
        public async Task ShouldNotAddValueWhenCalled()
        {
            //Arrange
            var message = new Message
            {
                Title = "",
            };

            // Act
            await _serviceMessage.Add(message);

            // Assert
            _IMessage.Verify(m => m.Add(message), Times.Never);
        }

        [Fact(DisplayName = "Should add messages and update fields when called")]
        public async void ShouldAddMessageAndUpdateFieldsWhenCalled()
        {
            //Arrange
            var validMessage = new Message { Title = "Valid Title" };

            // Act
            await _serviceMessage.Add(validMessage);

            // Assert
            Assert.NotNull(validMessage.CreatedDate);
            Assert.NotNull(validMessage.ModifiedDate);
            Assert.True(validMessage.Active);
        }

        [Fact(DisplayName = "Should update message when called")]
        public async Task ShouldUpdateMessageWhenCalled()
        {
            //Arrange
            var message = new Message
            {
                Title = "Testando tudo",
            };

            // Act
            await _serviceMessage.Update(message);

            // Assert
            _IMessage.Verify(m => m.Update(message), Times.Once);
        }

        [Fact(DisplayName = "Shouldnt update message when called")]
        public async Task ShouldNotUpdateValueWhenCalled()
        {
            //Arrange
            var message = new Message
            {
                Title = "",
            };

            // Act
            await _serviceMessage.Update(message);

            // Assert
            _IMessage.Verify(m => m.Update(message), Times.Never);
        }

        [Fact(DisplayName = "Should return active messages when called")]
        public async Task ShouldReturnActiveMessages()
        {
            // Arrange
            var messages = new List<Message>
                {
                 new Message { Id = 1, Title = "Message 1", Active = true },
                 new Message { Id = 2, Title = "Message 2", Active = true },
                 new Message { Id = 2, Title = "Message 2", Active = false }
                };

            _IMessage.Setup(m => m.GetMessagesList(It.IsAny<Expression<Func<Message, bool>>>()))
             .ReturnsAsync((Expression<Func<Message, bool>> predicate) => messages.Where(predicate.Compile()).ToList());

            // Act
            var result = await _serviceMessage.GetActiveMessagesList(true);

            // Assert
            var expectedMessages = new List<Message>
                {
                 new Message { Id = 1, Title = "Message 1", Active = true },
                 new Message { Id = 2, Title = "Message 2", Active = true }
                };
            Assert.Equal(expectedMessages.Count(), result.Count());
        }

        [Fact(DisplayName = "shouldnt return inactive messages when called")]
        public async Task ShouldReturnInactiveMessages()
        {
            // Arrange
            var messages = new List<Message>
                {
                 new Message { Id = 1, Title = "Message 1", Active = true },
                 new Message { Id = 2, Title = "Message 2", Active = true },
                 new Message { Id = 3, Title = "Message 2", Active = false }
                };

            _IMessage.Setup(m => m.GetMessagesList(It.IsAny<Expression<Func<Message, bool>>>()))
             .ReturnsAsync((Expression<Func<Message, bool>> predicate) => messages.Where(predicate.Compile()).ToList());

            // Act
            var result = await _serviceMessage.GetActiveMessagesList(false);

            // Assert
            var expectedMessages = new List<Message>
                {
                 new Message { Id = 3, Title = "Message 2", Active = false }
                };
            Assert.Equal(expectedMessages.Count(), result.Count());
        }
    }
}