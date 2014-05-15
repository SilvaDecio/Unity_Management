using UnityEngine;

using System.Collections;

public class Settings : MonoBehaviour
{
	Texture2D BackGroundImage;

    # region Settings Properties

    Button VibrationButton;
	
	Slider SongSlider , EffectSlider;
	
	TextBlock SongSliderInfo , EffectSliderInfo;

    # endregion

    public GUIStyle SliderStyle , ThumbStyle , LabelStyle , ButtonStyle;

	// Use this for initialization
	void Start ()
	{		
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

                BackGroundImage = Resources.Load("BackGroundImages/Settings") as Texture2D;

            break;

        	case GameLanguage.Portugues:

                BackGroundImage = Resources.Load("Telas/Configuracoes") as Texture2D;
			
            break;
        }

        # endregion

        # region Sliders

        SongSlider = new Slider(new Vector2(300 , 300) , new Vector2(100 , 100) , AudioController.SongVolume * 10);

        EffectSlider = new Slider(new Vector2(600 , 300) , new Vector2(100 , 100) , AudioController.EffectVolume * 10);

        SongSliderInfo = new TextBlock(new Vector2(SongSlider.BoundingRectangle.x , SongSlider.BoundingRectangle.y + 30) , new Vector2(100 , 50) , string.Empty);

        EffectSliderInfo = new TextBlock(new Vector2(EffectSlider.BoundingRectangle.x , SongSlider.BoundingRectangle.y + 30) , new Vector2(100 , 50) , string.Empty);

        # endregion

        //VibrationButton = new Button("Buttons/Vibration", new Vector2());
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
            GameObject.Find("AudioController").audio.volume = AudioController.SongVolume;

			if(Input.GetKeyDown(KeyCode.Escape))
			{
                AudioController.Save();
				
				StateManager.ChangeState(GameStates.Menu);
			}
		}
	}
	
	void OnGUI ()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);
        
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

        SongSliderInfo.Draw(LabelStyle);
        EffectSliderInfo.Draw(LabelStyle);

        AudioController.SongVolume = (int)SongSlider.Value;
        AudioController.EffectVolume = (int)EffectSlider.Value;

        AudioController.SongVolume /= 10;
        AudioController.EffectVolume /= 10;
		
		# endregion

        # region Vibration Button

        //VibrationButton.Draw();

        //if (VibrationButton.Clicked)
        //{

        //}

        # endregion

        StateManager.TransitionEffect();
	}
}