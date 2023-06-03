using Godot;
using System;


public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float JumpVelocity = -400.0f;
	
	public PackedScene telaDeGameOver = GD.Load<PackedScene>("res://levels/game over/gameover.tscn");
	public AnimationPlayer animacao;
 	public AnimatedSprite2D jogador;
    
	public Normal_Enemie inimigo;
	public Area2D swordhit;
       
	public CollisionShape2D hitbox;

	public double player_health;

	public Vector2 position = new Vector2(1, 1);
	public Vector2 position2 = new Vector2(-1, 1);
	
    public bool verifica = false;

	public TextureProgressBar vidaCamera;
	public int tempomaximo ;
	public Label cronometro;

	public boss boss;

	

	
	
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private bool isAttacking;

	
	public override void _Ready()

	
	{
			
		animacao = this.GetNode<AnimationPlayer>("AnimationPlayer");
		jogador = this.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		inimigo = this.GetParent().GetNode<Normal_Enemie>("Normal_Enemie");
		swordhit = jogador.GetNode<Area2D>("SwordHit");
		hitbox = swordhit.GetNode<CollisionShape2D>("CollisionShape2D");
		player_health = 15;
		vidaCamera = this.GetNode<TextureProgressBar>("CanvasLayer/VidaCamera"); 
		vidaCamera.Value = player_health;
		vidaCamera.MaxValue = player_health;
        cronometro = this.GetNode<Label>("CanvasLayer/Label");
		cronometro.Text = tempomaximo.ToString();
		boss = this.GetParent().GetNode<boss>("Boss");
	}

	public void _on_timer_timeout()
	{
        tempomaximo = tempomaximo - 1;
        cronometro.Text = tempomaximo.ToString();

		if(tempomaximo <=0)
		{
			// GetParent().AddChild(telaDeGameOver.Instantiate());
			GetTree().ChangeSceneToFile("res://levels/game over/gameover.tscn");
		}
	}

	

    
	

    public void ataque(Normal_Enemie inimigo)	
	{		
		 inimigo.ne_health = inimigo.ne_health - 2;
		 if (inimigo.ne_health <= 0)
		  {
			inimigo.QueueFree();
			
		  }
	}
	
	public void ataque2(boss boss)
	{
		boss.bosshealth = boss.bosshealth - 2;
		if(boss.bosshealth <= 0)
		{
			boss.QueueFree();
		}
	}

	private void _on_sword_hit_area_entered(Area2D area)
{
	

	
	var classeInimigo = area.GetParent().GetClass();	
	if(classeInimigo == "CharacterBody2D" && area.GetParent().Name == "Boss")
	{
		
		area.GetParent<boss>().ataque2(area.GetParent<boss>());
		
		//this.ataque();
	}		
     	if(classeInimigo == "CharacterBody2D" && area.GetParent().Name != "Boss")
		 {
		area.GetParent<Normal_Enemie>().ataque(area.GetParent<Normal_Enemie>());

		 }


	//GD.Print(area.Name);
	
	// var classeBoss = area.GetParent();	
	// if (classeBoss.Name == "Boss")
	// {
	// 	area.GetParent<boss>().ataque2(classeBoss);
	//     // this.ataque2();
	// }

	}



	public override void _PhysicsProcess(double delta)
	{
		
	
    
		
		
		Vector2 velocity = Velocity;

		
		if (!IsOnFloor()){      
			velocity.Y += gravity * (float)delta;
		}
	   
	    
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			animacao.Play("jump");
		}

		 
		
		
		

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

	  Vector2 lastDirection = Vector2.Right;
		
		if (direction.X > 0)
		{
			
			jogador.FlipH = false;
			hitbox.Position = new Vector2(1.862f,-2.842f);

		}
		else if ( direction.X < 0)       
		{
     	jogador.FlipH = true;
		hitbox.Position = new Vector2(-13.838f,-2.842f);
             

		}
    //    verifica == false

		
		
        
    if (Input.IsActionJustPressed("ui_attack"))
{
    animacao.Play("attack");
    isAttacking = true;
    velocity = Vector2.Zero; 
}
else 
{
    if (!isAttacking) 
    {
        if (direction != Vector2.Zero)
        {
            if (velocity.Y == 0){
                velocity.X = direction.X * Speed;
                animacao.Play("running");
            }
        }
        else
        {
            if (velocity.Y == 0){
                velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
                animacao.Play("idle");
            }
        }
    }
    else 
    {
if (animacao.CurrentAnimation == "attack" && animacao.IsPlaying())        
{
            velocity = Vector2.Zero;
        }
        else
        {
            isAttacking = false;
        }
    }
}
		Velocity = velocity;
		MoveAndSlide();
	}
}

