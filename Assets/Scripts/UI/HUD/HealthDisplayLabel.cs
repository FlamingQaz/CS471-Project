using Godot;
using System;

public partial class HealthDisplayLabel : Label
{
	private void _on_player_update_health_display(int health)
	{
		// Replace with function body.
		Text = $"Health: {health}";
	}
}



