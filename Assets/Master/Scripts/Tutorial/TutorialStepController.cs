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

    private int m_CurrentStepIndex = 0;
    private string m_CurrentSceneName;
    public static event Action<int> OnStepChanged;

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

        // Tự động lấy tên Scene hiện tại
        m_CurrentSceneName = SceneManager.GetActiveScene().name;

        // Đọc step từ SaveData thông qua SaveGameService
        int savedStep = SaveGameService.Data.GetStepForScene(m_CurrentSceneName);

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
            m_HandPointer.SetActive(true);
            if (currentStep.targetTransform != null)
            {
                m_HandPointer.transform.position = currentStep.targetTransform.position;
            }
        }
    }

    public void TriggerCompleteStep(int stepIndex)
    {
        if (stepIndex != m_CurrentStepIndex) return;

        LocalStep step = m_SceneSteps[m_CurrentStepIndex];

        step.onStepCompleted?.Invoke();

        m_CurrentStepIndex++;
        StartStep(m_CurrentStepIndex);
    }

    private void FinishSceneTutorial()
    {
        if (m_HandPointer != null)
        {
            m_HandPointer.SetActive(false);
        }

        SaveGameService.Data.SetStepForScene(m_CurrentSceneName, m_SceneSteps.Count);
        SaveGameService.Save();
    }
}
