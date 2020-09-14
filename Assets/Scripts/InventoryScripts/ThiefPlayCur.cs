using Assets.Scripts.InventoryScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefPlayCur : MonoBehaviour
{
    private void OnMouseDown()
    {
        CharacterManager.Instance.ClickThief();
        GameSaveManager.instance.Thief();

    }
}
