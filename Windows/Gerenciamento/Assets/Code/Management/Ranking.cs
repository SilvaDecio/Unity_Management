using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Ranking : MonoBehaviour
{
	Texture2D BackGroundImage;
	
	TextBlock Name0, Name1, Name2;

    TextBlock Record0, Record1, Record2;

	// Use this for initialization
	void Start ()
	{
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

            	BackGroundImage = (Texture2D)Resources.Load("BackGroundImages/Ranking");

            break;

        	case GameLanguage.Portugues:
			
				BackGroundImage = (Texture2D)Resources.Load("Telas/Recordes");
			
            break;
        }

        # endregion

        # region Players

        Name0 = new TextBlock(new Vector2(500, 200), PlayerController.Name0);
        Record0 = new TextBlock(new Vector2(600, 200), PlayerController.Record0.ToString());

        Name1 = new TextBlock(new Vector2(500, 300), PlayerController.Name1);
        Record1 = new TextBlock(new Vector2(600, 300), PlayerController.Record1.ToString());

        Name2 = new TextBlock(new Vector2(500, 400), PlayerController.Name2);
        Record2 = new TextBlock(new Vector2(600, 400), PlayerController.Record2.ToString());

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
	
	void OnGUI()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);

        # region Players Draw

        if (PlayerController.Name0 != string.Empty)
		{
			Name0.Draw(StateManager.GameSkin.label);
            Record0.Draw(StateManager.GameSkin.label);
		}

        if (PlayerController.Name1 != string.Empty)
		{
            Name1.Draw(StateManager.GameSkin.label);
            Record1.Draw(StateManager.GameSkin.label);
		}

        if (PlayerController.Name2 != string.Empty)
		{
            Name2.Draw(StateManager.GameSkin.label);
            Record2.Draw(StateManager.GameSkin.label);
        }

        # endregion

        StateManager.TransitionEffect();
	}
}