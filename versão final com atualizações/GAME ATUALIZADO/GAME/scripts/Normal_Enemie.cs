using Godot;
using System;

public partial class Normal_Enemie : CharacterBody2D
{
	public float Speed = 30.0f;
	public const float JumpVelocity = -400.0f;


	
	public double ne_health;
	public AnimationPlayer normalenemie_animacao;
	public AnimatedSprite2D normal_enemie; 
	public AnimatedSprite2D normal_enemie2; 
	public Player jogador;
	public double player_health;

	public RayCast2D walldetector;
	public RayCast2D walldetector2;
	public CollisionShape2D ataquebox;
	public CollisionShape2D detecatk;

	public bool seguirplayer  = false;

	private bool neattack;

	private bool virarinimigo = false; 
	
	private bool virarinimigo2 = false; 



	


	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		walldetector = this.GetNode<RayCast2D>("wall_detector");
		walldetector2 = this.GetNode<RayCast2D>("wall_detector2");
		normalenemie_animacao = this.GetNode<AnimationPlayer>("AnimationNormalEnemie");
		normal_enemie  = this.GetNode<AnimatedSprite2D>("NormalEnemieSprite");
		ne_health = 3;
		normal_enemie = this.GetNode<AnimatedSprite2D>("NormalEnemieSprite");
		jogador = this.GetParent().GetNode<Player>("Player");
		// player_health = 15;
		ataquebox = this.GetNode<CollisionShape2D>("ne_attack/ataquezao");
        detecatk = this.GetNode<CollisionShape2D>("ne_det/detectar");
		
	}


public void atacarplayer()
{
	jogador.player_health = jogador.player_health - 2;
	jogador.vidaCamera.Value = jogador.player_health;
	if (jogador.player_health  <= 0)
		  {
			jogador.QueueFree();
			GetTree().ChangeSceneToFile("res://levels/game over/gameover.tscn");

		  }
}


		
		   private void _on_ne_attack_body_entered(Node2D body)
	 {
	
	 
	if(body.Name == "Player")
	  {   
	   neattack = true;
	  
		this.atacarplayer();

	  }	 
	

	 }

 private void _on_ne_attack_body_exited(Node2D body)
		 {
	   
	   if(body.Name == "Player")
	  {   
		neattack = false;
	  
	
	  }
		 }

private void _on_ne_det_body_entered(Node2D body)
{
	 if(body.Name =="Player")
	 {
		neattack = true;
	 }
}

private void _on_ne_det_body_exited(Node2D body)
{
	if(body.Name == "Player")
	 {   
	   neattack = false;
	  
	
	 }
}






	 public void ataque(Normal_Enemie inimigo)
	{
		  if (inimigo.GetClass() == "CharacterBody2D")
		{		
		 inimigo.ne_health  = inimigo.ne_health  - 2;
		 if (inimigo.ne_health  <= 0)
		  {
			inimigo.QueueFree();
			
		  }
		}

	}






	 private void _on_sword_hit_area_entered(Area2D area)
{
	
	  var	enemy = area.GetParent<Normal_Enemie>();				
		ataque(enemy);	
	
}


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if(!IsOnFloor())
		{
			 velocity.Y += gravity * (float)delta;
		}
	   
	
		Vector2 direction = new Vector2(1,0);
		
			
		if (velocity.X > 0)
		{
			normal_enemie.FlipH = true;
			ataquebox.Position = new Vector2 (0.5f, 0f);
			detecatk.Position = new Vector2(57f, 10.833f);
		}
		if(velocity.X < 0)
		{
		   normal_enemie.FlipH = false;
		  ataquebox.Position = new Vector2 (-13.333f, 0f);
          detecatk.Position = new Vector2(50.5f, 10.833f);

		}



	if (walldetector.IsColliding())
	{
		

		virarinimigo = true; 
		virarinimigo2 = false;

	}
	
		if (walldetector2.IsColliding())
	{
		
		virarinimigo2 = true;
		virarinimigo = false;
		
	}
	
		
		
		
		if (direction != Vector2.Zero)
		{
			if (velocity.Y==0)
		{
			
			velocity.X = direction.X * Speed;
		}

			if(virarinimigo ==true)
			{
		velocity.X = direction.X * Speed * -1;
			}
		 if(virarinimigo2 ==true)
			{
				velocity.X = direction.X * Speed;
			}

			

			
		}
		
	if(neattack)	
	{
		 velocity.X = 0;
	
		GD.Print("atacou");
		normalenemie_animacao.Play("ne_attack");
	}
		
	 
	 else
	 {
		
			normalenemie_animacao.Play("ne_walk");
	 }
	
  	
		
	
	
	

	   
		Velocity = velocity;
		MoveAndSlide();
	}
}


 

