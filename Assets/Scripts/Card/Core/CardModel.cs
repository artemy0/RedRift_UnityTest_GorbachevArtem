using UnityEngine;
using System;

public class CardModel : IStats
{
    //TODO I think the most correct aching construction would be: <previouseHelth, currentHealth, maxHealth>.
    public event Action<int, int> OnHealthChanged;
    public event Action OnDestroyed;

    public int Mana { get; private set; }
    public int Health { get; private set; }
    public int Attack { get; private set; }

    public CardExposition Exposition { get; private set; }
    public Texture2D Texture { get; private set; }

    public CardModel(CardStats stats, CardExposition exposition, Texture2D texture)
    {
        Mana = stats.Mana;
        Health = stats.Health;
        Attack = stats.Attack;

        Exposition = exposition;
        Texture = texture;
    }

    public void ChangeHealth(int health)
    {
        var previousHelth = Health;
        Health = health;

        OnHealthChanged?.Invoke(previousHelth, health);
    }

    public void CheckDestroy()
    {
        if(Health <= 0)
        {
            OnDestroyed?.Invoke();
        }
    }
}
