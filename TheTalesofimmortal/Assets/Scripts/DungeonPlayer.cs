using System.Collections;
using System.Collections.Generic;

public class DungeonPlayer {
    public int Hp = 1;
    public int HpMax = 1;
    public int Mp = 1;
    public int Card = 3;

    public int Level;
    public School School;
    public string Name;
    public int Food;

    public List<Card> Library;

}

public enum School{
    ShaoLin,
    JinYiWei,
    TangMen,
    WuDu,
}
