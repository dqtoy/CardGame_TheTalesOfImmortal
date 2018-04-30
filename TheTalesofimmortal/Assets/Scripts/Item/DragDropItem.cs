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

    public Image frameImage;

	private GameObject navigator;
	private Vector3 lastPos;
	private Vector3 presentPos;
	static TargetArrow arrow;
   

    void Start(){
        frameImage.gameObject.SetActive(false);
		arrow = GetComponentInParent<TargetArrow> ();
    }

	//1. 选中卡牌
	public void OnPointerDown(PointerEventData eventData) 
	{ 
		//1.1 初始化导航器
		navigator = new GameObject ();
		navigator.transform.SetParent (transform);
		navigator.transform.localPosition = Vector3.zero;
		Transform t = GetComponentInParent<Canvas> ().transform;
		navigator.transform.SetParent (t);
		//1.2 获取箭头的初始位置
		lastPos = navigator.transform.localPosition;
		arrow.On (lastPos);

		//1.3 卡牌被点按的特效
		frameImage.gameObject.SetActive(true);
	} 

	//2. 拖拽卡牌
    public void OnDrag(PointerEventData eventData) 
    { 
		//2.1 根据鼠标当前位置更新箭头位置
		Vector3 v = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		navigator.transform.position = v;
		presentPos = navigator.transform.localPosition;
		arrow.UpdateArrow (lastPos, presentPos);
    } 

	//3. 释放拖拽
    public void OnPointerUp(PointerEventData eventData) 
    { 
		Debug.Log ("PointerUp!");
        //3.1 释放卡牌，取消点按的特效
        frameImage.gameObject.SetActive(false);

		//3.2 检测落点的区域属性,并根据区域判定卡牌效果
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, -Vector2.up);
		if (hit.collider == null) {
			Debug.Log ("Cannot find collider!");
		} else {
			Debug.Log ("Collider Name " + hit.collider.gameObject.name);
            Action(hit.collider.gameObject.name);
		}

		//3.3 销毁导航器
		DestroyImmediate (navigator);
		arrow.Off ();
    }

    public virtual void Action(string param){
        Debug.Log(param);
    }

}