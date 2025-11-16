using ChatSignalServer.Hubs;
using Moq;
using Microsoft.AspNetCore.SignalR;
using Xunit;
using System.Threading.Tasks;
using System.Net;
using System;

namespace ChatSignalServer.Tests.Hubs
{
    public class ChatHubTests
    {
        private static string GetSerializedInput(
            string input,
            int maxLength = 30
        )
        {
            var chatHubType = typeof(ChatHub);
            var methodInfo = chatHubType.GetMethod(
                "GetSerializedInput",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Static
            );

            var parameters = new object[] { input, maxLength };
            return (string)methodInfo!.Invoke(null, parameters)!;
        }

        [Theory]
        [InlineData("Hello World", "Hello World", 30)] 
        [InlineData("Testing 123", "Testing 123", 15)] 
        public void GetSerializedInput_ShouldReturnInput_WhenShorterThanMaxLength(
            string input,
            string expected,
            int maxLength
        )
        {
            string actual = GetSerializedInput(input, maxLength);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("This is a very long message that should be truncated.", "This is a very long message th", 30)]
        [InlineData("Another long test string.", "Another long", 12)]
        public void GetSerializedInput_ShouldTruncateInput_WhenLongerThanMaxLength(
            string input,
            string expected,
            int maxLength
        )
        {
            string actual = GetSerializedInput(input, maxLength);

            Assert.Equal(expected, actual);
            Assert.Equal(maxLength, actual.Length);
        }

        [Theory]
        [InlineData("<b>Injection</b>", "&lt;b&gt;Injection&lt;/b&gt;")]
        [InlineData("<script>alert('xss')</script>", "&lt;script&gt;alert(&#39;xss&#")]
        public void GetSerializedInput_ShouldHtmlEncodeInput(
            string input,
            string expected
        )
        {
            string actual = GetSerializedInput(input, 30); 

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task SendMessage_ShouldCallClientsAllSendAsyncWithSerializedInputs()
        {
            string testUser = "User<ID>";
            string testMessage = "Hello World!";

            string expectedSerializedUser = "User&lt;ID&gt;";
            string expectedSerializedMessage = "Hello World!";

            var mockClientProxy = new Mock<IClientProxy>();
            
            var mockClients = new Mock<IHubCallerClients>();
            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);

            var chatHub = new ChatHub
            {
                Clients = mockClients.Object
            };

            await chatHub.SendMessage(testUser, testMessage);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "ReceiveMessage",
                    It.Is<object[]>(args =>
                        (string)args[0] == expectedSerializedUser &&
                        (string)args[1] == expectedSerializedMessage
                    ),
                    default
                ),
                Times.Once
            );
        }
    }
}