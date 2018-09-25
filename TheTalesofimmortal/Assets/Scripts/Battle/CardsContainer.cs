//这个脚本用于自动调整手牌和已发牌区域的框大小
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class CardsContainer : MonoBehaviour {

    public void AddCard(List<GameObject> list, GameObject card,Vector3 startPos){
        list.Add(card);
        AdjustBorder(list.Count);
        card.transform.SetParent(transform);
    }




//
//    float cd = 0f;
//    float movingTime = 0.6f;
//    float xRightBorder = 0f;
//
//    /// <summary>
//    /// 添加卡，用于发牌、出牌
//    /// </summary>
//    /// <param name="list">List.</param>
//    /// <param name="card">Card.</param>
//    /// <param name="startPos">Start position.</param>
//    public void AddCard(List<GameObject> list, GameObject card,Vector3 startPos){
//        if (cd > 0)
//            StartCoroutine(WaitForCD(list, card, startPos));
//        else
//        {
//            Add(list, card, startPos);
//            cd += movingTime;
              //问题可能出在没有清空CD

//        }
//    }
//
//    IEnumerator WaitForCD(List<GameObject> container, GameObject card,Vector3 startPos){
//        yield return new WaitForSeconds(cd + 0.01f);
//        AddCard(container, card, startPos);
//    }
//
//
//    void Add(List<GameObject> container, GameObject card,Vector3 startPos){
//        //设定卡牌初始状态
//        Transform p = GetComponentInParent<Canvas>().transform;
//        card.transform.SetParent(p);
//        card.transform.localPosition = startPos;
//
////        1. 1生成1个空物体占位置
//        GameObject g = Instantiate(Resources.Load("Prefab/emptycard")) as GameObject;
//        g.transform.SetParent(transform);
//
//        //2. 将新卡移到新物体的位置
//        container.Add(card);
//        AdjustBorder(container.Count);
//        int index = container.Count - 1;//用于锁住当前卡牌序号，否则抽多张卡时会出错。
//
//        card.transform.SetParent(transform.parent);
//        card.transform.DOLocalMove(new Vector3(xRightBorder, 0, 0), movingTime);
//
//        //3. 移动完成将卡替代空物体
//        StartCoroutine(WaitToAdd(g,container,index));
//    }
//
//
//    IEnumerator WaitToAdd(GameObject g, List<GameObject> targetList,int index){
//        yield return new WaitForSeconds(movingTime);
//
//        //g = Cards[Cards.Count - 1];//这个可能存在风险
//        DestroyImmediate(g);
//        Debug.Log(" index = " + index + ", count = " + targetList.Count);
//        targetList[index].transform.SetParent(transform);
//        cd -= movingTime;
//    }

    void AdjustBorder(int count){
        float x = Mathf.Min(1120f, count * 150f);
        GetComponent<RectTransform>().sizeDelta = new Vector2(x, 256.5f);
//        xRightBorder = x / 2f;
    }


    public void MoveAway(List<GameObject> targetList, GameObject card,Vector3 endPos){
        Transform p = GetComponentInParent<Canvas>().transform;
        card.transform.SetParent(p);
        targetList.Add(card);
        card.SetActive(false);
    }






    /// <summary>
    /// 移除卡，用于弃牌、清除场上卡牌
    /// </summary>
    /// <param name="targetList">Target list.</param>
    /// <param name="card">Card.</param>
    /// <param name="endPos">End position.</param>
//    public void MoveAway(List<GameObject> targetList, GameObject card,Vector3 endPos){
//        Transform p = GetComponentInParent<Canvas>().transform;
//        card.transform.SetParent(p);
//        card.transform.DOLocalMove(endPos, movingTime);
//        StartCoroutine(WaitToAdd(card, targetList));
//    }
//        
//    IEnumerator WaitToAdd(GameObject g,List<GameObject> targetList){
//        yield return new WaitForSeconds(movingTime);
//        targetList.Add(g);
//		g.SetActive (false);
//    }
//

}
