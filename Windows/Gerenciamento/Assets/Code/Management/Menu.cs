using UnityEngine;

using System.Collections;

public class Menu : MonoBehaviour
{
	Texture2D BackGroundImage;	
	
	Button PlayButton , DirectionsButton , CreditsButton , RankingButton , SettingsButton;

    public static GameObject LocalAudio;

	// Use this for initialization
	void Start ()
	{		
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

            	BackGroundImage = (Texture2D)Resources.Load("BackGroundImages/Menu");

            break;

        	case GameLanguage.Portugues:
			
				BackGroundImage = (Texture2D)Resources.Load("Telas/Menu");
			
            break;
        }

        # endregion
		
		# region Buttons
		
		PlayButton = new Button("Buttons/Menu/Play" , new Vector2(200 , 300));
		
		DirectionsButton = new Button("Buttons/Menu/Directions" , new Vector2(400 , 300));
		
		CreditsButton = new Button("Buttons/Menu/Credits" , new Vector2(600 , 300));
		
		RankingButton = new Button("Buttons/Menu/Ranking" , new Vector2(800 , 300));
		
		SettingsButton = new Button("Buttons/Menu/Settings" , new Vector2(1000 , 300));
		
		# endregion

        # region Audio

        if (StateManager.HasAudioControl)
        {
            if (GameObject.Find("AudioController"))
            {
                //if (GameObject.Find("AudioController").audio.clip == AudioController.Songs[SongType.GamePlay])
                //{
                    
                //}

                //GameObject.Destroy(GamePlay.LocalAudio);
            }
            else
            {
                CreateAudio();
            }
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
				Application.Quit();
			}
		}
	}
	
	void OnGUI()
	{
        StateManager.DrawBackGroundImage(BackGroundImage);

        # region Buttons

        # region Draw

        PlayButton.Draw();
		DirectionsButton.Draw();
		CreditsButton.Draw();
		RankingButton.Draw();
		SettingsButton.Draw();

        # endregion

        # region Click

        if (!StateManager.IsOnTransition)
        {
            if (PlayButton.Clicked)
            {
                StateManager.ChangeState(GameStates.GamePlay);

                GameObject.Destroy(LocalAudio);
            }

            if (DirectionsButton.Clicked)
            {
                StateManager.ChangeState(GameStates.Directions);

                DontDestroyOnLoad(LocalAudio);
            }

            if (CreditsButton.Clicked)
            {
                StateManager.ChangeState(GameStates.Credits);

                DontDestroyOnLoad(LocalAudio);
            }

            if (RankingButton.Clicked)
            {
                StateManager.ChangeState(GameStates.Ranking);

                DontDestroyOnLoad(LocalAudio);
            }

            if (SettingsButton.Clicked)
            {
                StateManager.ChangeState(GameStates.Settings);

                DontDestroyOnLoad(LocalAudio);
            }
        }

        # endregion

        # endregion

        StateManager.TransitionEffect();
	}

    void CreateAudio()
    {
        LocalAudio = new GameObject("AudioController");
        LocalAudio.AddComponent<AudioSource>();
        LocalAudio.audio.clip = AudioController.Songs[SongType.Menu];
        LocalAudio.audio.loop = true;
        LocalAudio.audio.volume = AudioController.SongVolume / 10;
        LocalAudio.audio.Play();
    }
}