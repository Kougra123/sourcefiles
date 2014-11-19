using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class Charger : Enemy
	{
		Player player;
		Level _level;
		int timer;
		float yspeed;
		float xspeed = 3;
		List<SolidBlock> blocklist = new List<SolidBlock>();
		SolidBlock solidblock;

		public Charger (Level level) : base(level, "NPC - Charger.png",1,1)
		{
			player = level.GetPlayer ();
			_level = level;
			blocklist = _level.GetSolidBlockList ();
		}

			

		void Update(){
			this.y += yspeed;
			yspeed *= 0.9f;
			ApplyGravity ();
			Movement ();
			CollisionCheck ();
			if ((player.x > this.x - 500 && player.x < this.x + 500)&& //x in range of 500
				(player.y > this.y - 500 && player.y < this.y + 500)){ //y in range of 500
				if (player.x > this.x - 64) {
					this.x += xspeed;	//move towards player
				}
				if (player.x < this.x + 64) {
					this.x -= xspeed;	//move towards player
				}
			}
			timer++;
			if  (timer % 100 == 0 && //if charger is standing against player, attack
				(player.x > this.x - 68 && player.x < this.x + 68 )&& // numbers need to be a bit bigger then the number above
				(player.y > this.y - 64 && player.y < this.y + 64 )) {
				MeleeAttack ();
			}
		}
		private void CollisionCheck(){
			for (int I = 0; I < blocklist.Count; I++) {
				solidblock = blocklist [I];

					if (this.y > solidblock.y && this.y < solidblock.y + solidblock.height + 5 && this.x > solidblock.x - solidblock.width && this.x < solidblock.x + solidblock.width) {
						//this.y = solidblock.y + solidblock.height + 7; //hit bottomside of block
					} else {
						if (this.x < solidblock.x && this.x > solidblock.x - solidblock.width - 5 && this.y > solidblock.y - solidblock.height +20 && this.y < solidblock.y + solidblock.height -20) {
							this.x -= 5;	//hit leftside of block
						if ((player.x > this.x - 500 && player.x < this.x + 500) && //x in range of 500
						    (player.y > this.y - 500 && player.y < this.y + 500)) { //y in range of 500
							Jump ();
						}
						}
						if (this.x > solidblock.x && this.x < solidblock.x + solidblock.width + 5 && this.y > solidblock.y - solidblock.height +20 && this.y < solidblock.y + solidblock.height - 20) {
							this.x += 5;	//hit rightside of block
						if ((player.x > this.x - 500 && player.x < this.x + 500) && //x in range of 500
						    (player.y > this.y - 500 && player.y < this.y + 500)) { //y in range of 500
							Jump ();
						}
						}
					}

			}
		}

		private void Jump(){
			yspeed = -15;
		}

		private void MeleeAttack(){
			this.Turn (45); // just to make it visual that he attacked
			player.ChangeHealth (1);
		}

	}
}

