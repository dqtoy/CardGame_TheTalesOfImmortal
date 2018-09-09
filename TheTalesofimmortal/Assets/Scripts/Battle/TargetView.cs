using System.Collections.Generic;
using UnityEngine;
public class TargetView:MonoBehaviour
{

    public virtual void UpdateProfile(string path){}

    public virtual void UpdateHp(int hp,int maxHp){}

    public virtual void UpdateMp(int mp){}

    public virtual void UpdateAtk(int atk){}

    public virtual void UpdateBuffShow(){}

	public virtual void AddBuff(CardBuffType c,int layer){
	}

	public virtual void RemoveBuff(CardBuffType c){
	}
}

