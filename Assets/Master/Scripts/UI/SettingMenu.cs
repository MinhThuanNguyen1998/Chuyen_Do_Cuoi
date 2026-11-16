using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [Header("UI Sliders")]
    [SerializeField] private Slider m_BgmSlider;
    [SerializeField] private Slider m_SfxSlider;
    [SerializeField] private Slider m_VoiceSlider;
    private void OnEnable()
    {
        Bind(m_BgmSlider, AudioVolumeSettings.Bgm, OnBgmSliderChanged);
        Bind(m_SfxSlider, AudioVolumeSettings.Sfx, OnSfxSliderChanged);
        Bind(m_VoiceSlider, AudioVolumeSettings.Voice, OnVoiceSliderChanged);
    }

    private void OnDisable()
    {
        Unbind(m_BgmSlider, OnBgmSliderChanged);
        Unbind(m_SfxSlider, OnSfxSliderChanged);
        Unbind(m_VoiceSlider, OnVoiceSliderChanged);

        AudioVolumeSettings.Flush();
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused) AudioVolumeSettings.Flush();
    }

    private void OnApplicationQuit()
    {
        AudioVolumeSettings.Flush();
    }

    public void OnBgmSliderChanged(float value)
    {
        AudioVolumeSettings.Bgm = value;
        if (AudioManager.HasInstance) AudioManager.Instance.SetBgmVolume(value);
    }

    public void OnSfxSliderChanged(float value)
    {
        AudioVolumeSettings.Sfx = value;
        if (AudioManager.HasInstance) AudioManager.Instance.SetSfxVolume(value);
    }

    public void OnVoiceSliderChanged(float value)
    {
        AudioVolumeSettings.Voice = value;
        if (AudioManager.HasInstance) AudioManager.Instance.SetVoiceVolume(value);
    }

    private void Bind(Slider slider, float savedValue, UnityAction<float> callback)
    {
        if (slider == null) return;

        slider.SetValueWithoutNotify(savedValue);
        slider.onValueChanged.RemoveListener(callback);
        slider.onValueChanged.AddListener(callback);

        callback(savedValue);
    }

    private void Unbind(Slider slider, UnityAction<float> callback)
    {
        if (slider == null) return;
        slider.onValueChanged.RemoveListener(callback);
    }
}
