using UnityEngine;

using System.Collections;

public class Credits : MonoBehaviour
{
	Texture2D BackGroundImage;

	// Use this for initialization
	void Start ()
	{
		# region Language
		
		switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:
			
				BackGroundImage = (Texture2D)Resources.Load("BackGroundImages/Credits");
            
            break;

        	case GameLanguage.Portugues:

           		BackGroundImage = (Texture2D)Resources.Load("Telas/Creditos");
			
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
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				StateManager.ChangeState(GameStates.Menu);
			}
		}
	}
	
	void OnGUI()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);

        StateManager.TransitionEffect();
	}
}