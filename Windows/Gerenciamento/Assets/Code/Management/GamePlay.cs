using UnityEngine;

using System.Collections;

public class GamePlay : MonoBehaviour
{
	Texture2D BackGroundImage , PauseImage;
	
	bool Paused;

    # region HUD Properties

    public static float Score;

    TextBlock ScoreInfo;

    # endregion

    # region Pause Porperties

    Button ResumeButton, RestartButton, MenuButton, VibrationButton;

    Slider SongSlider, EffectSlider;

    TextBlock SongSliderInfo, EffectSliderInfo;

    # endregion

    public static GameObject LocalAudio;

    public GUIStyle labelStyle , ButtonStyle;

	// Use this for initialization
	void Start ()
	{
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

            	BackGroundImage = Resources.Load("BackGroundImages/GamePlay") as Texture2D;

                PauseImage = Resources.Load("BackGroundImages/Pause") as Texture2D;
			
            break;

        	case GameLanguage.Portugues:

                BackGroundImage = Resources.Load("Telas/Jogando") as Texture2D;

                PauseImage = Resources.Load("Telas/Pausa") as Texture2D;
			
            break;
        }

        # endregion
		
		# region Pause

        # region Buttons

        ResumeButton = new Button("Buttons/Pause/Resume" , new Vector2(200 , 250));
		
		RestartButton = new Button("Buttons/Pause/Restart" , new Vector2(400 , 250));
		
		MenuButton = new Button("Buttons/Pause/Menu" , new Vector2(600 , 250));

        //VibrationButton = new Button("Buttons/Vibration", new Vector2());

        # endregion

        # region Sliders

        SongSlider = new Slider(new Vector2(300 , 450) , new Vector2(100 , 100) , AudioController.SongVolume * 10);

        EffectSlider = new Slider(new Vector2(600 , 450) , new Vector2(100 , 100) , AudioController.EffectVolume * 10);

        SongSliderInfo = new TextBlock(new Vector2(SongSlider.BoundingRectangle.x , SongSlider.BoundingRectangle.y + 30) , new Vector2(100 , 50) , string.Empty);

        EffectSliderInfo = new TextBlock(new Vector2(EffectSlider.BoundingRectangle.x , SongSlider.BoundingRectangle.y + 30) , new Vector2(100 , 50) , string.Empty);

        # endregion
		
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

        Restart();
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
			if (Paused)
            {
                # region Escape

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    OutPause();
                }

                # endregion

                GameObject.Find("AudioController").audio.volume = AudioController.SongVolume;
			}
			else
            {

                # region Escape

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GetPause();
                }

                # endregion

                # region Score

                Score += Time.deltaTime;
                
                ScoreInfo.Text = ((int)Score).ToString();

                # endregion

                # region Won

                if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
				{
                    GetWon();
				}
				
				# endregion
				
				# region Lost
				
				if (Input.GetKeyDown(KeyCode.Space))
				{
                    GetLost();
				}
				
				#endregion
			}	
		}
	}
	
	void OnGUI ()
	{
		if (Paused)
		{
			StateManager.DrawBackGroundImage(PauseImage);

            # region Buttons

            # region Draw

            ResumeButton.Draw(ButtonStyle);
            RestartButton.Draw(ButtonStyle);
            MenuButton.Draw(ButtonStyle);
            //VibrationButton.Draw(ButtonStyle);

            # endregion

            # region Click

            if (ResumeButton.Clicked)
			{
                OutPause();
			}
			
			if (RestartButton.Clicked)
			{
                LocalAudio.audio.Stop();

                AudioController.Save();
				
				Restart();
			}
			
			if (MenuButton.Clicked)
			{
                AudioController.Save();
				
				StateManager.ChangeState(GameStates.Menu);
            }

            //if (VibrationButton.Clicked)
            //{

            //}

            # endregion

            # endregion

            # region Sliders

            SongSlider.Draw(0.0f , 10.0f);
            EffectSlider.Draw(0.0f , 10.0f);

            # region Language

            switch (StateManager.CurrentLanguage)
        	{
        	    case GameLanguage.English:

                    SongSliderInfo.Text = "Musics		" + (int)SongSlider.Value;
                    EffectSliderInfo.Text = "Effects		" + (int)EffectSlider.Value;
			
        	    break;
			
        		case GameLanguage.Portugues:

                    SongSliderInfo.Text = "Musicas		" + (int)SongSlider.Value;
                    EffectSliderInfo.Text = "Efeitos		" + (int)EffectSlider.Value;
				
        	    break;
            }

            # endregion

            SongSliderInfo.Draw(labelStyle);
            EffectSliderInfo.Draw(labelStyle);

            AudioController.SongVolume = (int)SongSlider.Value;
            AudioController.EffectVolume = (int)EffectSlider.Value;

            AudioController.SongVolume /= 10;
            AudioController.EffectVolume /= 10;
			
			# endregion
		}
		else
		{
			StateManager.DrawBackGroundImage(BackGroundImage);

            ScoreInfo.Draw(labelStyle);
		}

        StateManager.TransitionEffect();
	}

    void Restart ()
    {
        Paused = false;

        Score = 0f;

        ScoreInfo = new TextBlock(new Vector2(500 , 300) , new Vector2(100 , 50) , Score.ToString());

        LocalAudio.audio.Play();
    }

    public static void ResetScore ()
    {
        Score = 0f;
    }

    void GetPause ()
    {
        if (StateManager.HasAudioControl)
        {
            LocalAudio.audio.Pause();
        }

        Paused = true;
    }

    void OutPause ()
    {
        if (StateManager.HasAudioControl)
        {
            LocalAudio.audio.Play();
        }

        AudioController.Save();

        Paused = false;
    }

    void GetWon ()
    {
        Score = (int)Score;

        DontDestroyOnLoad(LocalAudio);

        StateManager.ChangeState(GameStates.Won);
    }

    void GetLost ()
    {
        Score = (int)Score;

        DontDestroyOnLoad(LocalAudio);

        StateManager.ChangeState(GameStates.Lost);
    }

    void CreateAudio ()
    {
        LocalAudio = new GameObject("AudioController");

        LocalAudio.AddComponent<AudioSource>();
        LocalAudio.audio.clip = AudioController.Songs[SongType.GamePlay];
        LocalAudio.audio.loop = true;
        LocalAudio.audio.volume = AudioController.SongVolume;
    }
}