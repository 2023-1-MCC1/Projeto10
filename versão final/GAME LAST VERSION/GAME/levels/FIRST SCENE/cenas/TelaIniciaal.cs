using Godot;
using System;

public partial class TelaIniciaal : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://levels/prologo.tscn");
	}

	private void _on_button2_pressed()
	{
	}

	private void _on_button_2_pressed()
	{
		GetTree().ChangeSceneToFile("res://Creditos/creditos.tscn");

	}
}
