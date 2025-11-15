using Godot;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;

public partial class MessageSender : Node
{
	private readonly HubConnection _connection;

	public MessageSender(HubConnection connection)
	{
		_connection = connection;
	}

	public async Task SendChatMessage(string user, string message)
	{
		try
		{
			await _connection.InvokeAsync("SendMessage", user, message);
		}
		catch (System.Exception e)
		{
			GD.PrintErr($"Failed to invoke SendMessage: {e.Message}");
		}
	}
}
