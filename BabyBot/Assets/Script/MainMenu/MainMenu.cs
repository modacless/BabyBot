using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuObject;
    public GameObject CreditMenu;
    public GameObject OptionsMenu;
    public TextMeshProUGUI NameInput;

    private void Start()
    {
        CreditMenu.SetActive(false);
        mainMenuObject.SetActive(true);
    }

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
        mainMenuObject.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void ExitOptions()
    {
        OptionsMenu.SetActive(false);
    }
    public void Credit()
    {
        mainMenuObject.SetActive(false);
        OptionsMenu.SetActive(false);
        CreditMenu.SetActive(true);
    }
    public void ExitCredit()
    {
        mainMenuObject.SetActive(true);
        CreditMenu.SetActive(false);
    }
    public void ChangeName()
    {
        //if (NameInput.text != "") PlayerManager.Instance.PlayerName = NameInput.text;
    }

    
}
