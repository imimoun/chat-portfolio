using Godot;
using System;

public partial class ReceivedMessages : ScrollContainer
{
	private NetworkManager _networkManager;
	private VBoxContainer _vBoxContainer;

	private AutoScrollButton _autoScrollButton;

	public override void _Ready()
	{
		_networkManager = GetNode<NetworkManager>("/root/NetworkManager");
		_networkManager.UserCountHandler.UserCountReceived += OnUserCountReceived;
		_networkManager.UserJoinedHandler.UserJoinedReceived += OnUserJoinedReceived;
		_networkManager.UserLeftHandler.UserLeftReceived += OnUserLeftReceived;
		_networkManager.MessageHandler.MessageReceived += OnMessageReceived;

		_vBoxContainer = GetNode<VBoxContainer>("VBoxContainer");

		_autoScrollButton = GetNode<AutoScrollButton>("%AutoScrollButton");
	}

	/// <summary>
	/// Handle user count received signal.
	/// </summary>
	/// <param name="count">The current user count.</param>
	/// <remarks> Subtract 1 to not count self. </remarks>
	private void OnUserCountReceived(int count)
	{
		AddNewLabel($"Users connected: {count - 1}");
	}

	private void OnUserJoinedReceived()
	{
		AddNewLabel("A new user has joined the chat.");
	}

	private void OnUserLeftReceived()
	{
		AddNewLabel("A user has left the chat.");
	}

	private void OnMessageReceived(string user, string message)
	{
		AddNewLabel($"{user}: {message}");
	}

	private void AddNewLabel(string text)
	{
		_vBoxContainer.AddChild(
			new Label{
				Text = text
			}
		);
		_autoScrollButton.UpdateVisibility();
	}

	public bool IsAbleToScrollDown(){
		return (
			GetNode<VScrollBar>("_v_scroll").Value <=
			_vBoxContainer.Size.Y - Size.Y
		);
	}

	public void _ScrollEnded()
	{
		_autoScrollButton.UpdateVisibility();
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
