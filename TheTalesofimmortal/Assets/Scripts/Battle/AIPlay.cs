using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlay
{
    private Player attacker;
    private Player defender;
    private BattleManager manager;
    private float cd = 0;
    private float waitingTime = 0.5f;
    public AIPlay(Player attacker,Player defender,BattleManager manager){
        this.attacker = attacker;
        this.defender = defender;
		this.manager = manager;
    }

    public void PlayInOrder(){
//		if (attacker.Info == PlayerInfo.Player)
//		{
//			manager.PlayerEndRound();
//		}
//		else
//		{
//			manager.EnemyEndRound();
//		}
//		return;
        int count=0;
        if (cd > 0)
            WaitingToPlay();
        else
        {
            while (attacker.Hands.Count > 0)
            {
                count++;
                if (count > 20)
                {
                    Debug.Log("Bad Tail!");
                    return;
                }

                int index = NextCard(attacker.Hands);
                if (index >= 0)
                {
                    Target t = GetTarget(attacker.Hands[index]);
                    manager.PlayCard(attacker, defender, attacker.Hands[index]);
                    //增加等待时间
                    cd += waitingTime;
                }
                else
                {
                    //enemy:如果没有可发的牌，则回合结束，清空所有卡
                    //player:如果没有可发的牌，则停止等待玩家行动
                    if (attacker.Info == PlayerInfo.Player)
                    {
                        manager.PlayerEndRound();
                    }
                    else
                    {
                        manager.EnemyEndRound();
                    }
                    break;
                }
            }
        }
    }

    IEnumerator WaitingToPlay(){
        yield return new WaitForSeconds(cd + 0.01f);
        PlayInOrder();
    }

    int NextCard(List<Card> list){
        List<int> PriList = new List<int>();
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
        if (pri > 0)
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


