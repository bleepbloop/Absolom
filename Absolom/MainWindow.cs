using System;
using System.Collections;
using Gtk;
using Gdk;
using Cairo;

public partial class MainWindow: Gtk.Window
{
	
	int scale;
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		scale = 10;
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	protected virtual void DrawArea_On_Expose_Event (object o, Gtk.ExposeEventArgs args)
	{
		
		Gdk.Window window = this.GdkWindow;
		
		int width = 0;
		int height = 0;
		window.GetSize(out width, out height);
		
		Console.WriteLine("Width: " + width.ToString());
		Console.WriteLine("Height: " + height.ToString());
		
		DrawingArea area = (DrawingArea) o;
		Cairo.Context context = Gdk.CairoHelper.Create(area.GdkWindow);
		
		/*
		// Create Points
	    PointD p1,p2,p3,p4;
	    p1 = new PointD (0, 0);
	    p2 = new PointD (50, 0);
	    p3 = new PointD (50, 50);
	    p4 = new PointD (0, 50);
	    
		//Draw Box
	    context.MoveTo (p1);
	    context.LineTo (p2);
	    context.LineTo (p3);
	    context.LineTo (p4);
	    context.LineTo (p1);
	    context.ClosePath ();
		*/
		
		ArrayList grid = CreateGrid(width, height);
		
		for(int i = 0; i < grid.Count; i ++)
		{
			context.Rectangle((Cairo.Rectangle)grid[i]);
		}
		
		//Color Box
	    context.Color = new Cairo.Color (1, 1, 1);
	    context.FillPreserve ();
		
		//Create Outline
	    context.Color = new Cairo.Color (0, 0, 0);
		context.LineWidth = 1;
	    context.Stroke();
	    
		//Preform Garbage Collection
	    ((IDisposable) context.Target).Dispose ();                                      
	    ((IDisposable) context).Dispose ();

	
	}
	
	protected virtual void Grid_On_Scroll_Event (object o, Gtk.ScrollEventArgs args)
	{
		//ScrolledWindow scrolledWindow = (ScrolledWindow) o;
		
		Cairo.Context context = Gdk.CairoHelper.Create(DrawArea.GdkWindow);
		
		//scrolledWindow.Children.
		
		String direction = args.Event.Direction.ToString();
		
		Console.WriteLine(direction);
		
		if(direction.Equals("Down"))
		{	
			if(scale <= 6)
				scale++;
			else
				scale--;
		}
		else
		{
			scale++;
		}
		
		int width = 0;
		int height = 0;
		this.GdkWindow.GetSize(out width, out height);
		
		ArrayList grid = CreateGrid(width, height);
		
		for(int i = 0; i < grid.Count; i ++)
		{
			context.Rectangle((Cairo.Rectangle)grid[i]);
		}
		
		//Color Box
	    context.Color = new Cairo.Color (1, 1, 1);
	    context.FillPreserve ();
		
		//Create Outline
	    context.Color = new Cairo.Color (0, 0, 0);
		context.LineWidth = 0.25;
	    context.Stroke();

		
		//Preform Garbage Collection
	    ((IDisposable) context.Target).Dispose ();                                      
	    ((IDisposable) context).Dispose ();
	}
	
	private ArrayList CreateGrid(int width, int height)
	{
		ArrayList grid = new ArrayList();
		
		for(int i = 0; i < (width / scale) + 1; i++)
		{
			for( int j = 0; j < (height / scale) + 1; j++)
			{
				grid.Add(new Cairo.Rectangle((i * scale), (j * scale), scale, scale));
			}
		}
		
		return grid;
	}
	
}