using UnityEngine;
using System.Collections;

namespace Assets.Scripts.InventoryScripts
{
    public class DesTest : MonoBehaviour
    {
        public GameObject desPanel;
        private Vector2 screenPosition;

        public float xPositionOffset;
        public float yPositionOffset;

        private void Awake()
        {
            desPanel = GameObject.Find("MyDesPanel");
        }
        public void Enter()
        {
            Debug.Log("进入物体");
            Invoke(nameof(MouseEnter), 0.5f);
        }

        private void MouseEnter()
        {
            Debug.Log("des面板");
            Debug.Log(Input.mousePosition.x);
            Debug.Log(Input.mousePosition.y);
            Debug.Log(Input.mousePosition.x - xPositionOffset);
            Debug.Log(Input.mousePosition.y - yPositionOffset);
            ////desPanel.transform.position = new Vector2(screenPosition.x - 42.5f, screenPosition.y - 81f);
            desPanel.transform.position = new Vector2(Input.mousePosition.x - xPositionOffset, Input.mousePosition.y - yPositionOffset);
        }
      
    }
}