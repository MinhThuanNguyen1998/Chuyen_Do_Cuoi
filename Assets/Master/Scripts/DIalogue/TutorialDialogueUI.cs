using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogueUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI m_TextComponent;
    [SerializeField] private Image m_AvatarImage;

    [Header("Buttons")]
    [SerializeField] private Button m_ButtonContinue;

    private TutorialDialogueLine m_CurrentLine;
    public static event System.Action OnButtonContinueClicked;
    private void Awake()
    {
        if (m_ButtonContinue != null)
        {
            m_ButtonContinue.onClick.AddListener(OnContinueClicked);
        }
    }
    private void OnEnable()
    {
        TutorialStepController.OnStepChanged += HandleStepChanged;
        TutorialUIManager.OnDialogueTriggeredToPlayAudio += PlayAudioForCurrentStep;
    }
    private void OnDisable()
    {
        TutorialStepController.OnStepChanged -= HandleStepChanged;
        TutorialUIManager.OnDialogueTriggeredToPlayAudio -= PlayAudioForCurrentStep;
    }

    private void OnContinueClicked()
    {
        OnButtonContinueClicked?.Invoke();
    }

    private void HandleStepChanged(int stepIndex, TutorialStepData step)
    {
        m_CurrentLine = step?.dialogue;
        if (m_CurrentLine == null) return;

        if (m_TextComponent != null)
        {
            m_TextComponent.text = m_CurrentLine.textContent;
        }

        if (m_AvatarImage != null)
        {
            bool hasAvatar = m_CurrentLine.avatarSprite != null;
            m_AvatarImage.gameObject.SetActive(hasAvatar);
            if (hasAvatar) m_AvatarImage.sprite = m_CurrentLine.avatarSprite;
        }
    }

    public void PlayAudioForCurrentStep()
    {
        if (m_CurrentLine == null || m_CurrentLine.voiceAudio == null) return;
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayVoice(m_CurrentLine.voiceAudio);
        }
    }
}
