using System;

namespace GXPEngine
{
	public class SolidBlock : AnimSprite	//this will be abstract -> other tiles can be a solidblock, a solid block on its own is not allowed, just for testing now
	{
		public SolidBlock (string image,int cols, int rows) : base(image ,cols, rows)
		{
			SetOrigin (width / 2, height / 2);
		}
	}
}

