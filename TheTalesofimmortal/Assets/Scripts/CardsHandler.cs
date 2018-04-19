using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardsHandler : MonoBehaviour {

    public Transform HeroHand;
    public Transform PlayedCard;

    public GameObject cardPrefab;

    public Text waitingNumText_Hero;
    public Text leftNumText_Hero;
    public Text waitingNumText_Enemy;
    public Text leftNumText_Enemy;

//    private Vector3 pos;
//    private Vector3 ang;

    private ArrayList CardPool ;


    private List<CardData> leftCards_Hero;//余牌
    private List<CardData> handCards_Hero;//手牌
    private List<CardData> playedCards_Hero;//出场的牌
    private List<CardData> waitingCards_Hero;//等待牌

    private List<CardData> leftCards_Enemy;//余牌
    private List<CardData> handCards_Enemy;//手牌
    private List<CardData> playedCards_Enemy;//出场的牌
    private List<CardData> waitingCards_Enemy;//等待牌


    /// <summary>
    /// 初始化对象池
    /// </summary>
    public void InitCardsPool(Enemy e){

        CardPool = new ArrayList();

        leftCards_Hero = GameData.thisHero.Cards;
        handCards_Hero = new List<CardData>();
        playedCards_Hero = new List<CardData>();
        waitingCards_Hero = new List<CardData>();

        leftCards_Enemy = e.Deck;
        handCards_Enemy = new List<CardData>();
        playedCards_Enemy = new List<CardData>();
        waitingCards_Enemy = new List<CardData>();

        UpdateShow_Deck();
    }

    /// <summary>
    /// 发多张牌
    /// </summary>
    /// <param name="n">N.</param>
    public void AddCard(int n){
        StartCoroutine(WaitAndAddCard(n));
    }

    IEnumerator WaitAndAddCard(int n){
        for (int i = 0; i < n; i++)
        {
            if (i == 0)
                AddCard();
            else
            {
                yield return new WaitForSeconds(0.9f);
                AddCard();
            }
        }
    }


    /// <summary>
    /// 发1张牌
    /// </summary>
    public void AddCard(){

        //先(Monster根据权重)随机获得1张卡牌
        int newCardId = GetNewCard();
        GameObject o;
     

//        SetCardGameobject(o);

        //判断已经加载的卡牌是否>一共需要的卡牌数量
        if (CardPool.Count <= 0)
        {
            o = Instantiate(cardPrefab) as GameObject;
        }
        else
        {
            o = CardPool[0] as GameObject;
            CardPool.RemoveAt(0);
        }

        o.gameObject.name = newCardId.ToString();



        SetCardsToHand(o);
//        handCards_Hero.Add(LoadConfigs.CardDictionary[newCardId]);
    }

    int GetNewCard(){
        return 1;
    }
       

    /// <summary>
    /// 卡牌移动动画
    /// </summary>
    /// <param name="o">O.</param>
    void SetCardsToHand(GameObject o){
        o.SetActive(true);
        o.transform.SetParent(this.gameObject.transform);
        o.transform.localPosition = new Vector3(-800, -395, 0);
        o.transform.DOBlendableScaleBy(new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
        o.transform.DOMove(new Vector3(933, 525, 0), 0.5f);
        StartCoroutine(WaitAndJoin(o));
    }

    IEnumerator WaitAndJoin(GameObject o){
        yield return new WaitForSeconds(1f);
        o.transform.DOBlendableScaleBy(new Vector3(-0.5f, -0.5f, -0.5f), 0.2f);
        o.transform.DOMove(new Vector3(933f + handCards_Hero.Count * 30f, 145f, 0), 0.5f);

        yield return new WaitForSeconds(0.6f);
        o.transform.SetParent(HeroHand);
        o.transform.localPosition = Vector3.zero;
        o.transform.localScale = Vector3.one;
        UpdateShow_HeroHand();
    }




    /// <summary>
    /// 出牌的逻辑
    /// </summary>
    /// <param name="c">C.</param>
    public void PlayCard(GameObject o){
    //1.移除手牌并添加到场牌
        CardData c = new CardData();
        handCards_Hero.Remove(c);
        playedCards_Hero.Add(c);

    //2.计算消耗和结果
        GameData.CastCost(c);


    }

    /// <summary>
    /// 将已发牌重置到等待牌库
    /// </summary>
    public void SetHandToWaitingList(){
        for(int i=0;i<handCards_Hero.Count;i++){
            waitingCards_Hero.Add(handCards_Hero[i]);
            handCards_Hero.Remove(handCards_Hero[i]);
        }

        foreach (Transform t in PlayedCard.gameObject.GetComponentsInChildren<Transform>())
        {
            t.SetParent(Camera.main.transform);
            CardPool.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
    }



 


    public void UpdateShow_HeroHand(){

        HeroHand.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Min(1000, handCards_Hero.Count * 60f), 200f);
    }

    public void UpdateShow_PlayedCards(){
        int c = PlayedCard.childCount;
        DragDropItem[] d = PlayedCard.GetComponentsInChildren<DragDropItem>();
        for (int i=0;i<c;i++)
        { 
            Transform t = d[i].gameObject.transform;
            //如果有变动，则遍历，否则增加。这个效果还没有
            t.localPosition = GetUsedCardPosition(i, c);
        }
    }

    Vector3 GetUsedCardPosition(int thisIndex,int totalNum){
        float x;
        float y;

        int t = 1 + ((totalNum-1) / 9);
        int c = 1 + thisIndex / 9;
        int i = thisIndex % 9;

        if (t % 2 == 1 && c==t)
        {
            y = 0f;
        }else
        {
            if (c % 2 == 1)
                y = 120f;
            else
                y = -120f;
        }

        x = (i - 4) * 105f;

        Debug.Log(thisIndex + "," + totalNum + "\t=\t" + x + "," + y);
        return new Vector3(x, y, 0f);
    }

    public void UpdateShow_Deck(){
        waitingNumText_Hero.text = waitingCards_Hero.Count.ToString();
        leftNumText_Hero.text = leftCards_Hero.Count.ToString();
    }


}
