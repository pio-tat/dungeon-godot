using Godot;
using System;

public partial class Chest : Node2D
{
	[Export] Items loot;
	AnimationPlayer animator;
	InteractionArea interactionArea;
	Item item;

    public override void _Ready()
    {
        animator = GetNode<AnimationPlayer>("animator");
		interactionArea = GetNode<InteractionArea>("InteractionArea");
		item = GetNode<Item>("item");

		interactionArea.interact = new Callable(this, MethodName.OnInteract);
		animator.Play("idle");
		item.itemType = loot;
    }

    public void OnInteract()
	{
		animator.Play("opening");
		interactionArea.Disable();
	}
}