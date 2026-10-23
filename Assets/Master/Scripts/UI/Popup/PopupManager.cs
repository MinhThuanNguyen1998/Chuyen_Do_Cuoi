using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PopupView m_PopupPrefab;
    [SerializeField] private Transform m_CanvasTransform;
    private PopupView m_CurrentPopupInstance;
    public void ShowPopup(PopupData data)
    {

        if (m_CurrentPopupInstance == null)
        {
            m_CurrentPopupInstance = Instantiate(m_PopupPrefab);
            DontDestroyOnLoad(m_CurrentPopupInstance.gameObject);
        }
        else
        {
            m_CurrentPopupInstance.gameObject.SetActive(true);
        }
        m_CurrentPopupInstance.Setup(data);
    }
}


