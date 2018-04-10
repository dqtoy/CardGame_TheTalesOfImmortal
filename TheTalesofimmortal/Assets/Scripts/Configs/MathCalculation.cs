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
}
