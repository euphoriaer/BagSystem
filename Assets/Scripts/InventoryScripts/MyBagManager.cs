using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MyBagManager : SingletnoAutoMono<MyBagManager>
{

    public Inventory myBag;
    // public Slot slotPrefab;

    public List<MyItem> myItems;//接收Json的数据
    public List<MyitemS> myItemScript;//创建可以放入背包的物体，将Json的数据赋予物体

    public Image imageTest;


    public void ReadJsonData()
    {
        myItems = JsonLoad();
        Debug.Log(myItems[0].name);
        if (myItems != null)
        {
            Debug.Log("Json数据存在");
            foreach (var item in myItems)
            {
                myItemScript.Add(CreatScriptItem(item));//创建物体并根据Json修改参数
            }
            for (int i = 0; i < myBag.itemList.Count&&i<myItemScript.Count; i++)//将创建的Item集合 放入到背包中
            {
                myBag.itemList[i] = myItemScript[i];
                //if (myBag.itemList[i] != null)
                //{
                //    Debug.LogError("!!!!!!!!!!!!!!!!!!!!!!!!");
                //    myBag.itemList[i] = myItemScript[i];
                //}

            }

        }
    }
    //将Json的值赋给新创建的MyitemS，然后放入item集合
    public MyitemS CreatScriptItem(MyItem myItem)
    {


        MyitemS myitemS = new MyitemS();
        myitemS.id = myItem.id;
        myitemS.name = myItem.name;
        myitemS.des = myItem.des;
        myitemS.type = myItem.type;
        myitemS.role = myItem.role;

        //加载图片，1.制成预制体，2.放入对应路径
        string mSpritePath = myItem.SpritePath;
        GameObject m_Image = Resources.Load(mSpritePath) as GameObject;
        myitemS.itemImage = m_Image.GetComponent<Image>().sprite;

        imageTest.sprite = m_Image.GetComponent<Image>().sprite;
        Debug.Log("数据给予完成");
        return myitemS;
    }


    public void Awake()
    {
        ReadJsonData();
    }



    public  void AddNewItem()
    {
        Debug.Log("添加新物体");
        JsonSave();
    }
    private void Start()
    {
        List<MyItem> myItems = new List<MyItem>();
        myItems=JsonLoad();
        Debug.Log(myItems[0].name);
    }
    public  void JsonSave()
    {
        
     
    }
    //
    public List<MyItem> JsonLoad()
    {
        string JsonLitLoad = File.ReadAllText("C:/Users/17641/Desktop" + "/MyItemData.txt");//可以使用try进行安全校验，读取Json字符串

        MyBagData myBagData = new MyBagData();
        myBagData = JsonMapper.ToObject<MyBagData>(JsonLitLoad);

        //用结构体接收 LitJsonData
        Debug.Log(JsonLitLoad);
        return myBagData.goods;
        

    }

}



