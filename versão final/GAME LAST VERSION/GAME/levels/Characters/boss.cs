using Godot;
using System;

public partial class boss : CharacterBody2D
{
	public const float Speed = 70.0f;
	public const float JumpVelocity = -400.0f;

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public AnimationPlayer bossanimation;
	public AnimatedSprite2D bossasprite;

	public RayCast2D bosswalldetector1;
	public RayCast2D bosswalldetector2;
    
	public CollisionShape2D bosshit;
	public CollisionShape2D bosshitdet;

     private bool bosstack;

	 public double bosshealth;

	public TextureProgressBar vidaboss;



	public bool virarboss= false;
	public bool virarboss2 = false;

	public Player jogador;


	public override void _Ready()
	{
	  bossanimation = this.GetNode<AnimationPlayer>("BossAnimation");
	  bossasprite = this.GetNode<AnimatedSprite2D>("BossSprite");
	  bosswalldetector1 = this.GetNode<RayCast2D>("walldetector");
	  bosswalldetector2 = this.GetNode<RayCast2D>("walldetector2");
	  jogador = this.GetParent().GetNode<Player>("Player");
	  bosshit = this.GetNode<CollisionShape2D>("bossattack/col_attack");
	  bosshitdet = this.GetNode<CollisionShape2D>("bossatkdet/col_det");
	  bosshealth = 25;
	  vidaboss.Value = bosshealth;
	  vidaboss.MaxValue = bosshealth;
	}

	public void atacarplayer()
{
	jogador.player_health = jogador.player_health - 7;
	jogador.vidaCamera.Value = jogador.player_health;
	if (jogador.player_health  <= 0)
		  {
			jogador.QueueFree();
			GetTree().ChangeSceneToFile("res://levels/game over/gameover.tscn");

		  }
}

public void ataque2(boss boss)
	{
		  if (boss.GetClass() == "CharacterBody2D")
		{		
		bosshealth  = bosshealth  - 10;
		 if (bosshealth  <= 0)
		  {
			boss.QueueFree();
			
		  }
		}

	}

private void _on_bossattack_body_entered(Node2D body)
{
	if(body.Name == "Player")
	  {   
	   bosstack = true;
	  
		this.atacarplayer();

	  }	 
	
}

private void _on_bossattack_body_exited(Node2D body)
{
	if(body.Name == "Player")
	  {   
		bosstack = false;
	  
	
	  }
}


private void _on_bossatkdet_body_entered(Node2D body)
{
	 if(body.Name =="Player")
	 {
		bosstack = true;
	 }
}

private void _on_bossatkdet_body_exited(Node2D body)
{
	if(body.Name == "Player")
	 {   
	   bosstack = false;
	  
	
	 }
}



	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}

		Vector2 direction = new Vector2(1,0);

		if (velocity.X > 0)
		{
			bossasprite.FlipH = false;
			bosshit.Position = new Vector2 (13f, 8.75f);
			bosshitdet.Position = new Vector2(6.872F, 3F);

		}

		if (velocity.X < 0)
		{
			bossasprite.FlipH = true;
			bosshit.Position = new Vector2 (-15f, 9);
			bosshitdet.Position = new Vector2(-5.532F, 3F);


		}


		if(bosswalldetector1.IsColliding())
		{
			virarboss = true;
			virarboss2 = false;
			GD.Print("VIRAR");
		}

		if (bosswalldetector2.IsColliding())
		{
			virarboss2 = true;
			virarboss = false;
			GD.Print("VIRAR2");

		}
	   
		 if(direction != Vector2.Zero)
		 {
			if(velocity.Y==0)
			{
				velocity.X = direction.X * Speed;
			}

			if(virarboss == true)
			{
				velocity.X = direction.X * Speed * -1;
			}

			if(virarboss2 ==true)
			{
				velocity.X = direction.X * Speed;
			}
		 }

		 if(bosstack)
		 {
			 velocity.X = 0;

			 bossanimation.Play("boss_attack");
		 }

		 else
		 {
			 bossanimation.Play("boss_run");
		 }
		

		Velocity = velocity;
		MoveAndSlide();
	}
}
