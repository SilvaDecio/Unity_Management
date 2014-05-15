using UnityEngine;

using System.Linq;

using System.Collections;
using System.Collections.Generic;

public class Won : MonoBehaviour
{
	Texture2D BackGroundImage;
	
	bool IsRecordBroken;

    TextBlock ScoreInfo;

    public GUIStyle LabelStyle;
	
	// Use this for initialization
	void Start ()
	{
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

                BackGroundImage = Resources.Load("BackGroundImages/Won") as Texture2D;
                
            break;

        	case GameLanguage.Portugues:

                BackGroundImage = Resources.Load("Telas/Venceu") as Texture2D;
			
            break;
        }

        # endregion
		
		# region Is Record Broken ?

        if (PlayerController.Name0 == string.Empty || PlayerController.Name1 == string.Empty || PlayerController.Name2 == string.Empty)
		{
			IsRecordBroken = true;
		}
		else
		{
            if (GamePlay.Score > PlayerController.Record2)
			{
				IsRecordBroken = true;
			}
			else
			{
				IsRecordBroken = false;
			}
		}
        
		# endregion

        ScoreInfo = new TextBlock(new Vector2(500 , 300) , new Vector2(100 , 50) , GamePlay.Score.ToString());
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
            # region Escape

            if (Input.GetKeyDown(KeyCode.Escape))
			{
                GetMenu();
            }

            # endregion

            # region Enter / Mouse

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
			{
				if (IsRecordBroken)
				{
                    GetSaveRecord();
				}
				else
				{
                    GetMenu();
				}
            }

            # endregion
        }		
	}
	
	void OnGUI ()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);

        ScoreInfo.Draw(LabelStyle);

        StateManager.TransitionEffect();
	}

    void GetMenu ()
    {
        GamePlay.ResetScore();

        GameObject.Destroy(GamePlay.LocalAudio);

        StateManager.ChangeState(GameStates.Menu);
    }

    void GetSaveRecord ()
    {
        StateManager.ChangeState(GameStates.SaveRecord);
    }
}