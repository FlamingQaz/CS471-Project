using Godot;
using System.Collections;
using System.Collections.Generic;
using CS471TowerDefense.Scripts;
using CS471TowerDefense.Scripts.Enemies;
using System.Runtime.CompilerServices;
using System.Runtime;

namespace CS471TowerDefense.Scripts.Towers
{
	public partial class TowerScript : Node2D
	{

		//Base Tower Stats
		[ExportGroup("Tower Stats")]
		[Export]
		private int _baseDamage;
		[Export]
		//This is in px for some ungodly reason
		private float _baseRange;
		[Export]
		private float _baseAttackSpeed;
		
		//Child References
		private Timer _attackTimer;
		private Area2D _enemyDetector;
		private CircleShape2D _detectionArea;
		
		//List to keep track of targets
		private List<Enemies.EnemyScript> _targets = new List<Enemies.EnemyScript>();
		
		private bool _canAttack = true;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_enemyDetector = (Area2D) GetChild(0);
			CollisionShape2D shapeHolder = (CollisionShape2D) _enemyDetector.GetChild(0);
			_detectionArea = (CircleShape2D) shapeHolder.Shape;
			_attackTimer = (Timer) GetChild(1);
			_canAttack = true;

			_detectionArea.Radius = _baseRange;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			//I have it set this way so it can attack as soon as something
			//enters its range of attack and then have the attack cooldown
			//start
			if (_targets.Count != 0 && _canAttack) {
				_Attack(_targets[0]);
			}
		}
		
		//This is a signal that triggers whenever something enters
		//the attached Area2D node
		//Adds enemies to the _target list when they are in range
		private void _on_area_2d_body_entered(Node body)
		{
			Enemies.EnemyScript potentialEnemy = (Enemies.EnemyScript)body.GetChild(0).GetChild(0);

			if (!_targets.Contains(potentialEnemy))
			{
				_targets.Add(potentialEnemy);
			}
		}
		
		//This is a signal that triggers whenever something exits
		//the attached Area2D node
		//Removes enemies to the _target list when they leave the range
		//of the tower
		private void _on_area_2d_body_exited(Node body)
		{
			Enemies.EnemyScript potentialEnemy = (Enemies.EnemyScript) body.GetChild(0).GetChild(0);

			if (_targets.Contains(potentialEnemy)) 
				_targets.Remove( potentialEnemy);
		}
		
		//When the attached timer reaches 0
		//This is for simulating the attack speed, basically
		//when it reaches 0 the attack cooldown is over
		private void _on_timer_timeout()
		{
			_canAttack = true;
		}
		
		//function to attack enemy - simply calls the enemy's TakeDamage function
		//Might change in the future for more complicated attack situations
		private void _Attack(Enemies.EnemyScript target)
		{
			GD.Print(this.Name + " attacks " + target.Name);
			if (target.HasMethod("TakeDamage"))
				target.TakeDamage(_baseDamage);
			_attackTimer.Start(_baseAttackSpeed);
			_canAttack = false;
		}
	}
}
