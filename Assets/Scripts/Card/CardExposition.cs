using System;

[Serializable]
public struct CardExposition
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public CardExposition(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
