using UnityEngine;

[CreateAssetMenu(fileName = "CardExpositionFactory", menuName = "Factory/Card/CardExpositionFactory")]
public class CardExpositionFactory : Factory<CardExposition>
{
    [SerializeField] private int minNameLength, maxNameLength;
    [SerializeField] private int minDescriptionLength, maxDescriptionLength;

#if UNITY_EDITOR
    private void OnValidate()
    {
        maxNameLength = maxNameLength < minNameLength ? minNameLength : maxNameLength;
        maxDescriptionLength = maxDescriptionLength < minDescriptionLength 
            ? minDescriptionLength 
            : maxDescriptionLength;
    }
#endif

    public override CardExposition GetRandom()
    {
        var nameLength = Random.Range(minNameLength, maxNameLength);
        var name = StringGenerationHelper.GetRandomString(nameLength);

        var descriptionLength = Random.Range(minDescriptionLength, maxDescriptionLength);
        var description = StringGenerationHelper.GetRandomString(descriptionLength, true);

        var cardExposition = new CardExposition(name, description);
        return cardExposition;
    }
}
