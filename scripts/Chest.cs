using Godot;
using System;

public partial class Chest : Node2D
{
	AnimationPlayer animator;
	InteractionArea interactionArea;

    public override void _Ready()
    {
        animator = GetNode<AnimationPlayer>("animator");
		interactionArea = GetNode<InteractionArea>("InteractionArea");
		interactionArea.interact = new Callable(this, MethodName.OnInteract);
		animator.Play("idle");
    }

    public void OnInteract()
	{
		animator.Play("opening");
		interactionArea.Disable();
	}
}