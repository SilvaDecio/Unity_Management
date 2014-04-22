using UnityEngine;

using System.Collections;

public class Settings : MonoBehaviour
{
	Texture2D BackGroundImage;
	
	Button VibrationButton;
	
	Slider SongSlider , EffectSlider;
	
	TextBlock SongSliderInfo , EffectSliderInfo;

	// Use this for initialization
	void Start ()
	{		
		# region Sliders

        SongSlider = new Slider(new Vector2(500, 300), AudioController.SongVolume);

        EffectSlider = new Slider(new Vector2(700, 300), AudioController.EffectVolume);		
		
		SongSliderInfo = new TextBlock(new Vector2(SongSlider.BoundingRectangle.x , SongSlider.BoundingRectangle.y + 30) , string.Empty);
			
		EffectSliderInfo = new TextBlock(new Vector2(EffectSlider.BoundingRectangle.x , SongSlider.BoundingRectangle.y + 30) , string.Empty);
		
		# endregion
		
		# region Language

        switch (StateManager.CurrentLanguage)
        {
            case GameLanguage.English:

            	BackGroundImage = (Texture2D)Resources.Load("BackGroundImages/Settings");

            break;

        	case GameLanguage.Portugues:
			
				BackGroundImage = (Texture2D)Resources.Load("Telas/Configuracoes");
			
            break;
        }

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
			if(Input.GetKeyDown(KeyCode.Escape))
			{
                AudioController.Save();
				
				StateManager.ChangeState(GameStates.Menu);
			}

            GameObject.Find("AudioController").audio.volume = AudioController.SongVolume / 10;
		}		
	}
	
	void OnGUI()
	{
		StateManager.DrawBackGroundImage(BackGroundImage);
        
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

        # region Vibration Button

        //VibrationButton.Draw();

        //if (VibrationButton.Clicked)
        //{

        //}

        # endregion

        StateManager.TransitionEffect();
	}
}