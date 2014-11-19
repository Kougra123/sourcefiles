using System;

namespace GXPEngine
{
	public class Player : Shooter
	{
		private float gravity = 5;
		private float basegravity = 5;
		private int timer = 0;
		private float yspeed = 0;
		private float xspeed = 0;

		private float frame = 0;
		private float minframe = 0;
		private float maxframe = 8;

		private int health = 5;
		private int lives = 3;

		private int freezepowerup = 0; 
		private int speedboost = 0; 
		private int shield = 0;

		public Player (Level level) : base ("Sprite Sheet Character.png", level, 20,1)
		{
			//SetOrigin (width / 2, height / 2);
			//SetScaleXY (2, 2);
		}


		public int GetHealth(){
			return health;
		}
		public int GetLives(){
			return lives;
		}

		private void SetFrames(int min, int max){
			minframe = min;
			maxframe = max;
		}

		public int GetFreeze(){
			return freezepowerup;
		}
		public int GetShield(){
			return shield;
		}
		public int GetSpeed(){
			return speedboost;
		}

		void Update(){
			//Console.WriteLine ("Freeze" + freezepowerup);
			//Console.WriteLine ("shield" + shield);
			//Console.WriteLine ("speed" + speedboost);
			frame += 0.2f;
			if (frame >= maxframe + 1) {
				frame = minframe;
			}
			if (frame < minframe) {
				frame = maxframe;
			}
			SetFrame ((int)frame);
			if (!GetFaceRight ()) {
				Mirror (true, false);
			} else {
				Mirror (false, false);
			}

			if (Input.GetKeyDown (Key.SPACE)) {
				Shoot (this);
			}
			if (Input.GetKey (Key.R)) {
				this.y -= 7;
				gravity = 0;
			}
			//Console.WriteLine (yspeed);
			//this.y += yspeed;
			if (yspeed < 0) {
				yspeed *= 0.95f;
			}
			xspeed *= 0.9f;
			gravity += 0.1f;
			Movement ();
			ApplyGravity ();
		}

		public void ResetGravity(){
			gravity = basegravity;
		}

		public void AddFreezePowerup(int amount)
		{
			freezepowerup += amount;
		}
		public void AddSpeedBoost(int amount)
		{
			speedboost += amount;
		}

		public void AddShield(int amount)
		{
			shield += amount;
		}


		private void Movement(){

			this.x += xspeed;
			this.y += yspeed;

			if (Input.GetKey (Key.A)) {
				//moving left
				xspeed = -5;
				SetFaceRight (false);
				SetFrames (1, 8);
			}
			if (Input.GetKey (Key.D)) {
				//moving right
				xspeed = 5;
				SetFaceRight (true);
				SetFrames (1, 8);
			}
			if (!Input.GetKey (Key.A) && !Input.GetKey (Key.D)) {
				SetFrames (0, 0); // idle
			}
			if (Input.GetKey (Key.W)) {
				if (timer <= 25) {
					timer++;
				}
				//_midAir = true;
			} else {
				if (timer > 0) {
					//FreezePlayer ();
					this.y -= 1;
					yspeed -= 20 + 0.6f * timer;
					timer = 0;
				}
			}

		}

		public void ChangeHealth(int damage){
			health -= damage;
		}

		private void ApplyGravity(){
			this.y += gravity;
		}
	}
}

