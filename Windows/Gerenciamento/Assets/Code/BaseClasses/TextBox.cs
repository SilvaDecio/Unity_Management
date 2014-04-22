using UnityEngine;

using System.Collections;

public class TextBox
{
	public string Text;
	
	Rect BoundingRectangle;
	
	float Width = 100 , Heigth = 30;
	
	public TextBox (Vector2 Position , string text)
	{
		Text = text;
		
		BoundingRectangle = new Rect(Position.x , Position.y , Width , Heigth);
	}
	
	public void Draw(GUIStyle TextFieldStyle)
	{
		Text = GUI.TextField(BoundingRectangle , Text , TextFieldStyle);
	}
}