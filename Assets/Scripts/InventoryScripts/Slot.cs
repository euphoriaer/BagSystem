using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;//空格ID 等于 物品ID
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    public GameObject itemInSlot;

    public GameObject gray;

    private float main_time = 0;
    public float click_time;
    private float two_click_time;
    private int count;

    public void ItemOnClicked() //两个思路，一个是button按第一下，在update里面，检测 input鼠标的第二下；一个是根据Time实时的时间，检测两次button按下的间隔
    {
        if (Time.time - main_time <= 0.3f)
        {
            Debug.Log("双击");
            InventoryManager.instance.myBag.itemList[slotID] = null;
            InventoryManager.RefreshItem();

        }
        else
        {
            Debug.Log("时间过长不认为双击");
            main_time = Time.time;
        }

        Debug.Log("使用物品");
        InventoryManager.UpdateItemInfo(slotInfo);
    }

    public void ItemOnClickedScropt()
    {
        InventoryManager.UpdateItemInfo(slotInfo);
    }
    public void SetupSlot(MyitemS item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        int m = 1;
        slotImage.sprite = item.itemImage;//物品的图片
        slotNum.text = m.ToString();//item.ToString(); //物品的数量
        slotInfo = item.des;
    }

    private void Update()
    {
    }
}