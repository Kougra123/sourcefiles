using System;

namespace GXPEngine
{
	public class EnemySpawner : SolidBlock
	{
		Player _player;
		Level _level;
		private int timer = 0;
		private int spawntimer = 500;
		Charger charger;
		Turret turret;

		public EnemySpawner (Player player, Level level) : base("spawner.png", 1,1)
		{
			_player = player;
			_level = level;
		}

		void Update(){
			Console.WriteLine(timer);
			if (_player.x < this.x + 500 && _player.x > this.x - 500 &&
				_player.y < this.y + 500 && _player.y > this.y - 500) {
				timer++;
				if (timer % spawntimer == 0) {
					SpawnEnemy ();
					//Console.WriteLine (true);
				}
			}

		}

		public void SpawnEnemy(){
			switch(NumberUtil.GetDieValue(1)){
			case 1:
				charger = new Charger (_level);
				_level.AddChild (charger);
				charger.SetXY (this.x, this.y - 50);
				_level.AddToEnemyList (charger);
					break;
			case 2:
				turret = new Turret (_level);
				_level.AddChild (turret);
				turret.SetXY (this.x, this.y - 50);
				_level.AddToEnemyList (turret);
					break;
				case 3:
					break;



			}
		}

	}
}

