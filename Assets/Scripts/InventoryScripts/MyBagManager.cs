using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 将Json数据与Scriptable联系起来
/// </summary>
public class MyBagManager : SingletnoAutoMono<MyBagManager>
{

    public Inventory myBag;
    // public Slot slotPrefab;

    public List<MyItemJson> myItemJson;//接收Json的数据
    public List<MyitemScript> myItemScript;//创建可以放入背包的物体，将Json的数据赋予物体

    public Image imageTest;


    public void ReadJsonData()
    {
        myItemJson = JsonLoad();
        Debug.Log(myItemJson[0].name);
        if (myItemJson != null)
        {
            Debug.Log("Json数据存在");
            foreach (var item in myItemJson)
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
    private MyitemScript CreatScriptItem(MyItemJson myItem)
    {


        MyitemScript myitemS = new MyitemScript();
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
        List<MyItemJson> myItems = new List<MyItemJson>();
        myItems=JsonLoad();
        Debug.Log(myItems[0].name);
    }
    //存储Json数据
    public  void JsonSave()
    {
        //TODO
     
    }
    //读取Json数据
    public List<MyItemJson> JsonLoad()
    {
        string JsonLitLoad = File.ReadAllText("C:/Users/17641/Desktop" + "/MyItemData.txt");//可以使用try进行安全校验，读取Json字符串

        MyBagData myBagData = new MyBagData();
        myBagData = JsonMapper.ToObject<MyBagData>(JsonLitLoad);

        //用结构体接收 LitJsonData
        Debug.Log(JsonLitLoad);
        return myBagData.goods;
        

    }

}



