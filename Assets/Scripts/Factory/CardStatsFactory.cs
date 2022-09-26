using UnityEngine;

[CreateAssetMenu(fileName = "CardStatsFactory", menuName = "Factory/Card/CardStatsFactory")]
public class CardStatsFactory : Factory<CardStats>
{
    [SerializeField] private int minMana, maxMana;
    [SerializeField] private int minHealth, maxHealth;
    [SerializeField] private int minAttack, maxAttack;

#if UNITY_EDITOR
    private void OnValidate()
    {
        maxMana = maxMana < minMana ? minMana : maxMana;
        maxHealth = maxHealth < minHealth ? minHealth : maxHealth;
        maxAttack = maxAttack < minAttack ? minAttack : maxAttack;
    }
#endif

    public override CardStats GetRandom()
    {
        var randomMana = Random.Range(minMana, maxMana);
        var randomHealth = Random.Range(minHealth, maxHealth);
        var randomAttack = Random.Range(minAttack, maxAttack);

        var rendomCardStats = new CardStats(randomMana, randomHealth, randomAttack);
        return rendomCardStats;
    }
}
