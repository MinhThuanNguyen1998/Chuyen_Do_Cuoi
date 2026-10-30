using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TutorialStepController : MonoBehaviour
{
    [System.Serializable]
    public class LocalStep
    {
        public string stepName;
        public Transform targetTransform;
        public UnityEvent onStepCompleted;
    }

    [SerializeField] private List<LocalStep> m_SceneSteps = new List<LocalStep>();

    [Header("Shared Hand Pointer")]
    [SerializeField] private GameObject m_HandPointer;

    [SerializeField] private bool m_ReplayEventsOnResume = false;

    private int m_CurrentStepIndex;
    private string m_CurrentSceneName;
    private bool m_IsFinished;
   
    public static event Action<int> OnStepChanged;

    public bool IsFinished => m_IsFinished;
    public int CurrentStepIndex => m_CurrentStepIndex;
    private void OnEnable()
    {
        InteractiveObject2D.OnObjectClicked += HandleObjectClicked;
    }

    private void OnDisable()
    {
        InteractiveObject2D.OnObjectClicked -= HandleObjectClicked;
    }

    private void Start()
    {
        if (m_HandPointer != null) m_HandPointer.SetActive(false);

        m_CurrentSceneName = gameObject.scene.name;

        int savedStep = Mathf.Max(0, SaveGameService.Data.GetStepForScene(m_CurrentSceneName));

        if (m_ReplayEventsOnResume)
        {
            int replayCount = Mathf.Min(savedStep, m_SceneSteps.Count);
            for (int i = 0; i < replayCount; i++)
            {
                m_SceneSteps[i].onStepCompleted?.Invoke();
            }
        }

        StartStep(savedStep);
    }

    private void HandleObjectClicked(int clickedIndex)
    {
        TriggerCompleteStep(clickedIndex);
    }

    public void StartStep(int index)
    {
        if (index >= m_SceneSteps.Count)
        {
            FinishSceneTutorial();
            return;
        }

        m_CurrentStepIndex = index;
        LocalStep currentStep = m_SceneSteps[m_CurrentStepIndex];
        OnStepChanged?.Invoke(m_CurrentStepIndex);

        SaveGameService.Data.SetStepForScene(m_CurrentSceneName, m_CurrentStepIndex);
        SaveGameService.Save();

        if (m_HandPointer != null)
        {
            if (currentStep.targetTransform != null)
            {
                m_HandPointer.transform.position = currentStep.targetTransform.position;
            }
            m_HandPointer.SetActive(true);
        }
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

        if (m_HandPointer != null)
        {
            m_HandPointer.SetActive(false);
        }

        OnStepChanged?.Invoke(m_SceneSteps.Count); 
        SaveStep(m_SceneSteps.Count);
    }
   
    private void SaveStep(int step)
    {
        if (SaveGameService.Data.SetStepForScene(m_CurrentSceneName, step))
        {
            SaveGameService.Save();
        }
    }
}
