using System;

namespace GXPEngine
{
	public abstract class Shooter : SolidBlock
	{
		Bullet bullet;
		bool faceright = true;
		Level _level;

		public Shooter (string image, Level level, int cols, int rows) : base(image, cols, rows)
		{
			_level = level;
		}

		public void SetFaceRight(bool truefalse){
			faceright = truefalse;
		}
		public bool GetFaceRight(){
			return faceright;
		}


		public void Shoot(Player player){
			bullet = new Bullet (faceright, 5, "Laser_Shot.png");
			_level.AddBullet (player, bullet);
			bullet.SetXY (this.x, this.y);
		}
		public void Shoot(Enemy enemy){
			bullet = new Bullet (faceright, 5,"Laser_Shot.png");
			_level.AddBullet (enemy, bullet);
			if (faceright) { //bullet spawns NOT inside the enemy but rather next to him to avoid collision with himself
				bullet.SetXY (this.x + 100, this.y);
			} else {
				bullet.SetXY (this.x - 100, this.y);
			}
		}

	}
}

