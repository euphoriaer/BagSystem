using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.InventoryScripts
{
    public class GameSaveManager : MonoBehaviour
    {
        public Inventory myInventory;
        public Dropdown Dropdown;

        public void SaveGame()
        {
            var json = JsonUtility.ToJson(myInventory);
            File.WriteAllText("C:/Users/17641/Desktop" + "/dateLit.txt", json);
            InventoryManager.RefreshItem();
        }

        public void LoadGame()
        {
            // MyBagManager.instance.ReadJsonData();
            Debug.Log("读取数据");
            string strJson = File.ReadAllText("C:/Users/17641/Desktop" + "/dateLit.txt");
            JsonUtility.FromJsonOverwrite(strJson, myInventory);
            InventoryManager.RefreshItem();
        }

        public void Weapon()
        {
            for (int i = 0; i < InventoryManager.instance.myBag.itemList.Count; i++)//检测物体的type，不是weapon就利用格子Slot置灰。
            {
                if (InventoryManager.instance.myBag.itemList[i] != null)
                {
                    if (InventoryManager.instance.myBag.itemList[i].type != "weapon")
                    {
                        Debug.Log("检测到不是weapon");

                        InventoryManager.instance.slots[i].GetComponent<Slot>().gray.SetActive(true);
                    }
                    else
                    {
                        InventoryManager.instance.slots[i].GetComponent<Slot>().gray.SetActive(false);
                    }
                }
            }
        }

        public void armor()
        {
            for (int i = 0; i < InventoryManager.instance.myBag.itemList.Count; i++)//检测物体的type，不是weapon就利用格子Slot置灰。
            {
                if (InventoryManager.instance.myBag.itemList[i] != null)
                {
                    if (InventoryManager.instance.myBag.itemList[i].type != "armor")
                    {
                        Debug.Log("检测到不是armor");
                        InventoryManager.instance.slots[i].GetComponent<Slot>().gray.SetActive(true);
                    }
                    else
                    {
                        InventoryManager.instance.slots[i].GetComponent<Slot>().gray.SetActive(false);
                    }
                }
            }
        }

        public void Restore()
        {
            for (int i = 0; i < InventoryManager.instance.myBag.itemList.Count; i++)//检测物体的type，不是weapon就利用格子Slot置灰。
            {
                if (InventoryManager.instance.myBag.itemList[i] != null)
                {
                    if (InventoryManager.instance.myBag.itemList[i].type != "resotre")
                    {
                        Debug.Log("检测到不是resotre");

                        InventoryManager.instance.slots[i].GetComponent<Slot>().gray.SetActive(true);
                    }
                    else
                    {
                        InventoryManager.instance.slots[i].GetComponent<Slot>().gray.SetActive(false);
                    }
                }
            }
        }

        public void All()
        {
            for (int i = 0; i < InventoryManager.instance.myBag.itemList.Count; i++)//检测物体的type，不是weapon就利用格子Slot置灰。
            {
                if (InventoryManager.instance.myBag.itemList[i] != null)
                {
                    InventoryManager.instance.slots[i].GetComponent<Slot>().gray.SetActive(false);
                }
            }
        }

        public void Sort(string one)
        {
            Inventory m_inventory = new Inventory();//创建一个临时背包
                                                    //创建一个临时物品
            int m = 0;
            for (int i = 0; i < InventoryManager.instance.myBag.itemList.Count; i++)
            {
                Debug.Log("开始排序");
                if (InventoryManager.instance.myBag.itemList[i] != null)
                {
                    if (InventoryManager.instance.myBag.itemList[i].type == one && m != i)
                    {
                        var temp = InventoryManager.instance.myBag.itemList[m];//将靠前的物品进行交换
                        InventoryManager.instance.myBag.itemList[m] = InventoryManager.instance.myBag.itemList[i];
                        InventoryManager.instance.myBag.itemList[i] = temp;
                        m++;
                    }
                }
            }

            //刷新物体
            InventoryManager.RefreshItem();
        }

        public void ItemSortDropDown()
        {
            if (Dropdown.value == 0)
            {
                All();
            }
            if (Dropdown.value == 1)
            {
                Sort("weapon");
                Weapon();
            }
            if (Dropdown.value == 2)
            {
                Sort("armor");
                armor();
            }
            if (Dropdown.value == 3)
            {
                Sort("resotre");
                Restore();
            }
            Debug.Log("DropDown");
        }
    }
}