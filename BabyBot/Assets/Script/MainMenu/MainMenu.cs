using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject CreditMenu;
    public GameObject OptionsMenu;
    public TextMeshProUGUI NameInput;

    public void Play()
    {
        SceneManagement.instance.LoadScene(1);
    }
    public void ReturnToMenu()
    {
        SceneManagement.instance.LoadScene(0);
    }
    public void Exit()
    {
        SceneManagement.instance.Quit();
    }
    public void Options()
    {

        CreditMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void ExitOptions()
    {
        OptionsMenu.SetActive(false);
    }
    public void Credit()
    {
        OptionsMenu.SetActive(false);
        CreditMenu.SetActive(true);
    }
    public void ExitCredit()
    {
        CreditMenu.SetActive(false);
    }
    public void ChangeName()
    {
        //if (NameInput.text != "") PlayerManager.Instance.PlayerName = NameInput.text;
    }

    
}
