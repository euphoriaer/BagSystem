using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public GameObject thief;
    public GameObject man;
    public GameObject die;

    public Dropdown dropdown;
    private int dropCurrent = 0;
    private GameObject currentPlayer;

    public void ClickThief()
    {
        thief.AddComponent<PlayerMovement>().enabled = true;
        man.GetComponent<PlayerMovement>().enabled = false;
        currentPlayer = thief;
        dropdown.value = 0;
    }

    public void ClickMan()
    {
        thief.AddComponent<PlayerMovement>().enabled = false;
        man.GetComponent<PlayerMovement>().enabled = true;
        currentPlayer = man;
        dropdown.value = 1;
    }

    public void ClickDie()
    {
        thief.AddComponent<PlayerMovement>().enabled = false;
        man.GetComponent<PlayerMovement>().enabled = false;
        currentPlayer = die;
        dropdown.value = 2;
    }

    private void Update()
    {
        switch (dropdown.value)
        {
            case 0: currentPlayer = thief; break;
            case 1: currentPlayer = man; break;
            case 2: currentPlayer = die; break;

            default:
                break;
        }






    }
}