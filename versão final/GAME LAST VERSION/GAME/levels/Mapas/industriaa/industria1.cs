using Godot;
using System;

public partial class industria1 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.GetNode<Player>("Player").tempomaximo = 90;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	

	private void _on_final_body_entered(Node2D CharacterBody)
	{
        GetTree().ChangeSceneToFile("res://mensagemfinal/mensagemfinal.tscn");

	}
}
