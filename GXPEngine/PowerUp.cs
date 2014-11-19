using System;

namespace GXPEngine
{
	public class PowerUp : Sprite
	{	
		//Level _level;
		Player _player;
		int dropchance;
		//int freezepowerupchance;
		//int shieldchance;
		//int speedboostchance;

		public PowerUp (Player player, Level level) : base ("checkers.png")
		{
			_player = player;
		}

		public void AddPowerUps(Player player, Level level)
		{
			dropchance = 0;
			//freezepowerupchance = 3;
			//shieldchance = 6; 
			//speedboostchance = 9;



			if (dropchance < NumberUtil.GetDieValue (10)) 
			{
				switch (NumberUtil.GetDieValue (3)) 
				{
				case 1:
					player.AddFreezePowerup (1);
					break;

				case 2: 
					player.AddShield (1);
					break;

				case 3: 
					player.AddSpeedBoost (1);
					break;

				}


			}



		}


	}
}