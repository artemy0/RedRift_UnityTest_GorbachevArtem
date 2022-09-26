using System;

[Serializable]
public struct CardStats : IStats
{
    public int Mana { get; private set; }
    public int Health { get; private set; }
    public int Attack { get; private set; }

    public CardStats(int mana, int health, int attack)
    {
        Mana = mana;
        Health = health;
        Attack = attack;
    }
}
