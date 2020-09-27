using UnityEngine;
using System.Collections;

namespace Assets.Scripts.InventoryScripts
{
    public class MyItemOnworld : MonoBehaviour
    {

        public MyItemJson mydata;

        public void Start()
        {
           
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                AddNewItem();
                Destroy(this.gameObject);
            }
        }

        public void AddNewItem()
        {
           
        }

    }
}