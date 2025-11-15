using Godot;
using System;

public partial class Main : Control
{
	public override void _Ready()
	{
		PackedScene childPackedScene = ResourceLoader.Load<PackedScene>(
			"res://ChatInterface/ConfidentialityWarning/Main.tscn"
		);

		AddChild(childPackedScene.Instantiate());
	}
}
