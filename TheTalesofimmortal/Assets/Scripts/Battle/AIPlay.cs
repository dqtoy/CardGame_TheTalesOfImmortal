using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlay
{
    private Player attacker;
    private Player defender;
    private BattleManager manager;

    public AIPlay(Player attacker,Player defender,BattleManager manager){
        this.attacker = attacker;
        this.defender = defender;
		this.manager = manager;
    }

    public void PlayInOrder(){
        Debug.Log("开始AI出牌");
//		if (attacker.Info == PlayerInfo.Player)
//		{
//			manager.PlayerEndRound();
//		}
//		else
//		{
//			manager.EnemyEndRound();
//		}
//		return;
        int count = 0;

        while (attacker.Hands.Count > 0)
        {
            count++;
            if (count > 20)
            {
                Debug.Log("Bad Tail!");
                break;
                ;
            }

            int index = NextCard(attacker.Hands);
            if (index < 0)
                break;
            Target t = GetTarget(attacker.Hands[index]);
            manager.PlayCard(attacker, t, attacker.Hands[index]);

        }
    }
        

    int NextCard(List<Card> list){
        int index = 0;
        int pri = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (!manager.CanPlay(attacker,list[i].data))
                continue;
            if (list[i].data.PRI > pri)
            {
                pri = list[i].data.PRI;
                index = i;
            }
        }
        if (pri >= 0)
            return index;
        else
            return -1;
    }

    //默认目标 0敌人 1自己 2敌方召唤物   3我方召唤物  (召唤物默认选第1个)
    Target GetTarget(Card card){
        switch (card.data.DefaultTarget)
        {
            case 0:
                return defender as Target;
            case 1:
                return attacker as Target;
            case 2:
                return defender.Puppets[0];
            case 3:
                return attacker.Puppets[0];
            default:
                return null;
        }
    }
}


