using System;
using Gtk;

namespace Absolom
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			//Absolom main = new Absolom();
			Application.Run ();
		}
	}
}
