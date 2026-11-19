using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TutorialStepController : MonoBehaviour
{
    [SerializeField] private TutorialDialogueSO m_Dialogue;
    [SerializeField] private List<TutorialStepData> m_SceneSteps = new List<TutorialStepData>();

    [Header("Shared Hand Pointer")]
    [SerializeField] private GameObject m_HandPointer;
    [SerializeField] private bool m_HandPointerEnabled = true;

    [SerializeField] private bool m_ReplayEventsOnResume = false;

    private int m_CurrentStepIndex;
    private string m_CurrentSceneName;
    private bool m_IsFinished;
    private bool m_PointerHiddenByClick;

    public static event Action<int, TutorialStepData> OnStepChanged;
    public static event Action<TutorialDialogueLine> OnDialogueLineChanged;
    public bool IsFinished => m_IsFinished;
    public int CurrentStepIndex => m_CurrentStepIndex;

    private void OnEnable()
    {
        InteractiveObject2D.OnTutorialObjectClicked += HandleObjectClicked;
        TutorialDialogueUI.OnButtonContinueClicked += CompleteCurrentStep;
    }
    private void OnDisable()
    {
        InteractiveObject2D.OnTutorialObjectClicked -= HandleObjectClicked;
        TutorialDialogueUI.OnButtonContinueClicked -= CompleteCurrentStep;
    }
    private void Start()
    {
        m_CurrentSceneName = gameObject.scene.name;
        int savedStep = Mathf.Max(0, SaveGameService.Data.GetStepForScene(m_CurrentSceneName));

        if (m_ReplayEventsOnResume)
        {
            int replayCount = Mathf.Min(savedStep, m_SceneSteps.Count);
            for (int i = 0; i < replayCount; i++) m_SceneSteps[i].onStepCompleted?.Invoke();
        }
        StartStep(savedStep);
    }
    private void HandleObjectClicked()
    {
        m_PointerHiddenByClick = true;
        RefreshHandPointer();
    }

    public void CompleteCurrentStep()
    {
        if (m_IsFinished) return;
        TriggerCompleteStep(m_CurrentStepIndex);
    }
    public void StartStep(int index)
    {
        if (index >= m_SceneSteps.Count)
        {
            FinishSceneTutorial();
            return;
        }
        m_CurrentStepIndex = index;
        m_PointerHiddenByClick = false;

        ApplyInteractiveStates(index);
        OnDialogueLineChanged?.Invoke(GetLine(index));
        OnStepChanged?.Invoke(index, m_SceneSteps[index]);

        SaveGameService.Data.SetStepForScene(m_CurrentSceneName, index);
        SaveGameService.Save();

        RefreshHandPointer();
    }
    public void TriggerCompleteStep(int stepIndex)
    {
        if (m_IsFinished) return;
        if (stepIndex != m_CurrentStepIndex) return;
        if (m_CurrentStepIndex < 0 || m_CurrentStepIndex >= m_SceneSteps.Count) return;

        m_SceneSteps[m_CurrentStepIndex].onStepCompleted?.Invoke();
        StartStep(m_CurrentStepIndex + 1);
    }
    private void FinishSceneTutorial()
    {
        m_IsFinished = true;
        m_CurrentStepIndex = m_SceneSteps.Count;

        RefreshHandPointer();
        ApplyInteractiveStates(m_SceneSteps.Count);
        OnDialogueLineChanged?.Invoke(null);
        OnStepChanged?.Invoke(m_SceneSteps.Count, null);
        SaveStep(m_SceneSteps.Count);
    }
    private void RefreshHandPointer()
    {
        if (m_HandPointer == null) return;

        bool hasStep = !m_IsFinished && m_CurrentStepIndex >= 0 && m_CurrentStepIndex < m_SceneSteps.Count;
        var step = hasStep ? m_SceneSteps[m_CurrentStepIndex] : null;
        bool show = m_HandPointerEnabled && step != null && step.showHandPointer && !m_PointerHiddenByClick;

        if (show && step.targetTransform != null)
        {
            var effect = m_HandPointer.GetComponent<HandPointerEffect>();
            if (effect != null) effect.MoveTo(step.targetTransform.position);
            else m_HandPointer.transform.position = step.targetTransform.position;
        }
        m_HandPointer.SetActive(show);
    }
    private void ApplyInteractiveStates(int currentIndex)
    {
        for (int i = 0; i < m_SceneSteps.Count; i++)
        {
            var obj = m_SceneSteps[i].interactiveObject;
            if (obj != null) obj.ApplyTutorialState(isCurrent: i == currentIndex, isPassed: i < currentIndex);
        }
    }

    private TutorialDialogueLine GetLine(int index) => m_Dialogue != null ? m_Dialogue.GetLine(index) : null;
    private void SaveStep(int step)
    {
        if (SaveGameService.Data.SetStepForScene(m_CurrentSceneName, step)) SaveGameService.Save();
    }
}
