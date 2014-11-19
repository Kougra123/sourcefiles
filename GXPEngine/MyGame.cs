using System;
using GXPEngine;


public class MyGame : Game
{	

	MainMenu mainmenu;
	Hud hud;
	//Player player;


	public MyGame () : base(800, 600, false)
	{


		mainmenu = new MainMenu (this);
		AddChild (mainmenu);

	}
	
	void Update () {
		if (hud != null) {
			hud.Display ();
		}
	}

	public void MakeHud(Player player){
		hud = new Hud (player);
		AddChild (hud);
		//hud.Display ();
	}

	static void Main() {
		new MyGame().Start();
	}


}

