using Godot;
using System.Collections;
using System.Collections.Generic;
using CS471TowerDefense.Scripts;
using CS471TowerDefense.Scripts.Enemies;
using System.Runtime.CompilerServices;
using System.Runtime;
using System.Reflection.Emit;
using System.Security.AccessControl;

namespace CS471TowerDefense.Scripts.GameSystems
{
	public partial class Player : Node
	{
		private PlayerSignals _playerSignals;

		[Signal]
		public delegate void UpdateHealthDisplayEventHandler(int health);


		private int _health { get; set; }


		readonly public PackedScene gameOverScreenPath = 
			ResourceLoader.Load<PackedScene>("res://Assets/Prefab Objects/Map Systems/game_over_screen.tscn");

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_health = 1000;

			EmitSignal(SignalName.UpdateHealthDisplay, _health);

			_playerSignals = GetNode<PlayerSignals>("/root/PlayerSignals");
			_playerSignals.TakeDamage += HandleTakeDamage;

		}

		public override void _Notification(int what)
		{
			if (what == NotificationPredelete)
			{
				_playerSignals.TakeDamage -= HandleTakeDamage;
			}
		}

		private void HandleTakeDamage(int amount)
		{
			_health -= amount;
			GD.Print($"Damaged for: {amount}, Health now at: {_health}");
			EmitSignal(SignalName.UpdateHealthDisplay, _health);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (_health <= 0)
			{
				gameOver();
			}
		}

		public void gameOver()
		{
			AddChild(gameOverScreenPath.Instantiate());
			GetTree().Paused = true;
		}
	}
}
