using System;

namespace GXPEngine
{
	public class NumberUtil
	{
		private static Random rnd = new Random ();

		static public int GetDieValue (int sides)
		{
			int dieRoll = rnd.Next (1, sides + 1);
			return dieRoll;
		}
	}
}