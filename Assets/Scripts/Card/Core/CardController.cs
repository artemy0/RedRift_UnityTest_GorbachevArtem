using UnityEngine;
using System;

public class CardController : MonoBehaviour
{
    public event Action<CardController> OnDroped;
    public event Action<CardController> OnDestoryed;

    public CardFactory OriginFactory { get; set; }

    [SerializeField] private CardView cardView;
    [SerializeField] private Interactable interactable;
    
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

        interactable.OnSelected += Select;
        interactable.OnReleased += Release;
        interactable.OnDroped += Drop;

        cardView.Initialize(cardModel);
        interactable.SetInteractableActive(true);
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
    
    private void Select()
    {
        cardView.SetSelecte(true);
    }
    private void Release()
    {
        cardView.SetSelecte(false);
    }
    private void Drop()
    {
        cardView.SetSelecte(false);
        interactable.SetInteractableActive(false);

        OnDroped?.Invoke(this);
    }

    private void OnDestroy()
    {
        //Unsubscribe from event.
        cardModel.OnHealthChanged -= cardView.SetHealth;
        cardView.OnHealthChangeAnimationFinished -= cardModel.CheckDestroy;
        cardModel.OnDestroyed -= DestroyCard;

        interactable.OnSelected -= Select;
        interactable.OnReleased -= Release;
        interactable.OnDroped -= Drop;
    }
}
