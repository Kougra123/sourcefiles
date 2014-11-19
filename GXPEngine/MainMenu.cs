using System;

namespace GXPEngine
{
	public class MainMenu : GameObject
	{
		Level _level;
		MyGame _game;


		public MainMenu (MyGame game)
		{
			_game = game;
			LoadLevel (1);
		}

		private void LoadLevel(int level){

			if (level == 1) {
				_level = new Level ("level1", _game);
				AddChild (_level);
			}
			if (level == 2) {
				_level = new Level ("level2", _game);
				AddChild (_level);
			}

			_game.RemoveChild (this); // after level is loaded, remove this menu
			_game.MakeHud (_level.GetPlayer());
		}

	}
}

