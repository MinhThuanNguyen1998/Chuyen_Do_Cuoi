using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeSettings
{
    private const string BGM_KEY = "BgmVolume";
    private const string SFX_KEY = "SfxVolume";
    private const string VOICE_KEY = "VoiceVolume";
    private const float DEFAULT_VOLUME = 1f;

    public static float Bgm
    {
        get { return Get(BGM_KEY); }
        set { Set(BGM_KEY, value); }
    }

    public static float Sfx
    {
        get { return Get(SFX_KEY); }
        set { Set(SFX_KEY, value); }
    }

    public static float Voice
    {
        get { return Get(VOICE_KEY); }
        set { Set(VOICE_KEY, value); }
    }

    public static void Flush()
    {
        PlayerPrefs.Save();
    }

    private static float Get(string key)
    {
        return Mathf.Clamp01(PlayerPrefs.GetFloat(key, DEFAULT_VOLUME));
    }

    private static void Set(string key, float value)
    {
        PlayerPrefs.SetFloat(key, Mathf.Clamp01(value));
    }
}
