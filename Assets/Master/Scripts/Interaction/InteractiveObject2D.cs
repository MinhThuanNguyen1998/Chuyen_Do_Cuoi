using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractiveObject2D : MonoBehaviour
{
    [SerializeField] private bool m_HideSelfOnClicked = true;
    [SerializeField] private Button m_MyButton;

    [FormerlySerializedAs("m_TutorialStepIndex")]
    [SerializeField, HideInInspector] private int m_LegacyStepIndex;

    public int LegacyStepIndex => m_LegacyStepIndex;

    public static event Action OnTutorialObjectClicked;

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
    public void ApplyTutorialState(bool isCurrent, bool isPassed)
    {
        if (m_MyButton != null)
        {
            m_MyButton.interactable = isCurrent;
        }

        if (isPassed)
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
