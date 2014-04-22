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

	// Use this for initialization
	void Start ()
	{
		# region Sliders
		
		SongSlider = new Slider(new Vector2(500 , 450) , AudioController.SongVolume);

        EffectSlider = new Slider(new Vector2(800, 450), AudioController.EffectVolume);		
		
		SongSliderInfo = new TextBlock(new Vector2(SongSlider.BoundingRectangle.x , SongSlider.BoundingRectangle.y + 30) , string.Empty);
			
		EffectSliderInfo = new TextBlock(new Vector2(EffectSlider.BoundingRectangle.x , SongSlider.BoundingRectangle.y + 30) , string.Empty);
		
		# endregion
		
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

            	BackGroundImage = (Texture2D)Resources.Load("BackGroundImages/GamePlay");
			
				PauseImage = (Texture2D)Resources.Load("BackGroundImages/Pause");
			
            break;

        	case GameLanguage.Portugues:
			
				BackGroundImage = (Texture2D)Resources.Load("Telas/Jogando");
			
				PauseImage = (Texture2D)Resources.Load("Telas/Pausa");
			
            break;
        }

        # endregion
		
		# region Pause
		
		ResumeButton = new Button("Buttons/Pause/Resume" , new Vector2(400 , 250));
		
		RestartButton = new Button("Buttons/Pause/Restart" , new Vector2(600 , 250));
		
		MenuButton = new Button("Buttons/Pause/Menu" , new Vector2(800 , 250));

        //VibrationButton = new Button("Buttons/Vibration", new Vector2());
		
		# endregion

        # region Audio

        if (StateManager.HasAudioControl)
        {
            if (GameObject.Find("AudioController"))
            {
                //GameObject.Destroy(Menu.LocalAudio);
            }
            else
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
	
	void OnGUI()
	{
		if (Paused)
		{
			StateManager.DrawBackGroundImage(PauseImage);

            # region Buttons

            # region Draw

            ResumeButton.Draw();
			RestartButton.Draw();
			MenuButton.Draw();
            //VibrationButton.Draw();

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

            SongSlider.Draw(StateManager.GameSkin.horizontalSlider, StateManager.GameSkin.horizontalSliderThumb);

            EffectSlider.Draw(StateManager.GameSkin.horizontalSlider, StateManager.GameSkin.horizontalSliderThumb);

            AudioController.SongVolume = (int)SongSlider.Value;

            AudioController.EffectVolume = (int)EffectSlider.Value;

            # region Language

            switch (StateManager.CurrentLanguage)
        	{
        	    case GameLanguage.English:

                    SongSliderInfo.Text = "Musics		" + (int)AudioController.SongVolume;

                    EffectSliderInfo.Text = "Effects		" + (int)AudioController.EffectVolume;
			
        	    break;
			
        		case GameLanguage.Portugues:

                SongSliderInfo.Text = "Músicas		" + (int)AudioController.SongVolume;

                EffectSliderInfo.Text = "Efeitos		" + (int)AudioController.EffectVolume;
				
        	    break;
            }

            # endregion

            SongSliderInfo.Draw(StateManager.GameSkin.label);

            EffectSliderInfo.Draw(StateManager.GameSkin.label);
			
			# endregion
		}
		else
		{
			StateManager.DrawBackGroundImage(BackGroundImage);

            ScoreInfo.Draw(StateManager.GameSkin.label);
		}

        StateManager.TransitionEffect();
	}

    void Restart()
    {
        Paused = false;

        Score = 0f;

        ScoreInfo = new TextBlock(new Vector2(500 , 300), Score.ToString());

        LocalAudio.audio.Play();
    }

    public static void ResetScore()
    {
        Score = 0f;
    }

    void GetPause()
    {
        if (StateManager.HasAudioControl)
        {
            LocalAudio.audio.Pause();
        }

        Paused = true;
    }

    void OutPause()
    {
        if (StateManager.HasAudioControl)
        {
            LocalAudio.audio.Play();
        }

        AudioController.Save();

        Paused = false;
    }

    void GetWon()
    {
        Score = (int)Score;

        DontDestroyOnLoad(LocalAudio);

        StateManager.ChangeState(GameStates.Won);
    }

    void GetLost()
    {
        Score = (int)Score;

        DontDestroyOnLoad(LocalAudio);

        StateManager.ChangeState(GameStates.Lost);
    }

    void CreateAudio()
    {
        LocalAudio = new GameObject("AudioController");
        LocalAudio.AddComponent<AudioSource>();
        LocalAudio.audio.clip = AudioController.Songs[SongType.GamePlay];
        LocalAudio.audio.loop = true;
        LocalAudio.audio.volume = AudioController.SongVolume / 10;
    }
}