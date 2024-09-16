using Godot;
using System;

public partial class PlayerAttack : Area2D
{
	PlayerMovement movement;
	AnimationTree animator;
	bool attacking = false;
	[Export] int playerDamage = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		movement = GetParent() as PlayerMovement;
		animator = movement.GetNode<AnimationTree>("AnimationTree");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		if(Input.IsActionJustPressed("interact") && !attacking){
			movement.UpdateAttackAnimationParameters();
			movement.frozen = true;
			animator.Set("parameters/conditions/is_moving", false);
			animator.Set("parameters/conditions/idle", false);
			
			attacking = true;
		}
		animator.Set("parameters/conditions/attack", attacking);
	}

	public void OnAreaEntered(Node2D area)
	{
		if(area is not MonsterHealth) return;

		(area as MonsterHealth).TakeDamage(playerDamage);
	}

	public void AttackEnded()
	{
		attacking = false;
		movement.frozen = false;
	}
}
