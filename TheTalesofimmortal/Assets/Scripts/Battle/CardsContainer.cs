//这个脚本用于自动调整手牌和已发牌区域的框大小
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class CardsContainer : MonoBehaviour {

    float cd = 0f;
    float movingTime = 0.6f;
    float xRightBorder = 0f;


    public void AddCard(List<GameObject> list, GameObject card,Vector3 startPos){
        Debug.Log("cd = " + cd);
        if (cd > 0)
            StartCoroutine(WaitForCD(list, card, startPos));
        else
        {
            Add(list, card, startPos);
            cd += movingTime;
        }
    }

    IEnumerator WaitForCD(List<GameObject> container, GameObject card,Vector3 startPos){
        yield return new WaitForSeconds(cd + 0.01f);
        AddCard(container, card, startPos);
    }


    void Add(List<GameObject> container, GameObject card,Vector3 startPos){
        card.transform.position = startPos;

        //1. 1生成1个空物体占位置
        GameObject g = Instantiate(Resources.Load("Prefab/emptycard")) as GameObject;
        g.transform.SetParent(transform);

        //2. 将新卡移到新物体的位置
        container.Add(card);
        AdjustBorder(container.Count);
        int index = container.Count - 1;//用于锁住当前卡牌序号，否则抽多张卡时会出错。

        card.transform.SetParent(transform.parent);
        card.transform.DOLocalMove(new Vector3(xRightBorder, 0, 0), movingTime);

        //3. 移动完成将卡替代空物体
        StartCoroutine(WaitToAdd(g,container,index));
    }


    IEnumerator WaitToAdd(GameObject g,List<GameObject> container,int index){
        yield return new WaitForSeconds(1f);

        //g = Cards[Cards.Count - 1];//这个可能存在风险
        DestroyImmediate(g);
        container[index].transform.SetParent(transform);
        cd -= movingTime;
    }

    void AdjustBorder(int count){
        float x = Mathf.Min(1120f, count * 150f);
        GetComponent<RectTransform>().sizeDelta = new Vector2(x, 256.5f);
        xRightBorder = x / 2f;
    }


    //等待处理
    public void MoveAway(List<GameObject> targetList, GameObject card,Vector3 endPos){
        targetList.Add(card);
    }
        



}
