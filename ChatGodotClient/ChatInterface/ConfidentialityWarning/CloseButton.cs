using Godot;
using System;

public partial class CloseButton : Button
{
	public void _OnPressed()
	{
		GetParent().GetParent().GetParent().GetParent().GetParent().QueueFree();
	}
}
