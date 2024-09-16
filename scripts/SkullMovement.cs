using Godot;
using System;

public partial class SkullMovement : CharacterBody2D
{
	Node2D player;
	bool chasing = false;
	AnimatedSprite2D sprite;
	[Export] float speed = 100f;
	bool isPlayerDead = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		player = GetTree().CurrentScene.GetNode<Node2D>("Player");
		player.Connect("OnPlayerDead", Callable.From(() => OnPlayerDeath()));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(isPlayerDead) return;
		if(chasing){
			Vector2 direction = player.GlobalPosition - GlobalPosition;
			direction = direction.Normalized();

			sprite.FlipH = direction.X < 0;

			Velocity = direction * speed;
			MoveAndSlide();
		}
	}

	public void OnBodyEntered(Area2D body)
	{
		if(body.IsInGroup("player")){
			GD.Print("chasing!");
			chasing = true;
		}
	}

	public void OnBodyExited(Area2D body)
	{
		if(body.IsInGroup("player")){
			GD.Print("Not chasing...");
			chasing = false;
		}
	}

	void OnPlayerDeath()
	{
		isPlayerDead = true;
	}
}
