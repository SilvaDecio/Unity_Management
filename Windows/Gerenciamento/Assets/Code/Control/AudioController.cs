using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public enum SongType
{
    Menu , GamePlay
}

public enum EffetcType
{
    Button
}

public class AudioController
{
    public static float SongVolume , EffectVolume;

    public static Dictionary<SongType , AudioClip> Songs;

    public static Dictionary<EffetcType , AudioClip> Effects;

    public static void Initialize ()
    {
        if (!PlayerPrefs.HasKey("SongVolume"))
        {
            SongVolume = 0.5f;
            EffectVolume = 0.5f;

            PlayerPrefs.SetFloat("SongVolume" , SongVolume);
            PlayerPrefs.SetFloat("EffectVolume" , EffectVolume);

            PlayerPrefs.Save();
        }
        else
        {
            SongVolume = PlayerPrefs.GetFloat("SongVolume");
            EffectVolume = PlayerPrefs.GetFloat("EffectVolume");
        }

        Songs = new Dictionary<SongType , AudioClip>();
        Effects = new Dictionary<EffetcType , AudioClip>();

        Songs.Add(SongType.Menu , Resources.Load("Audio/Songs/Menu") as AudioClip);
        Songs.Add(SongType.GamePlay , Resources.Load("Audio/Songs/GamePlay") as AudioClip);

        Effects.Add(EffetcType.Button , Resources.Load("Audio/Effects/Button") as AudioClip);
    }

    public static void Save ()
    {
        PlayerPrefs.SetFloat("SongVolume" , SongVolume);
        PlayerPrefs.SetFloat("EffectVolume" , EffectVolume);

        PlayerPrefs.Save();
    }

    public static void Delete ()
    {
        PlayerPrefs.DeleteKey("SongVolume");
        PlayerPrefs.DeleteKey("EffectVolume");

        PlayerPrefs.Save();
    }

    public static void PlaySoundEffect (EffetcType Mode)
    {
        AudioSource.PlayClipAtPoint(Effects[Mode] , new Vector3() , EffectVolume);
    }
}