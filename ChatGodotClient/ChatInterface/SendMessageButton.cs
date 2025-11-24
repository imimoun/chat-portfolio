using Godot;
using System;
using System.Threading.Tasks;

public partial class SendMessageButton : Button
{
	private LineEdit _userNameLineEdit;
	private LineEdit _messageLineEdit;

	private NetworkManager _networkManager;

	public override void _Ready()
	{
		_userNameLineEdit = GetNode<LineEdit>("%UserNameLineEdit");
		_messageLineEdit = GetNode<LineEdit>("%MessageLineEdit");

		_networkManager = GetNode<NetworkManager>("/root/NetworkManager");
	}

	public void _OnPressed()
	{
		string userNameText = _userNameLineEdit.Text;
		string messageText = _messageLineEdit.Text;
		
		Task.Run(async () =>
		{
			try
			{
				await _networkManager.MessageSender.SendChatMessage(
					userNameText,
					messageText
				);
				_messageLineEdit.CallDeferred(
					LineEdit.MethodName.SetText,
					string.Empty
				);
			}
			catch (System.Exception e)
			{
				GD.PrintErr($"Message not sent: {e.Message}");
			}
		});
	}
}
