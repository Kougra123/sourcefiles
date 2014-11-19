using System;
using System.Drawing;

namespace GXPEngine
{
	public class Hud : Canvas // for now a canvas, will change because instead of tekst we will have pictures for health n stuff
	{
		Player _player;

		Font font;
		Brush brush;
		PointF position;
		PointF position2;

		PointF powerup1;
		PointF powerup2;
		PointF powerup3;

		public Hud (Player player) : base(800,250)
		{
			_player = player;
			font = new Font ("Arial", 20, FontStyle.Regular);
			brush = new SolidBrush (Color.White);
			position = new PointF (0, 0);
			position2 = new PointF (0, 25);

			powerup1 = new PointF (0, 100);
			powerup2 = new PointF (0, 125);
			powerup3 = new PointF (0, 150);
		}

		public void Display(){
			graphics.Clear (Color.Empty);
			graphics.DrawString ("health: " + _player.GetHealth(), font, brush, position);
			graphics.DrawString ("lives: " + _player.GetLives(), font, brush, position2);

			if (_player.GetFreeze () != 0) {
				graphics.DrawString ("Freeze: " + _player.GetFreeze(), font, brush, powerup1);
			}
			if (_player.GetShield () != 0) {
				graphics.DrawString ("Shield: " + _player.GetShield (), font, brush, powerup2);
			}
			if (_player.GetSpeed () != 0) {
				graphics.DrawString ("Speed: " + _player.GetSpeed (), font, brush, powerup3);
			}
		}

	}
}

