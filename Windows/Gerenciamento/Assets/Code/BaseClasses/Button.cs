using UnityEngine;

using System.Collections;

public class Button
{
	Texture2D Image;
	
	Rect BoundingRectangle;
	
	public bool Clicked;
	
	public Button (string ImagePath, Vector2 Position)
	{
		Image = (Texture2D)Resources.Load(ImagePath);
        
		BoundingRectangle = new Rect(Position.x , Position.y , Image.width , Image.height);
		
		Clicked = false;
	}
	
	public void Draw()
	{
		Clicked = GUI.Button(BoundingRectangle , Image);		
	}
}