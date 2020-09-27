using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArraryTest
{
    public MyItemJson[] MydatasArrary;
}

public class JsonTest : MonoBehaviour
{
    public Sprite One;
    public Sprite Two;

    private void Start()
    {
        MyItemJson mydata2 = new MyItemJson();
        MyItemJson mydata = new MyItemJson();
        mydata.id = 1;
        mydata.name = "nameTest";
        mydata.des = "测试";
        mydata.type = "armor";
        mydata.role = "Man";

        mydata2.id = 2;
        mydata2.name = "nameTest2";
        mydata2.des = "测试2";
        mydata2.type = "weapon";
        mydata2.role = "All";

        ArraryTest arrary = new ArraryTest();
        arrary.MydatasArrary = new MyItemJson[] { mydata, mydata2 };

        MyBagData listTest = new MyBagData();
        listTest.goods = new List<MyItemJson>();
        listTest.goods.Add(mydata);
        listTest.goods.Add(mydata2);

        string str3 = JsonUtility.ToJson(listTest);
        Debug.Log(str3);
        MyBagData listTest2 = new MyBagData();
        listTest2 = JsonMapper.ToObject<MyBagData>(str3);
        Debug.Log(listTest2.goods[1].name);

        string str4 = JsonUtility.ToJson(arrary);
        Debug.Log(str4);

        ArraryTest arrary2 = new ArraryTest();
        arrary2 = JsonUtility.FromJson<ArraryTest>(str4);
        Debug.Log(arrary2.MydatasArrary[0].name);
    }
}