using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsKey
{
    public static string LEVEL_KEY = "level_key";
    public static string SPEED_KEY = "speed_key";
}

public class DataGameSave : MonoBehaviour
{
    public static int GetLevel()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.LEVEL_KEY, 1);
    }

    public static void SetLevel(int level)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.LEVEL_KEY, level);
    }

    public static int GetSpeed()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey.SPEED_KEY, 10);
    }

    public static void SetSpeed(float speed)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKey.SPEED_KEY, speed);
    }
}
