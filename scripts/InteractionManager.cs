using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

public partial class InteractionManager : Node2D
{
	Node2D player;
	Label label;
	const string baseText = "[E] to ";

	static List<InteractionArea> activeAreas = new();
	InteractionArea closestArea = null;
	bool canInteract = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		label = GetNode<Label>("Label");
	}

    public override void _Process(double delta)
    {
        if(activeAreas.Count > 0 && canInteract){
			closestArea = FindClosest();

			//setting label
			label.Text = baseText + closestArea.ActionName;
			label.GlobalPosition = closestArea.GlobalPosition;
			label.GlobalPosition = new Vector2(closestArea.GlobalPosition.X - (label.Size.X / 2), closestArea.GlobalPosition.Y - 60);
			label.Show();
		}else{
			label.Hide();
		}
    }

    public override async void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("interact") && canInteract){
			if(activeAreas.Count == 0) return;

			canInteract = false;
			label.Hide();

			closestArea.interact.Call();

			canInteract = true;
		}
    }

    public static void RegisterArea(InteractionArea area)
	{
		activeAreas.Add(area);
	}

	public static void UnregisterArea(InteractionArea area)
	{
		activeAreas.Remove(area);
	}

	InteractionArea FindClosest()
	{
		if(activeAreas.Count == 0) return null;

		InteractionArea closestArea = activeAreas[0];
		foreach(InteractionArea area in activeAreas){
			if(player.GlobalPosition.DistanceTo(area.GlobalPosition) < player.GlobalPosition.DistanceTo(closestArea.GlobalPosition))
				closestArea = area;
		}

		return closestArea;
	}
}
