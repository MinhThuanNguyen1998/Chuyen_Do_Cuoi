using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameService : MonoBehaviour
{
    private const string SAVE_KEY = "GAME_SAVE_DATA";
    private static SaveData m_CachedData;

    public static SaveData Data
    {
        get
        {
            if (m_CachedData == null)
            {
                Load();
            }
            return m_CachedData;
        }
    }
    public static void Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            m_CachedData = JsonUtility.FromJson<SaveData>(json);
        }

        if (m_CachedData == null)
        {
            m_CachedData = new SaveData();
        }
    }

    public static void Save()
    {
        if (m_CachedData == null) m_CachedData = new SaveData();

        string json = JsonUtility.ToJson(m_CachedData);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }
    public static void ClearData()
    {
        m_CachedData = new SaveData();
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
        Debug.Log("Game progress reset successfully!");
    }
}
