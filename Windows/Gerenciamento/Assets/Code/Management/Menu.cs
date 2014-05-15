using UnityEngine;

using System.Collections;

public class Menu : MonoBehaviour
{
	Texture2D BackGroundImage;	
	
	Button PlayButton , DirectionsButton , CreditsButton , RankingButton , SettingsButton;

    public static GameObject LocalAudio;

    public GUIStyle ButtonStyle;

	// Use this for initialization
	void Start ()
	{		
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

                BackGroundImage = Resources.Load("BackGroundImages/Menu") as Texture2D;

            break;

        	case GameLanguage.Portugues:

                BackGroundImage = Resources.Load("Telas/Menu") as Texture2D;
			
            break;
        }

        # endregion
		
		# region Buttons
		
		PlayButton = new Button("Buttons/Menu/Play" , new Vector2(150 , 250));
		
		DirectionsButton = new Button("Buttons/Menu/Directions" , new Vector2(400 , 250));
		
		CreditsButton = new Button("Buttons/Menu/Credits" , new Vector2(650 , 250));
		
		RankingButton = new Button("Buttons/Menu/Ranking" , new Vector2(275 , 450));
		
		SettingsButton = new Button("Buttons/Menu/Settings" , new Vector2(575 , 450));
		
		# endregion

        # region Audio

        if (StateManager.HasAudioControl)
        {
            if (!GameObject.Find("AudioController"))
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
	
	void OnGUI ()
	{
        StateManager.DrawBackGroundImage(BackGroundImage);

        # region Buttons

        # region Draw

        PlayButton.Draw(ButtonStyle);
		DirectionsButton.Draw(ButtonStyle);
        CreditsButton.Draw(ButtonStyle);
        RankingButton.Draw(ButtonStyle);
        SettingsButton.Draw(ButtonStyle);

        # endregion

        # region Click

        if (!StateManager.IsOnTransition)
        {
            if (PlayButton.Clicked)
            {
                AudioController.PlaySoundEffect(EffetcType.Button);
                
                StateManager.ChangeState(GameStates.GamePlay);

                GameObject.Destroy(LocalAudio);
            }

            if (DirectionsButton.Clicked)
            {
                AudioController.PlaySoundEffect(EffetcType.Button);

                StateManager.ChangeState(GameStates.Directions);

                DontDestroyOnLoad(LocalAudio);
            }

            if (CreditsButton.Clicked)
            {
                AudioController.PlaySoundEffect(EffetcType.Button);

                StateManager.ChangeState(GameStates.Credits);

                DontDestroyOnLoad(LocalAudio);
            }

            if (RankingButton.Clicked)
            {
                AudioController.PlaySoundEffect(EffetcType.Button);
                
                StateManager.ChangeState(GameStates.Ranking);

                DontDestroyOnLoad(LocalAudio);
            }

            if (SettingsButton.Clicked)
            {
                AudioController.PlaySoundEffect(EffetcType.Button);

                StateManager.ChangeState(GameStates.Settings);

                DontDestroyOnLoad(LocalAudio);
            }
        }

        # endregion

        # endregion

        StateManager.TransitionEffect();
	}

    void CreateAudio ()
    {
        LocalAudio = new GameObject("AudioController");

        LocalAudio.AddComponent<AudioSource>();
        LocalAudio.audio.clip = AudioController.Songs[SongType.Menu];
        LocalAudio.audio.loop = true;
        LocalAudio.audio.volume = AudioController.SongVolume;

        LocalAudio.audio.Play();
    }
}