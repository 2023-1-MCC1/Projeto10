using Godot;
using System;

public partial class cena1_Praia : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.GetNode<Player>("Player").tempomaximo = 20;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_area_2d_body_entered(Node2D CharacterBody)
	{
		 GetTree().ChangeSceneToFile("res://levels/Mapas/industriaa/industria1.tscn");
	}
}
