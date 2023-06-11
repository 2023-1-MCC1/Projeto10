using Godot;
using System;

public partial class teste : CharacterBody2D
{
	public  float Speed = 10.0f;
	public const float JumpVelocity = -400.0f;

	public int vidainimiga;

   public Sprite2D testee;

   



	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
public override void _Ready()
{
	   vidainimiga = 2;
	   testee = this.GetNode("teste").GetNode<Sprite2D>("Sprite2D");
}

public void atacado()
   {
	  this.vidainimiga = this.vidainimiga - 2;
	  if (this.vidainimiga == 0)
	  {

		this.QueueFree();
	  }
   }

private void _on_area_2d_body_entered(Node2D body)
{
	GD.Print("bobs");
	
	 teste jogador = this.GetParent().GetNode<teste>("teste");
	 jogador.Speed = jogador.Speed * -1;
	//  this.atacado();
}



	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		
		Vector2 direction = new (1, 0);
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			
			
		}
		// if (velocity.X < 0)
		// 	{
		// 		testee.FlipH = true;
		// 	}
		

		Velocity = velocity;
		MoveAndSlide();
	}
}
