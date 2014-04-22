using UnityEngine;

using System.Collections;

public class Loading : MonoBehaviour
{
	Texture2D BackGroundImage;

	// Use this for initialization
	void Start ()
	{
        StateManager.Start();

		# region Language
		
		switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:
			
				BackGroundImage = (Texture2D)Resources.Load("BackGroundImages/Loading");

            break;

        	case GameLanguage.Portugues:

            	BackGroundImage = (Texture2D)Resources.Load("Telas/Carregando");

            break;
        }
		
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
			if (Time.time >= 2)
      		{			
				StateManager.ChangeState(GameStates.Menu);
      		}
			
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}		
	}
	
	void OnGUI()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);

        StateManager.TransitionEffect();
	}
}