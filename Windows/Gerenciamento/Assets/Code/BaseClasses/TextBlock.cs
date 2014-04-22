using UnityEngine;

using System.Collections;

public class TextBlock
{
	Rect BoundingRectangle;
	
	public string Text;
	
	static float Width = 100 , Heigth = 50;
	
	public TextBlock (Vector2 Position , string text)
	{		
		Text = text;
		
		BoundingRectangle = new Rect(Position.x , Position.y , Width , Heigth);
	}
	
	public void Draw(GUIStyle LabelStyle)
	{
		GUI.Label(BoundingRectangle , Text , LabelStyle);
	}
}