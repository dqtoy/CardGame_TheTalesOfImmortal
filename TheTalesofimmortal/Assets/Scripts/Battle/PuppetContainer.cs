//这个脚本用于更新物池子的显示
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuppetContainer : MonoBehaviour {

    private List<GameObject> puppets = new List<GameObject>();

    public void InitShow(){
        ClearPuppets();
    }

    public void AddPuppet(GameObject g){
        puppets.Add(g);
        g.transform.SetParent(transform);
        g.transform.DOLocalMove(GetPos(puppets.Count - 1), 0.5f);
    }

    public void RemovePuppet(int index){
        if (index < puppets.Count-1)
            MoveForward(index);
        GameObject g = puppets[index];
        puppets.RemoveAt(index);
        DestroyImmediate(g);
    }

    void MoveForward(int index){
        for (int i = index + 1; i < puppets.Count; i++)
        {
            puppets[i].transform.DOLocalMove(GetPos(i - 1), 0.5f);
        }
    }

    void ClearPuppets(){
        if(puppets.Count>0)
            for (int i = puppets.Count - 1; i >= 0; i--)
            {
                GameObject g = puppets[i];
                puppets.RemoveAt(i);
                DestroyImmediate(g);
            }
        puppets = new List<GameObject>();
    }

    Vector3 GetPos(int index){
        float x = 450 - index * 180;
        return new Vector3(x,0,0);
    }

}
