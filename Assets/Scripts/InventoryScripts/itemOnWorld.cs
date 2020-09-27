using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWorld : MonoBehaviour
{
    public MyitemScript thisItem;
    public Inventory playerInventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BagFull();
            AddNewItem();
            Destroy(gameObject);
        }
    }
    public void BagFull()
    {
        int m_bagItem = 0;
        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i] == null)
            {
                Debug.Log("背包没满");
                
                return;
            }
            else
            {
                m_bagItem++;
            }
        }

        Debug.LogError("背包满了");

    }
    public void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisItem))//检测背包列表里面是否有同样的物品
        {
            // playerInventory.itemList.Add(thisItem);
            // InventoryManager.CreateNewItem(thisItem);
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    break;
                }
            }
        }

        InventoryManager.RefreshItem();
    }
}
