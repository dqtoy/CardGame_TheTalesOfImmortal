//这个脚本用于自动调整手牌和已发牌区域的框大小
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;

public class CardsContainer : MonoBehaviour {
    

    public void AddCard(List<GameObject> container, GameObject card,Vector3 startPos){
        card.transform.position = startPos;

        //1. 1生成1个空物体占位置
        GameObject g = new GameObject();
        g.transform.SetParent(transform);
        Vector3 v = g.transform.position;

        //2. 将新卡移到新物体的位置
        container.Add(card);
        Squeeze(container.Count);
        card.transform.DOMove(v, 0.5f);

        //3. 移动完成将卡替代空物体
        StartCoroutine(WaitToAdd(g,container));
    }


    IEnumerator WaitToAdd(GameObject g,List<GameObject> container){
        yield return new WaitForSeconds(0.5f);

        //g = Cards[Cards.Count - 1];//这个可能存在风险
        DestroyImmediate(g);
        container[container.Count - 1].transform.SetParent(transform);
    }

//    public void MoveCard(int index){
//        Cards.RemoveAt(index);
//    }
//        

    void Squeeze(int count){
        float x = Mathf.Min(1120f, count * 150f);
        GetComponent<RectTransform>().sizeDelta = new Vector2(x, 256.5f);
    }

}
