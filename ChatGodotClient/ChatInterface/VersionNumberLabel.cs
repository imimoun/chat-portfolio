using Godot;
using System;

public partial class VersionNumberLabel : RichTextLabel
{

	public override void _Ready()
	{
		Text = String.Format(
			"[i]v{0}[/i]",
			(
				ProjectSettings
				.GetSetting("application/config/version")
				.AsString()
			)
		);
	}
}
