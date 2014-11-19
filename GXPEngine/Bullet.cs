using System;

namespace GXPEngine
{
	public class Bullet : Sprite
	{
		float speed = 5;
		int damage = 1;


		public Bullet (bool faceright,float speedz,string image) : base(image)
		{
			if (!faceright) {
				Mirror (true, false);
			}
			SetScaleXY (0.5f, 0.75f); //sprite was very big
			speed = speedz;
			if (!faceright) { //if 'shooter' is facting to the left, bullet goes to the left too
				speed = -speed;
			}
		}
		public int GetDamage(){
			return damage;
		}


		void Update(){
			this.x += speed;

		}
	}
}

