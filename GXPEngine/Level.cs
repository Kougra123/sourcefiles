using System;
using System.IO;
using System.Collections.Generic;


namespace GXPEngine
{
	public class Level : GameObject
	{

		StreamReader streamreader;
		string filedata;
		int TILESIZE = 64;
		private string _currentlevel;
		Sprite background;

		Player player;
		SolidBlock solidblock;
		List<SolidBlock> solidblocklist = new List<SolidBlock> ();
		MyGame _game;
		Enemy enemy; 
		Turret turret;
		Charger charger;
		EnemySpawner enemyspawner;
		PowerUp powerup;

		List<Enemy> enemylist = new List<Enemy> ();
		List<Bullet> playerbullets = new List<Bullet>();
		List<Bullet> enemybullets = new List<Bullet>();
		Bullet bullet;
		List<PowerUp> PowerUps = new List<PowerUp>();

	

		public Level (string level, MyGame game) // "level1.txt" or "level2.txt"
		{
			_game = game;
			game.Add (this);
			_currentlevel = level + ".txt";
			background = new Sprite (level + "background.png");
			//AddChild (background);


			streamreader = new StreamReader (_currentlevel);
			filedata = streamreader.ReadToEnd ();
			streamreader.Close ();


			string[] lines = filedata.Split ('\n');
			for (int J = 0; J < lines.Length-1; J++) {
				string[] cols = lines [J].Split (',');
				for (int I = 0; I < cols.Length-1; I++) {
					string col = cols [I];
					int[,] _data = new int[lines.Length-1,cols.Length-1];
					_data [J, I] = int.Parse (col);
					int tile = _data [J, I];
					if (tile != 0) {
						AddGameObject (I * TILESIZE, J * TILESIZE, tile);
					}
				}
			}
		}

		private void AddGameObject(int x, int y, int tile){
			switch (tile) {
			case 1:
				player = new Player (this);
				AddChild (player);
				player.SetXY (x, y);
				break;
			case 2:
				solidblock = new SolidBlock ("colors.png",1,1);
				AddChild (solidblock);
				solidblock.SetXY (x, y);
				solidblocklist.Add (solidblock);
				break;
			case 3:
				turret = new Turret (this);
				AddChild (turret);
				turret.SetXY (x, y);
				enemylist.Add (turret);
				solidblocklist.Add (turret);
				break;
			case 4:
				charger = new Charger (this);
				AddChild (charger);
				charger.SetXY (x, y);
				enemylist.Add (charger);
				break;
			case 5:
				enemyspawner = new EnemySpawner (player,this);
				AddChild (enemyspawner);
				enemyspawner.SetXY (x, y);
				break;
			case 6:
				powerup = new PowerUp (player, this);
				AddChild (powerup);
				powerup.SetXY (x, y);
				PowerUps.Add (powerup);
				break;


			}



		}
		public void AddToEnemyList(Enemy enemy){
			enemylist.Add (enemy);
		}

		public Player GetPlayer(){
			return player;
		}
		public List<SolidBlock> GetSolidBlockList(){
			return solidblocklist;
		}

		public void AddBullet(Shooter shooter, Bullet bullet){
			AddChild (bullet);
			if (shooter is Player) {
				playerbullets.Add (bullet);
			}
			if (shooter is Enemy) {
				enemybullets.Add (bullet);
			}
		}
		private void Collision(){

			for (int C = 0; C < solidblocklist.Count; C++) {
				solidblock = solidblocklist [C];
				for (int I = 0; I < playerbullets.Count; I++) {
					bullet = playerbullets [I];
					if (bullet.HitTest (solidblock)) {
						playerbullets.Remove (bullet);
						bullet.Destroy ();
					}
				}
				for (int I = 0; I < enemybullets.Count; I++) {
					bullet = enemybullets [I];
					if (bullet.HitTest (solidblock)) {
						enemybullets.Remove (bullet);
						bullet.Destroy ();
					}
				}
			}


			for (int I = 0; I < solidblocklist.Count; I++) {
				solidblock = solidblocklist [I];
				if (player.y < solidblock.y && player.y > solidblock.y - solidblock.height - 5 && player.x > solidblock.x - solidblock.width && player.x < solidblock.x + solidblock.width) {
					player.y = solidblock.y - solidblock.height - 5; //hit topside of block
					player.ResetGravity ();
				} else {
					if (player.y > solidblock.y && player.y < solidblock.y + solidblock.height + 5 && player.x > solidblock.x - solidblock.width && player.x < solidblock.x + solidblock.width) {
						player.y = solidblock.y + solidblock.height + 7; //hit bottomside of block
					} else {
						if (player.x < solidblock.x && player.x > solidblock.x - solidblock.width - 5 && player.y > solidblock.y - solidblock.height +5 && player.y < solidblock.y + solidblock.height) {
							player.x -= 5;	//hit leftside of block
						}
						if (player.x > solidblock.x && player.x < solidblock.x + solidblock.width + 5 && player.y > solidblock.y - solidblock.height +5 && player.y < solidblock.y + solidblock.height) {
							player.x += 5;	//hit rightside of block
						}
					}
				}

			}
		}

		void Update(){

			for (int i = 0; i < PowerUps.Count; i++) {
				powerup = PowerUps [i];
				if (player.HitTest (powerup)) {
					powerup.AddPowerUps (player, this);
					PowerUps.Remove (powerup);
					powerup.Destroy ();

				}
			}

			for (int A = 0; A < enemylist.Count; A++) {
				enemy = enemylist [A];
				if (enemy.GetHealth () <= 0) {
					enemylist.Remove (enemy);
					if (solidblocklist.Contains(enemy)) {
						solidblocklist.Remove (enemy);
					}
					enemy.Destroy ();
				}
			}

			//Console.WriteLine (playerbullets.Count);
			for (int I = 0; I < playerbullets.Count; I++) {
				bullet = playerbullets [I];
				for (int A = 0; A < enemylist.Count; A++) {
					enemy = enemylist [A];
					if (bullet.HitTest (enemy)) {
						enemy.ChangeHealth (bullet.GetDamage ());
						playerbullets.Remove (bullet);
						bullet.Destroy ();
					}
				}

			}
			for (int I = 0; I < enemybullets.Count; I++) {
				bullet = enemybullets [I];
				if (player.HitTest (bullet)) {
					player.ChangeHealth (bullet.GetDamage ());
					enemybullets.Remove (bullet);
					bullet.Destroy ();
				}
			}


			Collision ();


			for (int I = 0; I < enemylist.Count; I++) {
				enemy = enemylist [I];
				for (int j = 0; j < solidblocklist.Count; j++) {
					solidblock = solidblocklist [j];
					if (enemy.y < solidblock.y && enemy.y > solidblock.y - solidblock.height - 5 && enemy.x > solidblock.x - solidblock.width && enemy.x < solidblock.x + solidblock.width) {
						enemy.y = solidblock.y - solidblock.height - 5; //hit topside of block
					}

				}
			}

		}



	}
}
