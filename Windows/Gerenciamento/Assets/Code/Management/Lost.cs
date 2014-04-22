using UnityEngine;

using System.Collections;

public class Lost : MonoBehaviour
{
	Texture2D BackGroundImage;
	
	// Use this for initialization
	void Start ()
	{
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

            	BackGroundImage = (Texture2D)Resources.Load("BackGroundImages/Lost");

            break;

        	case GameLanguage.Portugues:
			
				BackGroundImage = (Texture2D)Resources.Load("Telas/Perdeu");
			
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
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
			{
				GamePlay.ResetScore();

                GameObject.Destroy(GamePlay.LocalAudio);

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