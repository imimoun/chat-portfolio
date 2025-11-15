using Godot;
using System;
using Microsoft.AspNetCore.SignalR.Client;

public partial class UserJoinedEventHandler : Node
{
	private HubConnection _connection;

	[Signal]
	public delegate void UserJoinedReceivedEventHandler();

	public UserJoinedEventHandler(HubConnection connection)
	{
		_connection = connection;
		SetupHandler();
	}

	private void SetupHandler()
	{
		_connection.On("UserJoined", () =>
		{
			CallDeferred(MethodName.EmitUserJoinedReceived);
		});
	}
	
	private void EmitUserJoinedReceived()
	{
		EmitSignal(SignalName.UserJoinedReceived);
	}
}
