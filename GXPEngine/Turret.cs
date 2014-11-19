using System;

namespace GXPEngine
{
	public class Turret : Enemy
	{
		int timer = 0;

		public Turret (Level level) : base(level, "checkers.png",1,1)
		{
		}

		void Update(){
			ApplyGravity ();
			timer++;
			if (timer % 100 == 0) {	// every 100'ticks' shoot
				Shoot (this);
			}
		}

	}
}

