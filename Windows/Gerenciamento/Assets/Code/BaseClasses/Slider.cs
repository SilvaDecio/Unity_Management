using UnityEngine;

using System.Collections;

public class Slider
{
	public Rect BoundingRectangle;
	
	float Width = 100 , Heigth = 100;
	
	public float Value;
	
	public Slider (Vector2 Position , float value)
	{		
		BoundingRectangle = new Rect(Position.x , Position.y , Width , Heigth);
		
		Value = value;
	}

	public void Draw(GUIStyle SliderStyle , GUIStyle ThumbStyle)
	{
        Value = GUI.HorizontalSlider(BoundingRectangle, Value, 0.0f, 10.0f, SliderStyle, ThumbStyle);
	}
}