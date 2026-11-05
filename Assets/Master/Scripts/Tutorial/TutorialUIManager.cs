using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
    [Header("Shared UI Panels")]
    [SerializeField] private GameObject m_PanelDialogue;

    public static event System.Action OnDialogueTriggeredToPlayAudio;

    private TutorialStepData m_CurrentStep;

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

        SetPanels(m_CurrentStep, true);
    }

    private void HandleStepChanged(int currentStepIndex, TutorialStepData step)
    {
        SetPanels(m_CurrentStep, false);

        if (m_PanelDialogue != null)
        {
            m_PanelDialogue.SetActive(false);
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopVoice();
        }

        m_CurrentStep = step; ;
        if (step != null && step.interactiveObject == null)
        {
            SetPanels(step, true);
        }
    }

    private static void SetPanels(TutorialStepData step, bool active)
    {
        if (step == null || step.panels == null) return;

        foreach (var panel in step.panels)
        {
            if (panel != null) panel.SetActive(active);
        }
    }
}
