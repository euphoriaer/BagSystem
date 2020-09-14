using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class MyItem{
    public int id;
    public string name;
    public string des;
    public string type;
    public string role;
    public String SpritePath;
}


[Serializable]
public enum ItemType
{
    weapon, armor, resotre
}
[Serializable]
public enum ItemProfession
{
    Thief, Man, All
}

