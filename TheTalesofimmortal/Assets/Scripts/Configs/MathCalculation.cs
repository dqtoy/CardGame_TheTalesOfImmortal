using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathCalculation {

	public static int GetRandomValue(int top){
		return Random.Range (0, top);
	}

	public static int[] GetRandomValues(int top,int count){
		int[] values;
		if (count > top) {
			values = new int[top];
			for (int i = 0; i < top; i++)
				values [i] = i;
		} else {
			values = new int[count];
			List<int> l = new List<int> ();
			for (int i = 0; i < top; i++)
				l.Add (i);
			for (int i = 0; i < count; i++) {
				int n = GetRandomValue (l.Count);
				values [i] = l [n];
				l.RemoveAt (n);
			}
		}
		return values;
	}

	//衰减式增长，用于伤害减免叠加，闪避叠加等
	public static int PropDecayIncrease(int org,int inc){
//		return 100 - (100 - org) * (100 - inc) / 100;
		return Mathf.Min (100, org + inc);
	}

	public static int PropDecayDecrease(int org,int dec){
//		return 100 - (100 - org) * 100 / (100 - dec);
		return Mathf.Max (0, org - dec);
	}

	public static bool IsDodge(int rate){
		return Random.Range (0, 10000) < rate * 100;
	}
}
