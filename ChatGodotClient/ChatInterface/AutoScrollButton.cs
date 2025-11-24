using Godot;
using System;

public partial class AutoScrollButton : Button
{
	private ReceivedMessages _receivedMessagesScrollContainer;

	public override void _Ready()
	{
		_receivedMessagesScrollContainer = GetNode<
			ReceivedMessages
		>(
			"%ReceivedMessages"
		);
	}

	public void UpdateVisibility()
	{
		Visible = (
			_receivedMessagesScrollContainer
			.IsAbleToScrollDown()
		);
	}
	
	private void _OnPressed()
	{
		_receivedMessagesScrollContainer.GetNode<VScrollBar>("_v_scroll").Value = (
			_receivedMessagesScrollContainer.GetNode<VBoxContainer>("VBoxContainer").Size.Y - Size.Y
		);
		Visible = false;
	}
}
