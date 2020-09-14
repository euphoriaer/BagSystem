using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New MyItem")]
public class MyitemS : ScriptableObject
{
    public int id;
    public string name;

    [TextArea]
    public string des;
    public string type;
    public string role;

    public Sprite itemImage;
}