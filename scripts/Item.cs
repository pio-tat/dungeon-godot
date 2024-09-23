using Godot;
using System;

public partial class Item : Sprite2D
{
	[Export] public Items itemType;
	[Export] int quantity = 1; 
	[Export] bool isChestItem;
	InteractionArea interaction;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		interaction = GetNode<InteractionArea>("InteractionArea");
		interaction.interact = new Callable(this, MethodName.OnInteract);

		if(isChestItem){
			interaction.Disable();
			Visible = false;
		}

		Texture = ItemData.data[itemType];
	}

	public void OnInteract()
	{
		interaction.Disable();
		
		QueueFree();
	}

	public void EnableItem()
	{
		Texture = ItemData.data[itemType];
		interaction.Enable();
	}
}
