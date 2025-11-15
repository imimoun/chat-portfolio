using Godot;
using System;
using Microsoft.AspNetCore.SignalR.Client;

public partial class UserLeftEventHandler : Node
{
	private HubConnection _connection;

	[Signal]
	public delegate void UserLeftReceivedEventHandler();

	public UserLeftEventHandler(HubConnection connection)
	{
		_connection = connection;
		SetupHandler();
	}

	private void SetupHandler()
	{
		_connection.On("UserLeft", () =>
		{
			CallDeferred(MethodName.EmitUserLeftReceived);
		});
	}
	
	private void EmitUserLeftReceived()
	{
		EmitSignal(SignalName.UserLeftReceived);
	}
}
