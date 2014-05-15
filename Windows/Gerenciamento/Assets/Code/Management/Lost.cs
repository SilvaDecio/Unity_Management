using UnityEngine;

using System.Collections;

public class Lost : MonoBehaviour
{
	Texture2D BackGroundImage;

    TextBlock ScoreInfo;

    public GUIStyle LabelStyle;

	// Use this for initialization
	void Start ()
	{
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

                BackGroundImage = Resources.Load("BackGroundImages/Lost") as Texture2D;

            break;

        	case GameLanguage.Portugues:

                BackGroundImage = Resources.Load("Telas/Perdeu") as Texture2D;
			
            break;
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
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
			{
				GamePlay.ResetScore();

                GameObject.Destroy(GamePlay.LocalAudio);

				StateManager.ChangeState(GameStates.Menu);
			}
		}
	}
	
	void OnGUI ()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);

        ScoreInfo.Draw(LabelStyle);

        StateManager.TransitionEffect();
	}
}