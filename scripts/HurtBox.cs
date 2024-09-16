using Godot;
using System;

public partial class HurtBox : Area2D
{
	AnimationPlayer hitAnimation;
	AnimationTree animator;
	[Export] int maxHP = 3;
	[Export] PlayerMovement movement;
	int hp;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		hp = maxHP;
		hitAnimation = GetNode<AnimationPlayer>("HitEffect");
		animator = movement.GetNode<AnimationTree>("AnimationTree");
		Engine.TimeScale = 1f;
	}

	public void TakeDamage(int damage)
	{
		hitAnimation.Play("hit");
		movement.TookDamage();
		hp -= damage;
		if(hp <= 0){
			Die();
		}
	}

	void Die()
	{
		movement.Death();
		movement.frozen = true;
		movement.ZIndex = 99;
		var stateMachine = animator.Get("parameters/playback").As<AnimationNodeStateMachinePlayback>();
		stateMachine.Travel("death");
	}

	void ResetScene()
	{
		GetTree().ReloadCurrentScene();
	}
}
