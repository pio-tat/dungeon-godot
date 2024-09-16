using Godot;
using System;

public partial class InteractionArea : Area2D
{
	[Export] string actionName;
	public string ActionName {get => actionName;}
	
	public Callable interact;

	bool enabled = true;

	public void OnBodyEntered(Node2D body)
	{
		GD.Print(body.Name);
		if(!body.IsInGroup("player") || !enabled) return;
		GD.Print("OOOOOOO");
		InteractionManager.RegisterArea(this);
	}

	public void OnBodyExited(Node2D body)
	{
		if(!body.IsInGroup("player") || !enabled) return;
		InteractionManager.UnregisterArea(this);
	}

	public void Disable()
	{
		enabled = false;
		InteractionManager.UnregisterArea(this);
	}
}
