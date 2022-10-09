using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public List<CardController> Cards { get => cards; }
    
    [SerializeField] private float upOffset = 0.001f;
    [SerializeField] private float horizontalDistanceBetweenCards = 1f;

    private List<CardController> cards;

    public void Initialize()
    {
        cards = new List<CardController>();
    }

    public void AddCard(CardController card)
    {
        if (!cards.Contains(card))
        {
            var cardIndex = GetCardIndex(card);
            cards.Insert(cardIndex, card);
            card.OnDestoryed += RemoveCard;

            card.transform.parent = transform;

            UpdateCardPositions();
        }
        else
        {
            Debug.LogError("You are trying to add an already added card to your drop");
        }
    }

    private void RemoveCard(CardController card)
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
            Debug.LogError("You are trying to remove an unadded card from your drop");
        }
    }

    private void UpdateCardPositions()
    {
        var cardCount = cards.Count;
        var startOffset = (1 - cardCount) / 2f;
        for (int i = 0; i < cardCount; i++)
        {
            var horizontalOffset = (startOffset + i) * horizontalDistanceBetweenCards;
            var currentPosition = transform.position + horizontalOffset * Vector3.right + upOffset * Vector3.up;
            cards[i].transform.position = currentPosition;
        }
    }

    private int GetCardIndex(CardController card)
    {
        var cardCount = cards.Count;
        var cardPosition = card.transform.position;
        for (int i = 0; i < cardCount; i++)
        {
            var currentCardPosition = cards[i].transform.position;
            if(cardPosition.x < currentCardPosition.x)
            {
                return i;
            }
        }

        return cardCount;
    }
}
