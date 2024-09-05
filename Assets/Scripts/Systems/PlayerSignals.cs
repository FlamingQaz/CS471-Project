using Godot;
using System;

namespace CS471TowerDefense.Scripts.GameSystems
{
	public partial class PlayerSignals : Node
	{
		[Signal]
		public delegate void TakeDamageEventHandler(int amount);

	}
}