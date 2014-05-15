using UnityEngine;

using System.Collections;

public class TextBlock
{
	Rect BoundingRectangle;
	
	public string Text;
	
	public TextBlock (Vector2 Position , Vector2 Size , string text)
	{		
		Text = text;
		
		BoundingRectangle = new Rect(Position.x , Position.y , Size.x , Size.y);
	}

    public void Draw ()
    {
        GUI.Label(BoundingRectangle , Text);
    }

	public void Draw (GUIStyle LabelStyle)
	{
		GUI.Label(BoundingRectangle , Text , LabelStyle);
	}
}