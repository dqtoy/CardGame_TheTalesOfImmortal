using System.Collections;
using System.Collections.Generic;

public class ChangQuan : PhysicalAtkCard {

	public ChangQuan(){
		Name="长拳";
		Description="初阶武术招式，造成1点物理伤害";
		Price = 1;
	}
	//是不是考虑把释放放到Player脚本里
	public override void Play(Player me,Player enemy){
		//CheckTrigger(base.Name) bool
		//PlayEffect
		enemy.Attack(1,AtkType);
	}
}
