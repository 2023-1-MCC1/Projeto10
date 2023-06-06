using Godot;
using System;

public partial class real_world : Node2D
{
	// Called when the node enters the scene tree for the first time.


	public override void _Ready()
	{
		this.GetNode<Player>("Player").tempomaximo = 60;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
    private void _on_change_scene_body_entered(Node2D CharacterBody)
	{
		GetTree().ChangeSceneToFile("res://levels/Mapas/praia/cena1_Praia.tscn");
		
	}
   

//    private void on_timer_timeout()
//    {
//     tempomaximo2 = tempomaximo2 - 1;

// 	if(tempomaximo2 <= 0)
// 	{
// 		GetTree().ChangeSceneToFile("res://levels/game over/gameover.tscn");

// 	}
   



}
