using UnityEngine;
using System;

public class CardController : MonoBehaviour
{
    public event Action<CardController> OnDestoryed;

    public CardFactory OriginFactory { get; set; }

    [SerializeField] private CardView cardView;
    
    private CardModel cardModel;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(cardView == null)
        {
            cardView = GetComponent<CardView>();
        }
    }
#endif

    public void Initialize(CardModel cardModel)
    {
        this.cardModel = cardModel;

        //Subscribe to event.
        cardModel.OnHealthChanged += cardView.SetHealth;
        cardView.OnHealthChangeAnimationFinished += cardModel.CheckDestroy;
        cardModel.OnDestroyed += DestroyCard;

        cardView.Initialize(cardModel);
    }

    public void ChangeHealth(int value)
    {
        cardModel.ChangeHealth(value);
    }

    public void ChangeLayer(int value)
    {
        cardView.SetLayer(value);
    }

    private void DestroyCard()
    {
        OnDestoryed?.Invoke(this);
        OriginFactory.Reclaim(this);
    }

    private void OnDestroy()
    {
        //Unsubscribe from event.
        cardModel.OnHealthChanged -= cardView.SetHealth;
        cardView.OnHealthChangeAnimationFinished -= cardModel.CheckDestroy;
        cardModel.OnDestroyed -= DestroyCard;
    }
}
