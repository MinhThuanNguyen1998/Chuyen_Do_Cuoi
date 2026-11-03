using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject2D : MonoBehaviour
{
    [SerializeField] private int m_TutorialStepIndex = 0;
    [SerializeField] private bool m_HideSelfOnClicked = true;
    [SerializeField] private Button m_MyButton;

  public static event Action OnTutorialObjectClicked;
    

    private void OnEnable()
    {
        TutorialStepController.OnStepChanged += HandleStepChanged;
    }

    private void OnDisable()
    {
        TutorialStepController.OnStepChanged -= HandleStepChanged;
    }

    private void Awake()
    {
        if (m_MyButton != null)
        {
            m_MyButton.onClick.AddListener(OnButtonClicked);
        }
    }
    private void OnDestroy()
    {
        if (m_MyButton != null)
        {
            m_MyButton.onClick.RemoveListener(OnButtonClicked);
        }
    }
    private void HandleStepChanged(int currentStepIndex)
    {
        if (m_MyButton != null)
        {
            m_MyButton.interactable = (currentStepIndex == m_TutorialStepIndex);
        }
        if (currentStepIndex > m_TutorialStepIndex)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnButtonClicked()
    {
        if (m_HideSelfOnClicked)
        {
            gameObject.SetActive(false);
        }
        OnTutorialObjectClicked?.Invoke();
    }
}
