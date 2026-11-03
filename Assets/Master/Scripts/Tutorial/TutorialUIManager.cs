using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
  
    [Header("UI Panels")]
    [SerializeField] private GameObject m_PanelDialogue;
    [SerializeField] private GameObject m_PanelItem;


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
        if (m_PanelDialogue != null) m_PanelDialogue.SetActive(true);
        if (m_PanelItem != null) m_PanelItem.SetActive(true);
    }
    private void HandleStepChanged(int currentStepIndex) // to handle the dialoue panel
    {
        if (m_PanelDialogue != null)
        {
            m_PanelDialogue.SetActive(false);
        }

    }
}
