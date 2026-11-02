using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogueUI : MonoBehaviour
{
    [SerializeField] private DialogueDataSO m_CurrentDialogueData; 
    [SerializeField] private TextMeshProUGUI m_TextComponent;     
    [SerializeField] private Image m_AvatarImage;                          

    private void OnEnable()
    {
        TutorialStepController.OnStepChanged += HandleStepChanged;
    }

    private void OnDisable()
    {
        TutorialStepController.OnStepChanged -= HandleStepChanged;
    }

    private void HandleStepChanged(int stepIndex)
    {
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

            if (targetDialogue.voiceAudio != null)
            {
                //AudioManager.Instance.PlayVoice(targetDialogue.voiceAudio);
            }
        }
    }
}
