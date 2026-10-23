using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI m_TitleText;
    [SerializeField] private TextMeshProUGUI m_MessageText;
    [SerializeField] private Image m_ContentImage;
    [SerializeField] private Button m_CloseButton;

    private System.Action m_OnCloseAction;

    private void Awake()
    {
        if (m_CloseButton != null)
        {
            m_CloseButton.onClick.AddListener(ClosePopup);
        }
    }

    public void Setup(PopupData data)
    {
        if (m_TitleText != null)
        {
            m_TitleText.text = data.title;
        }

        if (m_MessageText != null)
        {
            m_MessageText.text = data.message;
        }

        if (m_ContentImage != null)
        {
            if (data.iconSprite != null)
            {
                m_ContentImage.gameObject.SetActive(true);
                m_ContentImage.sprite = data.iconSprite;
            }
            else
            {
                m_ContentImage.gameObject.SetActive(false);
            }
        }

        m_OnCloseAction = data.onCloseCallback;
    }

    public void ClosePopup()
    {
        m_OnCloseAction?.Invoke();
        gameObject.SetActive(false);
    }
}
