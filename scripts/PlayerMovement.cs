using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	[Signal] public delegate void OnPlayerDeadEventHandler();
	[Export] float moveSpeed = 400f;
	[Export] Sprite2D sprite;
	[Export] PlayerAttack attack;
	AnimationTree animator;
	[Export] Vector2 direction;
	public bool frozen = false;
	Timer hitTimer;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Ready()
    {
		animator = GetNode<AnimationTree>("AnimationTree");
        hitTimer = GetNode("Hurtbox").GetNode<Timer>("HitTimer");
    }
    public override void _Process(double delta)
	{
		if(frozen) return;
		//getting direction
		direction = Input.GetVector("left", "right", "up", "down");

		if(direction == Vector2.Down){
			sprite.FlipH = false;
			attack.Rotation = Mathf.Pi;
		}else if(direction == Vector2.Up){
			sprite.FlipH = false;
			attack.Rotation = Mathf.Pi * 2;
		}else if(direction != Vector2.Zero){
			sprite.FlipH = direction.X < 0;
			attack.Rotation = direction.X > 0 ? Mathf.Pi / 2 : 1.5f * Mathf.Pi;
		}	

		//aplying movement
		Velocity = direction * moveSpeed;
		MoveAndSlide();

		UpdateAnimationParameters();
	}

	public void TookDamage()
	{
		// Position -= direction * 50;
		//frozen = true;
		hitTimer.Start();
	}
	public void EndOfHitFreeze()
	{
		GD.Print("Ended!");
		// frozen = false;
	}

	public void UpdateAnimationParameters()
	{
		if(Velocity == Vector2.Zero){
			animator.Set("parameters/conditions/idle", true);
			animator.Set("parameters/conditions/is_moving", false);
		}else{
			animator.Set("parameters/conditions/idle", false);
			animator.Set("parameters/conditions/is_moving", true);
		}

		if(direction != Vector2.Zero){
			animator.Set("parameters/idle/blend_position", direction);
			animator.Set("parameters/run/blend_position", direction);
		}
	}

	public void UpdateAttackAnimationParameters()
	{
		animator.Set("parameters/attack/blend_position", animator.Get("parameters/idle/blend_position"));
	}

	public void Death()
	{
		EmitSignal(SignalName.OnPlayerDead);
	}
}
