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

                BackGroundImage = Resources.Load("BackGroundImages/Loading") as Texture2D;

            break;

        	case GameLanguage.Portugues:

                BackGroundImage = Resources.Load("Telas/Carregando") as Texture2D;

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
	
	void OnGUI ()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);

        StateManager.TransitionEffect();
	}
}