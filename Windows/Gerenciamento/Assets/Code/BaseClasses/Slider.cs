using UnityEngine;

using System.Collections;

public class Slider
{
	public Rect BoundingRectangle;
	
	public float Value;
	
	public Slider (Vector2 Position , Vector2 Size , float value)
	{		
		BoundingRectangle = new Rect(Position.x , Position.y , Size.x , Size.y);
		
		Value = value;
	}

    public void Draw (float MinValue , float MaxValue)
    {
        Value = GUI.HorizontalSlider(BoundingRectangle , Value , MinValue , MaxValue);
    }

	public void Draw (float MinValue , float MaxValue , GUIStyle SliderStyle , GUIStyle ThumbStyle)
	{
        Value = GUI.HorizontalSlider(BoundingRectangle , Value , MinValue , MaxValue , SliderStyle , ThumbStyle);
	}
}