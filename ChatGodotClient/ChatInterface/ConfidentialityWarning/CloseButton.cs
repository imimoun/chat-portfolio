using Godot;
using System;

public partial class CloseButton : Button
{
	public void _on_pressed()
	{
		GetParent().GetParent().GetParent().GetParent().GetParent().QueueFree();
	}
}
