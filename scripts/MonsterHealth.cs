using Godot;
using System;

public partial class MonsterHealth : Area2D
{
	AnimationPlayer hitEffect;
	[Export] int maxHp = 2;
	int hp;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		hp = maxHp;
		hitEffect = GetNode<AnimationPlayer>("HitEffect");
	}

	public void TakeDamage(int damage)
	{
		hp -= damage;
		if(hp <= 0){
			Die();
		}

		hitEffect.Play("hit");
	}

	void Die()
	{
		GetParent().QueueFree();
	}
}
