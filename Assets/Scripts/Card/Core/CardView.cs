using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

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
    [SerializeField] private AnimationCurve changeTextCureve;
    [SerializeField] private float changeTextTime;
    [Space]
    [SerializeField] private float resultDisplayDuration = 0.5f;
    [SerializeField] private Color health—hange—olor;

    private Coroutine helthChangeAnimation;

    private WaitForSeconds healthColorWaiter;
    private Color healthDefaultColor;

    public void Initialize(CardModel value)
    {
        mana.text = value.Mana.ToString();
        health.text = value.Health.ToString();
        attack.text = value.Attack.ToString();

        title.text = value.Exposition.Name;
        description.text = value.Exposition.Description;

        art.texture = value.Texture;

        healthColorWaiter = new WaitForSeconds(resultDisplayDuration);
        healthDefaultColor = healthBackground.color;
    }

    public void SetLayer(int value)
    {
        canvas.sortingOrder = value;
    }

    public void SetHealth(int currentHealth, int targetHealth)
    {
        if(helthChangeAnimation != null)
        {
            StopCoroutine(helthChangeAnimation);
            OnHealthChangeAnimationFinished?.Invoke();
        }
        helthChangeAnimation = StartCoroutine(AnimateHealthChange(currentHealth, targetHealth));
    }

    private IEnumerator AnimateHealthChange(int initHealth, int targetHealth)
    {
        healthBackground.color = health—hange—olor;
        
        if (initHealth != targetHealth)
        {
            var iterationSign = (int)Mathf.Sign(targetHealth - initHealth);
            for (int currentHealth = initHealth; currentHealth != targetHealth + iterationSign; currentHealth += iterationSign)
            {
                var waitTime = changeTextCureve.Evaluate((currentHealth - initHealth) / (float)(targetHealth - initHealth)) * changeTextTime;
                yield return new WaitForSeconds(waitTime);

                health.text = currentHealth.ToString();
            }

        }

        yield return healthColorWaiter;
        healthBackground.color = healthDefaultColor;

        helthChangeAnimation = null;
        OnHealthChangeAnimationFinished?.Invoke();
    }
}
