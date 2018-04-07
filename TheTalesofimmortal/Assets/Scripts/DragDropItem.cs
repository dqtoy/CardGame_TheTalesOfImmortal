/*卡牌的拖拽、按住操作流程
 1. 鼠标按下，
 2. 拖拽操作：将卡牌放入临时容器（层级最高），拖拽停止后放入目标容器。

*/

using UnityEngine; 
using System.Collections; 
using UnityEngine.UI; 
using UnityEngine.EventSystems; 
public class DragDropItem : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler 
{ 

    private GameObject heroHandContainer;
    private GameObject heroDropContainer;
    private GameObject playedPanel;
    private GameObject TempContainer;
    public Image frameImage;

    private Transform lastContainer;
    private CardData thisCard;
    private CardsHandler _cardsHandler;

    void Awake(){
        heroHandContainer = GameObject.FindWithTag("HeroHand");
        heroDropContainer = GameObject.FindWithTag("HeroDrop");
        playedPanel = GameObject.FindWithTag("HeroUsed");
        TempContainer = GameObject.FindWithTag("TempContainer");
    }
        

    void Start(){
        frameImage.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData) 
    { 
        //clampy用于限制拖拽位置的左右、上下限制
        Vector3 v = new Vector3(Mathf.Clamp( Input.mousePosition.x,77,1525), Mathf.Clamp(Input.mousePosition.y, heroHandContainer.transform.position.y, playedPanel.transform.position.y), 0);
        transform.position=v; 
    } 

    public void OnPointerDown(PointerEventData eventData) 
    { 
        //获取当前容器
        lastContainer = transform.parent;

        //卡牌被点按的特效
//        transform.localScale=new Vector3(0.9f,0.9f,0.9f); 
        frameImage.gameObject.SetActive(true);

        //将卡牌放入临时的最高层级的容器中，防止被遮挡
        transform.SetParent(TempContainer.transform);
    } 

    public void OnPointerUp(PointerEventData eventData) 
    { 
        //释放卡牌，取消点按的特效
        frameImage.gameObject.SetActive(false);

        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, -Vector2.up);

        //如果没有检测到碰撞，则把卡牌放回原容器
        if (hit.collider == null)
            transform.SetParent(lastContainer);

        //弃牌
        if (hit.collider.gameObject.tag == "HeroDrop")
        {
            transform.SetParent(heroDropContainer.transform);
        }
        //使用卡牌
        else if (hit.collider.gameObject.tag == "HeroUsed")
        {
            //如果可以使用
            if (GameData.CanCast(thisCard))
            {
                //卡牌效果
                _cardsHandler.PlayCard(this.gameObject);

                //将卡牌放入使用区
                transform.SetParent(playedPanel.transform);

                //更新卡牌区和使用区
                _cardsHandler.UpdateShow_PlayedCards();
                _cardsHandler.UpdateShow_HeroHand(); 
            }
            //如果不能使用，将卡牌放回原容器
            else
            {
                transform.SetParent(lastContainer);
            }

        }

    }
}