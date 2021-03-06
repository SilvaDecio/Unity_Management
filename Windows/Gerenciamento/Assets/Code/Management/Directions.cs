using UnityEngine;

using System.Collections;

public class Directions : MonoBehaviour
{
	Texture2D BackGroundImage;

	// Use this for initialization
	void Start()
	{
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

                BackGroundImage = Resources.Load("BackGroundImages/Directions") as Texture2D;

            break;

            case GameLanguage.Portugues:

                BackGroundImage = Resources.Load("Telas/Instrucoes") as Texture2D;
			
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
	
	void OnGUI ()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);

        StateManager.TransitionEffect();
	}
}