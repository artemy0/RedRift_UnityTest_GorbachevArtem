using UnityEngine.UI;
using UnityEngine;
using System;

public class SessionUI : MonoBehaviour
{
    public event Action OnTakeDamageButtonClicked;
    public event Action OnRestartButtonClicked;

    [SerializeField] private Button takeDamageButton;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        takeDamageButton.onClick.AddListener(TakeDamageButtonClick);
        restartButton.onClick.AddListener(RestartButtonClick);
    }

    public void SetActiveTakeDamageButton(bool value)
    {
        takeDamageButton.gameObject.SetActive(value);
    }

    private void TakeDamageButtonClick()
    {
        OnTakeDamageButtonClicked?.Invoke();
    }

    private void RestartButtonClick()
    {
        OnRestartButtonClicked?.Invoke();
    }
}
