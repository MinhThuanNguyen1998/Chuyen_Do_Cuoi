using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
    [Header("Shared UI Panels")]
    [SerializeField] private GameObject m_PanelDialogue;

    [Tooltip("Old shared panel shown on every object click. Move it into each step's Panels list, then leave this empty.")]
    [SerializeField] private GameObject m_PanelItem;

    public static event System.Action OnDialogueTriggeredToPlayAudio;

    private TutorialStepData m_CurrentStep;
    private bool m_PendingShowDialogue;

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

    private void LateUpdate()
    {
        // Hiện dialogue "Immediately" ở cuối frame để TutorialDialogueUI kịp nạp câu thoại
        // của step mới trước khi phát voice (thứ tự gọi event giữa các listener không cố định).
        if (!m_PendingShowDialogue) return;
        m_PendingShowDialogue = false;
        ShowDialogue();
    }

    private void HandleObjectClicked()
    {
        if (m_CurrentStep != null &&
            m_CurrentStep.ResolvedDialogueTrigger == TutorialDialogueTrigger.OnObjectClicked)
        {
            ShowDialogue();
        }

        if (m_PanelItem != null)
        {
            m_PanelItem.SetActive(true);
        }

        SetPanels(m_CurrentStep, true);
    }

    private void HandleStepChanged(int currentStepIndex, TutorialStepData step)
    {
        m_PendingShowDialogue = false;
        SetPanels(m_CurrentStep, false);

        if (m_PanelDialogue != null)
        {
            m_PanelDialogue.SetActive(false);
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopVoice();
        }

        m_CurrentStep = step;
        if (step == null) return;

        if (step.interactiveObject == null)
        {
            SetPanels(step, true);
        }

        if (step.ResolvedDialogueTrigger == TutorialDialogueTrigger.Immediately)
        {
            m_PendingShowDialogue = true;
        }
    }

    private void ShowDialogue()
    {
        if (m_PanelDialogue == null) return;

        m_PanelDialogue.SetActive(true);
        OnDialogueTriggeredToPlayAudio?.Invoke(); // play voice when dialogue panel is shown
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
