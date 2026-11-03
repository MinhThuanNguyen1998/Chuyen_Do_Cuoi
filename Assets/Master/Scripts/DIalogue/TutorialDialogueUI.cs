using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogueUI : MonoBehaviour
{
    [Header("Data & UI Components")]
    [SerializeField] private DialogueDataSO m_CurrentDialogueData;
    [SerializeField] private TextMeshProUGUI m_TextComponent;
    [SerializeField] private Image m_AvatarImage;

    [Header("Buttons")]
    [SerializeField] private Button m_ButtonContinue;

    private int m_CurrentStepIndex = -1;

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
        PlayAudioForCurrentStep();
    }

    private void OnDisable()
    {
        TutorialStepController.OnStepChanged -= HandleStepChanged;
    }

    private void OnContinueClicked()
    {
        OnButtonContinueClicked?.Invoke();
    }

    private void HandleStepChanged(int stepIndex)
    {
        m_CurrentStepIndex = stepIndex; // Lưu lại step hiện tại
        if (m_CurrentDialogueData == null) return;

        var targetDialogue = m_CurrentDialogueData.dialogueList.Find(x => x.stepIndex == stepIndex);

        if (targetDialogue.stepIndex == stepIndex)
        {
            if (m_TextComponent != null)
            {
                m_TextComponent.text = targetDialogue.textContent;
            }

            if (m_AvatarImage != null)
            {
                if (targetDialogue.avatarSprite != null)
                {
                    m_AvatarImage.gameObject.SetActive(true);
                    m_AvatarImage.sprite = targetDialogue.avatarSprite;
                }
                else
                {
                    m_AvatarImage.gameObject.SetActive(false);
                }
            }
        }
    }

    public void PlayAudioForCurrentStep()
    {
        if (m_CurrentDialogueData == null) return;

        var targetDialogue = m_CurrentDialogueData.dialogueList.Find(x => x.stepIndex == m_CurrentStepIndex);
        if (targetDialogue.stepIndex == m_CurrentStepIndex && targetDialogue.voiceAudio != null)
        {
            AudioManager.Instance.PlayVoice(targetDialogue.voiceAudio);
        }
    }
}
