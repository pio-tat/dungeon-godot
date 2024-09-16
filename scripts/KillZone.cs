using Godot;
using System;

public partial class KillZone : Area2D
{
	
	void OnAreaEntered(Node2D body)
	{
		if(body is HurtBox){
			GD.Print("Hit!");
			(body as HurtBox).TakeDamage(1);
		}
	}
}
