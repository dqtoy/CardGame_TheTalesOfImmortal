using System.Collections.Generic;
public class Card {

    public int Id;
    public string Name;
    public string Description;
    public int ActionCost;
    public int Price;
    public int ManaCost;

    public int Level;
    public int MaxLevel;
    public int DecayTo;
    public int Tier;

    public Dictionary<int,int> Prop;
    //0 Physical Damage
    //1 Fire Damage
    //2 Water Damage
    //3 Earth Damage
    //4 Lightening Damage
    //5 Pierce Damage
    //6 Pray
}
