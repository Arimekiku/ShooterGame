using UnityEngine;

public static class TotalStatsHandler
{
    public static void UpdateKey(string key, int amount)
    {
        if (PlayerPrefs.HasKey(key))
        {
            int cachedValue = PlayerPrefs.GetInt(key);
            PlayerPrefs.SetInt(key, cachedValue + amount);
        }
        else
        {
            PlayerPrefs.SetInt(key, amount);
        }
    }
}