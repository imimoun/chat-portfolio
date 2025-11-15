using Godot;
using System;
using Microsoft.AspNetCore.SignalR.Client;

public partial class MessageEventHandler : Node
{
	private HubConnection _connection;

	[Signal]
	public delegate void MessageReceivedEventHandler(string user, string message);

	public MessageEventHandler(HubConnection connection)
	{
		_connection = connection;
		SetupHandler();
	}

	private void SetupHandler()
	{
		_connection.On<string, string>("ReceiveMessage", (user, message) =>
		{
			CallDeferred(MethodName.EmitMessageReceived, user, message);
		});
	}
	
	private void EmitMessageReceived(string user, string message)
	{
		EmitSignal(SignalName.MessageReceived, user, message);
	}
}
