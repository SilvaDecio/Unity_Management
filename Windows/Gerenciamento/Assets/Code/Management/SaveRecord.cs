using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class SaveRecord : MonoBehaviour
{
	Texture2D BackGroundImage;

    TextBox NameBox;
	
	Button OKButton;
	
	// Use this for initialization
	void Start ()
	{
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

            	BackGroundImage = (Texture2D)Resources.Load("BackGroundImages/SaveRecord");

            break;

        	case GameLanguage.Portugues:
			
				BackGroundImage = (Texture2D)Resources.Load("Telas/SalvarRecorde");
			
            break;
        }

        # endregion
		
		NameBox = new TextBox(new Vector2(500 , 300) , string.Empty);
		
		OKButton = new Button("Buttons/OK" , new Vector2(500 , 400));
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
                GetMenu();
			}
		}
	}
	
	void OnGUI()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);

        # region HUD Draw

        NameBox.Draw(StateManager.GameSkin.textField);
		
		OKButton.Draw();

        # endregion

        # region OK Button

        if (OKButton.Clicked)
		{
            if (NameBox.Text != string.Empty)
            {
                if (GamePlay.Score > PlayerController.Record1)
                {
                    if (GamePlay.Score > PlayerController.Record0)
                    {
                        PlayerController.Name2 = PlayerController.Name1;
                        PlayerController.Record2 = PlayerController.Record1;

                        PlayerController.Name1 = PlayerController.Name0;
                        PlayerController.Record1 = PlayerController.Record0;

                        PlayerController.Name0 = NameBox.Text;
                        PlayerController.Record0 = GamePlay.Score;
                    }
                    else
                    {
                        PlayerController.Name2 = PlayerController.Name1;
                        PlayerController.Record2 = PlayerController.Record1;

                        PlayerController.Name1 = NameBox.Text;
                        PlayerController.Record1 = GamePlay.Score;
                    }
                }
                else
                {
                    PlayerController.Name2 = NameBox.Text;
                    PlayerController.Record2 = GamePlay.Score;
                }

                GetRanking();
            }
            else
            {
                //MessageBox
            }
        }

        # endregion

        StateManager.TransitionEffect();
	}

    void GetMenu()
    {
        GamePlay.ResetScore();

        GameObject.Destroy(GamePlay.LocalAudio);

        StateManager.ChangeState(GameStates.Menu);
    }

    void GetRanking()
    {
        PlayerController.Save();

        GamePlay.ResetScore();

        StateManager.ChangeState(GameStates.Ranking);
    }
}