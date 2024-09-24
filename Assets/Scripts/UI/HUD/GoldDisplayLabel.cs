using Godot;
using System;

public partial class GoldDisplayLabel : Label
{
	private void _on_currency_manager_money_updated(Variant current_money)
	{
		// Replace with function body
		Text = $"Gold: {current_money}";
	}
}
