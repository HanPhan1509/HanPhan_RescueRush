using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsKey
{
    public static string LEVEL_KEY = "level_key";
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
}
