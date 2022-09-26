using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<CardController> Cards { get => cards; }

    [Header("DefaultOffset")]
    [SerializeField] private float viewCameraOffset = 10f;
    [SerializeField, Range(0.001f, 0.5f)] private float minViewBottomBorderOffset = 0.075f, maxViewBottomBorderOffset = 0.2f;
    
    [Header("SlerpOffset")]
    [SerializeField, Range(0f, 0.5f)] private float slerpHorizontalOffset = 0.3f;
    [SerializeField, Range(0f, 0.5f)] private float slerpverticalOffset = 0.3f;

    private int maxCardsCount;
    private Camera mainCamera;

    private List<CardController> cards;

    private Vector3 viewSlerpVerticalOffset;
    private Vector3 startViewSlerpOffset;
    private Vector3 endViewSlerpOffset;

    private float viewBottomBorderOffset;

    public void Initialize(int maxCardsCount, Camera mainCamera)
    {
        this.maxCardsCount = maxCardsCount;
        this.mainCamera = mainCamera;

        cards = new List<CardController>();

        var viewHorizontalOffset = new Vector3(slerpHorizontalOffset, 0f, 0f);
        viewSlerpVerticalOffset = new Vector3(0f, slerpverticalOffset, 0f);
        startViewSlerpOffset = viewSlerpVerticalOffset - viewHorizontalOffset;
        endViewSlerpOffset = viewSlerpVerticalOffset + viewHorizontalOffset;
    }

    public void AddCards(List<CardController> cards)
    {
        foreach (var card in cards)
        {
            AddCard(card);
        }
    }

    public void AddCard(CardController card)
    {
        if (!cards.Contains(card))
        {
            cards.Add(card);
            card.OnDestoryed += RemoveCard;

            card.transform.parent = transform;

            UpdateCardPositions();
        }
        else
        {
            Debug.LogError("You are trying to add an already added card to your hand");
        }
    }

    public void RemoveCard(CardController card)
    {
        if (cards.Contains(card))
        {
            cards.Remove(card);
            card.OnDestoryed -= RemoveCard;

            card.transform.parent = null;

            UpdateCardPositions();
        }
        else
        {
            Debug.LogError("You are trying to remove an unadded card from your hand");
        }
    }

    private void UpdateCardPositions()
    {
        var cardsCount = cards.Count;
        var handFullness = cardsCount / (float)maxCardsCount;

        viewBottomBorderOffset = 
            Mathf.Lerp(minViewBottomBorderOffset, maxViewBottomBorderOffset, handFullness);

        var progressStep = 1 / (float)(cardsCount + 1);
        for (int cardIndex = 0; cardIndex < cardsCount; cardIndex++)
        {
            CardController currentCard = cards[cardIndex];
            
            var cardProgress = (cardIndex + 1) * progressStep;
            UpdateCardPosition(currentCard.transform, cardProgress);
            
            currentCard.ChangeLayer(cardsCount - cardIndex);
        }
    }

    private void UpdateCardPosition(Transform cardTransform, float cardProgress)
    {
        //Position
        var viewPoint = new Vector3(0.5f, viewBottomBorderOffset, viewCameraOffset);
        var viewSlerpOffset = Vector3.Slerp(startViewSlerpOffset, endViewSlerpOffset, cardProgress);
        viewPoint += viewSlerpOffset - viewSlerpVerticalOffset;

        cardTransform.position = mainCamera.ViewportToWorldPoint(viewPoint);

        //Rotation
        var halfArcAngle = Vector3.Angle(startViewSlerpOffset, endViewSlerpOffset) / 2;
        var cardRotationAngle = Mathf.Lerp(-halfArcAngle, halfArcAngle, cardProgress);
        
        cardTransform.rotation = mainCamera.transform.rotation;
        cardTransform.Rotate(Vector3.back, cardRotationAngle);
    }
}
