using UnityEngine;

using System.Collections;

public class PlayerController
{
	public static string Name0 , Name1 , Name2;
	
	public static float Record0 , Record1 ,Record2;
    
    public static void Initialize()
    {
        if (!PlayerPrefs.HasKey("Name0"))
        {
            PlayerPrefs.SetString("Name0", string.Empty);
            PlayerPrefs.SetString("Name1", string.Empty);
            PlayerPrefs.SetString("Name2", string.Empty);

            PlayerPrefs.SetFloat("Record0", 0);
            PlayerPrefs.SetFloat("Record1", 0);
            PlayerPrefs.SetFloat("Record2", 0);

            PlayerPrefs.Save();
        }
        else
        {
            //PlayerController.Delete();

            Name0 = PlayerPrefs.GetString("Name0");
            Name1 = PlayerPrefs.GetString("Name1");
            Name2 = PlayerPrefs.GetString("Name2");

            Record0 = PlayerPrefs.GetFloat("Record0");
            Record1 = PlayerPrefs.GetFloat("Record1");
            Record2 = PlayerPrefs.GetFloat("Record2");
        }
    }

	public static void Save()
	{
		PlayerPrefs.SetString("Name0" , Name0);
		PlayerPrefs.SetString("Name1" , Name1);
		PlayerPrefs.SetString("Name2" , Name2);
		
		PlayerPrefs.SetFloat("Record0" , Record0);
		PlayerPrefs.SetFloat("Record1" , Record1);
		PlayerPrefs.SetFloat("Record2" , Record2);

        PlayerPrefs.Save();
	}
	
    public static void Delete()
    {
        PlayerPrefs.DeleteKey("Name0");
        PlayerPrefs.DeleteKey("Name1");
        PlayerPrefs.DeleteKey("Name2");

        PlayerPrefs.DeleteKey("Record0");
        PlayerPrefs.DeleteKey("Record1");
        PlayerPrefs.DeleteKey("Record2");

        PlayerPrefs.Save();
    }
}