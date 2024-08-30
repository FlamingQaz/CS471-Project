using Godot;
using System;
using CS471TowerDefense.Scripts;
using CS471TowerDefense.Scripts.GameSystems;

namespace CS471TowerDefense.Scripts.Enemies
{
	public partial class EnemyScript : Node
	{
		private GameSystems.PlayerSignals _playerSignals;
		
		private Label _healthLabel;
		private PathFollow2D _pathFollow2d;

		
		//Base Enemy Stats
		[ExportGroup("Enemy Stats")]
		[Export]
		private int _baseHealth;
		[Export]
		private float _baseMoveSpeed;
		[Export]
		private int _baseGoldReward;
		[Export]
		private int _baseDamage;


		[ExportGroup("DEBUG")]
		[Export]
		//a die on command button basically, to test that out
		private bool kill = false;
		[Export]
		private int _currHealth;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			kill = false;
			_currHealth = _baseHealth;
			_healthLabel = (Label) FindChild("HealthLabel");
			_healthLabel.Text = "" + _currHealth + "/" + _baseHealth;

			_pathFollow2d = (PathFollow2D)GetParent().GetParent().GetParent();

			_playerSignals = GetNode<GameSystems.PlayerSignals>("/root/PlayerSignals");
			//GameSystems.Player player = (GameSystems.Player) GetNode("/root/Player");
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			_pathFollow2d.Progress += (float) (_baseMoveSpeed * delta);

			if (_pathFollow2d.ProgressRatio == 1)
			{
				_playerSignals.EmitSignal(nameof(GameSystems.PlayerSignals.TakeDamage), _baseDamage);
				_pathFollow2d.GetParent().QueueFree();
			}

			if (kill)
				_Die();
		}
		
		//This is a custom signal that emmits when the entity reaches 0 Health
		[Signal]
		public delegate void HealthDepletedEventHandler();
		
		//This function allows for the entity to take damage
		//Its set as public as the attack function of the tower would be calling this
		public void TakeDamage(int amount)
		{
			_currHealth -= amount;
			_healthLabel.Text = "" + _currHealth + "/" + _baseHealth;

			if (_currHealth <= 0)
			{
				_Die();
			}
		}
		
		private void _Die()
		{
			kill = false;
			//emits the signal described above
			EmitSignal(SignalName.HealthDepleted);
			GD.Print(this.Name + " dies ");

			//This gets the Path node the enemy is attached too
			//TODO: Find something more elegant ig lol
			GetParent().GetParent().GetParent().GetParent().QueueFree();
		}
		
	}
}
