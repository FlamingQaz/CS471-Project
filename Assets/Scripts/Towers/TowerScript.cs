using Godot;
using System.Collections;
using System.Collections.Generic;

public partial class TowerScript : Node2D
{

	[ExportGroup("Child Refrences")]
	[Export]
	private Timer attackTimer;
	[Export]
	private Area2D enemyDetector;
	[Export]
	private Shape2D detectionArea;
	
	//Base Tower Stats
	[ExportGroup("Tower Stats")]
	[Export]
	private int _baseDamage;
	[Export]
	private float _baseRange;
	[Export]
	private float _baseAttackSpeed;
	
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
			//this is where the attack function goes when I make it
			GD.Print(this.Name + " attacks " + _targets[0].Name);
			
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

}
