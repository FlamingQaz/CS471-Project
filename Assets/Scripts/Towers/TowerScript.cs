using Godot;
using System.Collections;
using System.Collections.Generic;
using CS471TowerDefense.Scripts;

namespace CS471TowerDefense.Scripts.Towers
{
	public partial class TowerScript : Node2D
	{

		//Base Tower Stats
		[ExportGroup("Tower Stats")]
		[Export]
		private int _baseDamage;
		[Export]
		private float _baseRange;
		[Export]
		private float _baseAttackSpeed;
		
		//Child Refences, exporting them so I can easily add them
		//in the inspector rather than finding and setting them 
		//during runtime
		[ExportGroup("Child Refrences")]
		[Export]
		private Timer attackTimer;
		[Export]
		private Area2D enemyDetector;
		[Export]
		private Shape2D detectionArea;
		
		//List to keep track of targets
		private List<Node> _targets = new List<Node>();
		
		private bool _canAttack = true;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			//I have it set this way so it can attack as soon as something
			//enters its range of attack and then have the attack cooldown
			//start
			if (_targets.Count != 0 && _canAttack) {
				_Attack(_targets[0]);
				//EmitSignal(SignalName.AttackTarget);
				
				attackTimer.Start(_baseAttackSpeed);
				_canAttack = false;
			}
		}
		
		//This is a signal that triggers whenever something enters
		//the attached Area2D node
		//Adds enemies to the _target list when they are in range
		private void _on_area_2d_body_entered(Node body)
		{
			// Replace with function body
			if (!_targets.Contains(body.GetParent())) _targets.Add(body.GetParent());
		}
		
		//This is a signal that triggers whenever something exits
		//the attached Area2D node
		//Removes enemies to the _target list when they leave the range
		//of the tower
		private void _on_area_2d_body_exited(Node body)
		{
			// Replace with function body.
			if (_targets.Contains(body.GetParent())) _targets.Remove(body.GetParent());
		}
		
		//When the attached timer reaches 0
		//This is for simulating the attack speed, basically
		//when it reaches 0 the attack cooldown is over
		private void _on_timer_timeout()
		{
			// Replace with function body.
			_canAttack = true;
		}
		
		//function to attack enemy - simply calls the enemy's TakeDamage function
		//Might change in the future for more complicated attack situations
		private void _Attack(Node target)
		{
			GD.Print(this.Name + " attacks " + target.Name);
			//if (target.hasmethod("TakeDamage"))
				//target.TakeDamage(_baseDamage);
		}
	}
}
