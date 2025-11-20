using Godot;
using System;

public partial class ReceivedMessages : ScrollContainer
{
	private NetworkManager _networkManager;
	private VBoxContainer _vBoxContainer;

	public override void _Ready()
	{
		_networkManager = GetNode<NetworkManager>("/root/NetworkManager");
		_networkManager.UserCountHandler.UserCountReceived += OnUserCountReceived;
		_networkManager.UserJoinedHandler.UserJoinedReceived += OnUserJoinedReceived;
		_networkManager.UserLeftHandler.UserLeftReceived += OnUserLeftReceived;
		_networkManager.MessageHandler.MessageReceived += OnMessageReceived;

		_vBoxContainer = GetNode<VBoxContainer>("VBoxContainer");
	}

	/// <summary>
	/// Handle user count received signal.
	/// </summary>
	/// <param name="count">The current user count.</param>
	/// <remarks> Subtract 1 to not count self. </remarks>
	private void OnUserCountReceived(int count)
	{
		_vBoxContainer.AddChild(
			new Label{
				Text = $"Users connected: {count - 1}"
			}
		);
	}

	private void OnUserJoinedReceived()
	{
		_vBoxContainer.AddChild(
			new Label{
				Text = "A new user has joined the chat."
			}
		);
	}

	private void OnUserLeftReceived()
	{
		_vBoxContainer.AddChild(
			new Label{
				Text = "A user has left the chat."
			}
		);
	}

	private void OnMessageReceived(string user, string message)
	{
		_vBoxContainer.AddChild(
			new Label{
				Text = $"{user}: {message}"
			}
		);
	}

	public override void _ExitTree()
	{
		if (_networkManager?.MessageHandler != null)
		{
			_networkManager.MessageHandler.MessageReceived -= OnMessageReceived;
		}
		if (_networkManager?.UserLeftHandler != null)
		{
			_networkManager.UserLeftHandler.UserLeftReceived -= OnUserLeftReceived;
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
