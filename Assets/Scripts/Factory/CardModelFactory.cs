using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "CardModelFactory", menuName = "Factory/Card/CardModelFactory")]
public class CardModelFactory : Factory<UniTask<CardModel>>
{
    [SerializeField] private CardStatsFactory statsFactory;
    [SerializeField] private CardExpositionFactory expositionFactory;
    [SerializeField] private CardSpriteFactory spriteFactory;

    public async override UniTask<CardModel> GetRandom()
    {
        var cardStats = statsFactory.GetRandom();
        var cardExposition = expositionFactory.GetRandom();
        var cardSprite = await spriteFactory.GetRandom();

        var cardModel = new CardModel(cardStats, cardExposition, cardSprite);
        return cardModel;
    }
}
