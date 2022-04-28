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

    private PlayerInfo playerInfo;
    private Color colorToUse;

    #endregion

    private void Start()
    {
        SetupPlayer();
        playerInfo = wichPlayer;
    }
    private void SetupPlayer()
    {
        /*if (wichPlayer == 1)
        {
            playerInfo = PlayerInfoManager.instance.infoPlayer1;
            colorToUse = colorPlayer1;
            fillImage.color = colorToUse;
        }
        else
        {
            playerInfo = PlayerInfoManager.instance.infoPlayer2;
            colorToUse = colorPlayer2;
            fillImage.color = colorToUse;
        }*/
    }


    private void Update()
    {
        slider.value = playerInfo.actualScoreUpgrade / playerInfo.scoreNeedForNextUpgrade;
    }

}
