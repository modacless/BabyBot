using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    #region Variable 

    [Header("Wich player score to display")]
    public PlayerInfo wichPlayer;

    [Header ("Player 1")]
    public Color colorPlayer1;

    [Header ("Player 2")]
    public Color colorPlayer2;

    [Header ("Unity Setup")]
    public Slider slider;
    public Image fillImage;
    public GameObject upgradeMessage;

    private PlayerInfo playerInfo;

    #endregion

    private void Start()
    {
        playerInfo = wichPlayer;
        upgradeMessage.SetActive(false);
    }
 
    private void Update()
    {
        slider.value = playerInfo.actualScoreUpgrade / playerInfo.scoreNeedForNextUpgrade;

        if (playerInfo.actualScoreUpgrade >= playerInfo.scoreNeedForNextUpgrade)
        {
            upgradeMessage.SetActive(true);
        }
        else
        {
            upgradeMessage.SetActive(false);
        }
    }

}
