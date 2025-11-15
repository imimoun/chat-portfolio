using Microsoft.AspNetCore.SignalR;
using System.Net;
using System.Threading.Tasks;

namespace ChatSignalServer.Hubs
{
	public class ChatHub : Hub
	{
		private static int _connectedUsers = 0;

		public override async Task OnConnectedAsync()
		{
			var httpContext = Context.GetHttpContext();
			string clientKey = httpContext?.Request.Headers["X-Godot-Client-Key"].ToString() ?? string.Empty;

			// In production, the environment variable are in the Docker container settings
			if (clientKey != Environment.GetEnvironmentVariable("X_GODOT_CLIENT_KEY"))
			{
				Context.Abort();
				return;
			}

			int currentCount = Interlocked.Increment(ref _connectedUsers);

			await Clients.Caller.SendAsync("ReceiveUserCount", currentCount);

			await Clients.Others.SendAsync("UserJoined");

			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await Clients.Others.SendAsync("UserLeft");

			Interlocked.Decrement(ref _connectedUsers);

			await base.OnDisconnectedAsync(exception);
		}

		/// <summary>
		/// Serialize string input.
		/// </summary>
		/// <param name="input">The string input.</param>
		/// <param name="maxLength">The maximum length of the string.</param>
		/// <returns>The serialized string.</returns>
		private static string GetSerializedInput(
			string input,
			int maxLength = 30
		)
		{
			string safeInput = WebUtility.HtmlEncode(input);

			if (safeInput.Length <= maxLength)
			{
				return safeInput;
			}

			return safeInput.Substring(0, maxLength);
		}

		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync(
				"ReceiveMessage",
				GetSerializedInput(user),
				GetSerializedInput(message)
			);
		}
	}
}
