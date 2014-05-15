using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Ranking : MonoBehaviour
{
	Texture2D BackGroundImage;
	
	TextBlock Name0 , Name1 , Name2;

    TextBlock Record0 , Record1 , Record2;

    public GUIStyle LabelStyle;

	// Use this for initialization
	void Start ()
	{
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

            	BackGroundImage = Resources.Load("BackGroundImages/Ranking") as Texture2D;

            break;

        	case GameLanguage.Portugues:

                BackGroundImage = Resources.Load("Telas/Recordes") as Texture2D;
			
            break;
        }

        # endregion

        # region Players

        Name0 = new TextBlock(new Vector2(300 , 200) , new Vector2(100 , 50) , PlayerController.Name0);
        Record0 = new TextBlock(new Vector2(600 , 200) , new Vector2(100 , 50) , PlayerController.Record0.ToString());

        Name1 = new TextBlock(new Vector2(300 , 300) , new Vector2(100 , 50) , PlayerController.Name1);
        Record1 = new TextBlock(new Vector2(600 , 300) , new Vector2(100 , 50) , PlayerController.Record1.ToString());

        Name2 = new TextBlock(new Vector2(300 , 400) , new Vector2(100 , 50) , PlayerController.Name2);
        Record2 = new TextBlock(new Vector2(600 , 400) , new Vector2(100 , 50) , PlayerController.Record2.ToString());

        # endregion
    }

	// Update is called once per frame
	void Update ()
	{
		if (StateManager.IsOnTransition)
		{
			StateManager.Update();
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
                GameObject.Destroy(GamePlay.LocalAudio);

				StateManager.ChangeState(GameStates.Menu);
			}
		}
	}
	
	void OnGUI ()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);

        # region Players Draw

        if (PlayerController.Name0 != string.Empty)
		{
            Name0.Draw(LabelStyle);
            Record0.Draw(LabelStyle);
		}

        if (PlayerController.Name1 != string.Empty)
		{
            Name1.Draw(LabelStyle);
            Record1.Draw(LabelStyle);
		}

        if (PlayerController.Name2 != string.Empty)
		{
            Name2.Draw(LabelStyle);
            Record2.Draw(LabelStyle);
        }

        # endregion

        StateManager.TransitionEffect();
	}
}