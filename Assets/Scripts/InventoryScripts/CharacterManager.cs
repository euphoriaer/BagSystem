using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : SingletnoAutoMono<CharacterManager>
{
    public GameObject thief;
    public GameObject man;
    public GameObject die;


    

    public Dropdown dropdown;
    private int dropCurrent = 0;
    private GameObject currentPlayer;
    /// <summary>
    /// 点击角色，高亮，开启移动脚本，对物品进行判断置灰
    /// </summary>
    public void ClickThief()
    {
        thief.GetComponent<PlayerMovement>().enabled = true;
        thief.GetComponent<_2dxFX_Outline>().enabled = true;

        man.GetComponent<PlayerMovement>().enabled = false;
        man.GetComponent<_2dxFX_Outline>().enabled = false;

        die.GetComponent<_2dxFX_Outline>().enabled = false;
        currentPlayer = thief;

      

        dropdown.value = 0;
    }

    public void ClickMan()
    {
        thief.GetComponent<PlayerMovement>().enabled = false;
        thief.GetComponent<_2dxFX_Outline>().enabled = false;

        man.GetComponent<PlayerMovement>().enabled = true;
        man.GetComponent<_2dxFX_Outline>().enabled = true;

        die.GetComponent<_2dxFX_Outline>().enabled = false;
        
        

        currentPlayer = man;

        dropdown.value = 1;
    }

    public void ClickDie()
    {
        thief.GetComponent<PlayerMovement>().enabled = false;
        thief.GetComponent<_2dxFX_Outline>().enabled = false;

        man.GetComponent<PlayerMovement>().enabled = false;
        man.GetComponent<_2dxFX_Outline>().enabled = false;

        die.GetComponent<_2dxFX_Outline>().enabled = true;

       

        currentPlayer = die;

        dropdown.value = 2;
    }

    private void Update()
    {
        switch (dropdown.value)
        {
            case 0: currentPlayer = thief; ClickThief(); break;
            case 1: currentPlayer = man; ClickMan(); break;
            case 2: currentPlayer = die; ClickDie(); break;

            default:
                break;
        }

    }
}