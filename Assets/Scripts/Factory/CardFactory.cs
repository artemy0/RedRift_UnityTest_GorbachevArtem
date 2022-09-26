using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "CardFactory", menuName = "Factory/Card/CardFactory")]
public class CardFactory : Factory<UniTask<CardController>>
{
    [SerializeField] private CardModelFactory modelFactory;
    [Space]
    [SerializeField] private CardController cardPrefab;

    public async override UniTask<CardController> GetRandom()
    {
        var cardModel = await modelFactory.GetRandom();

        //TODO Try to add Object Pool
        var cardInstance = Instantiate(cardPrefab);
        cardInstance.OriginFactory = this;
        cardInstance.Initialize(cardModel);

        return cardInstance;
    }

    public void Reclaim(CardController card)
    {
        Destroy(card.gameObject);
    }
}
