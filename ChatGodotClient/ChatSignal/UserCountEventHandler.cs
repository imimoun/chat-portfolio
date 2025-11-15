using Godot;
using System;
using Microsoft.AspNetCore.SignalR.Client;

public partial class UserCountEventHandler : Node
{
	private HubConnection _connection;

	[Signal]
	public delegate void UserCountReceivedEventHandler(int count);

	public UserCountEventHandler(HubConnection connection)
	{
		_connection = connection;
		SetupHandler();
	}

	private void SetupHandler()
	{
		_connection.On<int>("ReceiveUserCount", (count) =>
		{
			CallDeferred(MethodName.EmitUserCountReceived, count);
		});
	}

	private void EmitUserCountReceived(int count)
	{
		EmitSignal(SignalName.UserCountReceived, count);
	}
}