using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHint : MonoBehaviour
{
    [SerializeField] private GameObject m_HintPanel;
    [SerializeField] private TextMeshProUGUI m_HintText;
    [SerializeField] private Button m_ButtonShow;

    private string m_CurrentHint;

    private void Awake()
    {
        if (m_ButtonShow != null) m_ButtonShow.onClick.AddListener(Show);

    }

    private void OnDestroy()
    {
        if (m_ButtonShow != null) m_ButtonShow.onClick.RemoveListener(Show);
    }

    private void OnEnable()
    {
        TutorialStepController.OnDialogueLineChanged += HandleLineChanged;
    }

    private void OnDisable()
    {
        TutorialStepController.OnDialogueLineChanged -= HandleLineChanged;
    }

    private void HandleLineChanged(TutorialDialogueLine line)
    {
        m_CurrentHint = line != null ? line.HintDescription : null;
        Hide();
    }

    public void Show()
    {
        if (string.IsNullOrWhiteSpace(m_CurrentHint)) return;

        if (m_HintText != null) m_HintText.text = m_CurrentHint;
        if (m_HintPanel != null) m_HintPanel.SetActive(true);
    }

    public void Hide()
    {
        if (m_HintPanel != null) m_HintPanel.SetActive(false);
    }
}
