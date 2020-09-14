using Assets.Scripts.InventoryScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManPlayerCur : MonoBehaviour
{
    private void OnMouseDown()
    {
        CharacterManager.Instance.ClickMan();
        GameSaveManager.instance.Man();

    }
}
