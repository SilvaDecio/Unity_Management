using UnityEngine;

using System.Collections;

public class TextBox
{
	public string Text;
	
	Rect BoundingRectangle;
	
	public TextBox (Vector2 Position , Vector2 Size , string text)
	{
		Text = text;
		
		BoundingRectangle = new Rect(Position.x , Position.y , Size.x , Size.y);
	}

    public void Draw ()
    {
        Text = GUI.TextField(BoundingRectangle , Text);
    }

	public void Draw (GUIStyle TextBoxStyle)
	{
		Text = GUI.TextField(BoundingRectangle , Text , TextBoxStyle);
	}
}