using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSpriteFactory", menuName = "Factory/Card/CardSpriteFactory")]
public class CardSpriteFactory : Factory<UniTask<Texture2D>>
{
    [SerializeField] private string textureUrl = "https://picsum.photos/200";

    public override UniTask<Texture2D> GetRandom()
    {
        return WebRequestHelper.GetTextureAsync(textureUrl);
    }
}
