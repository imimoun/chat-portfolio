using Godot;
using System;

public partial class ReceivedMessagesItemList : ItemList
{
	private NetworkManager _networkManager;

	public override void _Ready()
	{
		_networkManager = GetNode<NetworkManager>("/root/NetworkManager");
		_networkManager.UserCountHandler.UserCountReceived += OnUserCountReceived;
		_networkManager.UserJoinedHandler.UserJoinedReceived += OnUserJoinedReceived;
		_networkManager.UserLeftHandler.UserLeftReceived += OnUserLeftReceived;
		_networkManager.MessageHandler.MessageReceived += OnMessageReceived;
	}

	/// <summary>
	/// Handle user count received signal.
	/// </summary>
	/// <param name="count">The current user count.</param>
	/// <remarks> Subtract 1 to not count self. </remarks>
	private void OnUserCountReceived(int count)
	{
		AddItem($"Users connected: {count - 1}");
		EnsureCurrentIsVisible();
	}

	private void OnUserJoinedReceived()
	{
		AddItem("A new user has joined the chat.");
		EnsureCurrentIsVisible();
	}

	private void OnUserLeftReceived()
	{
		AddItem("A user has left the chat.");
		EnsureCurrentIsVisible();
	}

	private void OnMessageReceived(string user, string message)
	{
		AddItem($"{user}: {message}");
		EnsureCurrentIsVisible();
	}

	public override void _ExitTree()
	{
		if (_networkManager?.MessageHandler != null)
		{
			_networkManager.MessageHandler.MessageReceived -= OnMessageReceived;
		}
		if (_networkManager?.UserJoinedHandler != null)
		{
			_networkManager.UserJoinedHandler.UserJoinedReceived -= OnUserJoinedReceived;
		}
		if (_networkManager?.UserCountHandler != null)
		{
			_networkManager.UserCountHandler.UserCountReceived -= OnUserCountReceived;
		}
	}
}
