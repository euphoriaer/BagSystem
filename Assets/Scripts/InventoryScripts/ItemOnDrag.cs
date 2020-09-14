using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector2 screenPosition;
    private GameObject CanvenRoot;
    public Transform originalParent;
    public Inventory myBag;
    private int currentItemID;//当前物品ID

    private Text des;
    public GameObject desPanel;
    private Color m_color;
    private Color m_colorDes;

    private void Awake()
    {
        CanvenRoot = GameObject.Find("Canvas");
        desPanel = CanvenRoot.transform.Find("MyDesPanel").gameObject;

        //m_color = desPanel.GetComponent<Image>().color;
        //m_colorDes = des.color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;//射线阻挡关闭
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);//输出鼠标当前位置下到第一个碰到到物体名字
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null) //丢到世界上返回原来的位置
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")//判断下面物体名字是：Item Image 那么互换位置
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent, false);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //itemList的物品存储位置改变
                var temp = myBag.itemList[currentItemID];
                myBag.itemList[currentItemID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡开启，不然无法再次选中移动的物品
                InventoryManager.RefreshItem();
                return;
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
            {
                ////否则直接挂在检测到到Slot下面
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

                //互换位置
                //transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent, false);
                //transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

                //eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                //eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);

                //itemList的物品存储位置改变
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.itemList[currentItemID];
                //自己放自己的问题
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID)
                {
                    myBag.itemList[currentItemID] = null;
                }

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryManager.RefreshItem();
                return;
            }
        }
        //其他情况，物品归位
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Debug.Log("物品归位");
        InventoryManager.RefreshItem();
        return;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("鼠标进入");
        desPanel = CanvenRoot.transform.Find("MyDesPanel").gameObject;
        Debug.Log("进入物体");
        Invoke(nameof(MouseEnter), 0.5f);

        //throw new System.NotImplementedException();
    }

    private void MouseEnter()
    {
        Debug.Log("移动des面板");
        desPanel.SetActive(true);
        desPanel.transform.position = new Vector2(Input.mousePosition.x + 100f, Input.mousePosition.y - 60f);//des面板偏移

        //desPanel.GetComponent<Image>().color = m_color;
        //des.color = m_colorDes;
        if (this.gameObject.transform.parent.GetComponent<Slot>()!=null)
        {
        this.gameObject.transform.parent.GetComponent<Slot>().ItemOnClickedScropt();

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        desPanel = CanvenRoot.transform.Find("MyDesPanel").gameObject;
        Debug.Log("退出物体");
        desPanel.SetActive(false);
    }
}