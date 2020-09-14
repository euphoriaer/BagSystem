using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string name;
    public int itemHeld;
    public Sprite itemImage;

    [TextArea]
    public string itemInfo;

    public bool equip;


}
