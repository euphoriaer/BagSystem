using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePlayerCur : MonoBehaviour
{
    private void OnMouseDown()
    {
        CharacterManager.Instance.ClickDie();
        GameSaveManager.instance.Die();

    }
}
