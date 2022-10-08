using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class CardView : MonoBehaviour
{
    public event Action OnHealthChangeAnimationFinished;

    [Header("Reference")]
    [SerializeField] private Canvas canvas;
    [Space]
    [SerializeField] private TextMeshProUGUI mana;
    [SerializeField] private Image healthBackground;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI attack;
    [Space]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [Space]
    [SerializeField] private RawImage art;

    [Header("Animation")]
    [SerializeField] private Ease changeTextEase = Ease.InOutQuart;
    [SerializeField] private float changeTextDuration = 0.25f;
    [Space]
    [SerializeField] private float resultDisplayDuration = 0.5f;
    [SerializeField] private Color healthChangeColor;

    private Color healthDefaultColor;

    public void Initialize(CardModel value)
    {
        mana.text = value.Mana.ToString();
        health.text = value.Health.ToString();
        attack.text = value.Attack.ToString();

        title.text = value.Exposition.Name;
        description.text = value.Exposition.Description;

        art.texture = value.Texture;

        healthDefaultColor = healthBackground.color;
    }

    public void SetLayer(int value)
    {
        canvas.sortingOrder = value;
    }

    public void SetHealth(int currentHealth, int targetHealth)
    {
        var duration = Mathf.Abs(targetHealth - currentHealth) * changeTextDuration;

        var healthSettingSequence = DOTween.Sequence()
            .OnComplete(CompleteHealthSetting);
        healthSettingSequence.Append(
            healthBackground.DOColor(healthChangeColor, resultDisplayDuration));
        healthSettingSequence.Append(
            health.DOCounter(currentHealth, targetHealth, duration).SetEase(changeTextEase));
        healthSettingSequence.Append(
            healthBackground.DOColor(healthDefaultColor, resultDisplayDuration));
    }

    private void CompleteHealthSetting()
    {
        OnHealthChangeAnimationFinished?.Invoke();
    }
}
