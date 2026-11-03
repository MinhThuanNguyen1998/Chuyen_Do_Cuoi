using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{

    [Header("UI Panels")]
    [SerializeField] private GameObject m_PanelDialogue;
    [SerializeField] private GameObject m_PanelItem;

    [Header("References")]
    [SerializeField] private TutorialDialogueUI m_DialogueUI;

    private void OnEnable()
    {
        InteractiveObject2D.OnTutorialObjectClicked += HandleObjectClicked;
        TutorialStepController.OnStepChanged += HandleStepChanged;
    }

    private void OnDisable()
    {
        InteractiveObject2D.OnTutorialObjectClicked -= HandleObjectClicked;
        TutorialStepController.OnStepChanged -= HandleStepChanged;
    }

    private void HandleObjectClicked()
    {
        if (m_PanelDialogue != null)
        {
            m_PanelDialogue.SetActive(true);

            if (m_DialogueUI != null)
            {
                m_DialogueUI.PlayAudioForCurrentStep();
            }
        }

        if (m_PanelItem != null)
        {
            m_PanelItem.SetActive(true);
        }
    }

    private void HandleStepChanged(int currentStepIndex)
    {
        if (m_PanelDialogue != null)
        {
            m_PanelDialogue.SetActive(false);
        }
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopVoice(); 
        }

    }
}
