using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public enum GameLanguage
{
	English, Portugues
}

public class StateManager : MonoBehaviour
{
    # region Transition Properties

    public static GameStates CurrentState;
	
	public static bool IsOnTransition;
	
	static bool FadeIn , FadeOut;
	
	static float AlphaChannel , AlphaTax;
	
	static Texture2D TransitionImage;
	
	static string NextScene;

    # endregion

    # region Hud Properties
    
    public static GUISkin GameSkin;

    public static Color DefaultColor;

    # endregion

	public static bool HasAudioControl , HasVibrationControl;
	
	public static GameLanguage CurrentLanguage;

    // Use this for initialization
	public static void Start()
    {
        # region Language

        if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            StateManager.CurrentLanguage = GameLanguage.Portugues;
        }
        else
        {
            StateManager.CurrentLanguage = GameLanguage.English;
        }

        # endregion

        # region Transition

        CurrentState = GameStates.Loading;
		
		IsOnTransition = false;
		
		NextScene = string.Empty;
		
		FadeIn = false;
		FadeOut = false;
		
		AlphaChannel = 0.0f;
		AlphaTax = 0.025f;
		
		TransitionImage = (Texture2D)Resources.Load("TransitionImage");

        # endregion

        # region HUD

        GameSkin = Resources.Load("GameSkin") as GUISkin;

        DefaultColor = GUI.color;

        # endregion

        # region Audio

        AudioController.Initialize();

        // Verificar o Controle do Som
        HasAudioControl = true;

        # endregion
        
        // Verificar o Controle da Vibração
        HasVibrationControl = true;

        PlayerController.Initialize();
	}

	// Update is called once per frame
	public static void Update()
    {
        # region Fade Out

        if (FadeOut)
		{
			AlphaChannel += AlphaTax;
			
			if (AlphaChannel >= 1.0f)
			{
				AlphaChannel = Mathf.Clamp01(AlphaChannel);
				
				FadeOut = false;
				
				FadeIn = true;
				
				Application.LoadLevel(NextScene);
			}
        }

        # endregion

        # region Fade In

        if (FadeIn)
		{
			AlphaChannel -= AlphaTax;
			
			if (AlphaChannel <= 0.0f)
			{
				AlphaChannel = Mathf.Clamp01(AlphaChannel);
				
				FadeIn = false;
			
				IsOnTransition = false;
			}
        }

        # endregion
    }
	
	public static void ChangeState(GameStates NextState)
	{	
		CurrentState = NextState;

		NextScene = CurrentState.ToString();
		
		IsOnTransition = true;
		
		FadeOut = true;
	}
	
	public static void DrawBackGroundImage(Texture2D BackGroundImage)
	{
		GUI.DrawTexture(new Rect(0 , 0 , Screen.width , Screen.height) , BackGroundImage);
	}

    public static void TransitionEffect()
    {
        if (IsOnTransition)
        {
            GUI.color = new Color(0, 0, 0, AlphaChannel);

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TransitionImage);

            GUI.color = DefaultColor;
        }
    }
}