using System;

namespace GXPEngine
{
	public class Enemy : Shooter
	{

		private int health = 1;
		//private int score = 0; 
		private int gravity = 5;
		private int timer = 0;
		private float yspeed = 0;

		public Enemy(Level level, string image, int cols, int rows) :  base (image, level,cols, rows)
		{

			//_movement = true;
			//SetOrigin (width / 2, height / 2);
			SetFaceRight (false);
		}

		public void ChangeHealth(int damage){
			health -= damage;
		}

		public void Movement()
		{
			if (Input.GetKey (Key.LEFT)) 
			{
				//moving left
				this.x -= 5;
			}
			if (Input.GetKey (Key.RIGHT)) 
			{
				//moving right
				this.x += 5;
			}

			if (Input.GetKey (Key.UP)) 
			{
				if (timer <= 25) 
				{
					timer++;
				}
				//_midAir = true;
			} 

			else 
			{
				if (timer > 0) 
				{   //Freeze ();
					this.y -= 1;
					yspeed -= 20 + 0.6f * timer;
					timer = 0;
				}
			}
		}

		public void ApplyGravity()
		{
			this.y += gravity;
		}
		public int GetHealth(){
			return health;
		}




	}
}

