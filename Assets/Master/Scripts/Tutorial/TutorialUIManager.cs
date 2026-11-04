using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject m_PanelDialogue;
    [SerializeField] private GameObject m_PanelItem;

    public static event System.Action OnDialogueTriggeredToPlayAudio;

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
            OnDialogueTriggeredToPlayAudio?.Invoke(); // play voice when dialogue panel is shown
        }

        if (m_PanelItem != null)
        {
            m_PanelItem.SetActive(true);
        }
    }

    private void HandleStepChanged(int currentStepIndex, TutorialStepData step)
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
