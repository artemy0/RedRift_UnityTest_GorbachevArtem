using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private CardFactory cardFactory;
    [SerializeField] private CardStatsFactory statsFactory;

    //TODO Add card generation in the deck and smooth movement to the hand. Using Tween
    public async UniTask<CardController[]> GetRandomCards(int cardsCount)
    {
        var cardUniTasks = new List<UniTask<CardController>>(cardsCount);
        for (int i = 0; i < cardsCount; i++)
        {
            var cardUniTask = cardFactory.GetRandom();
            cardUniTasks.Add(cardUniTask);
        }

        return await cardUniTasks;
    }

    public List<CardStats> GetRandomStats(int statsCount)
    {
        var cardStatses = new List<CardStats>(statsCount);
        for (int i = 0; i < statsCount; i++)
        {
            var cardStats = statsFactory.GetRandom();
            cardStatses.Add(cardStats);
        }

        return cardStatses;
    }
}
