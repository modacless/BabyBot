using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    #region Variable

    public static PlayerInfoManager instance;

    public PlayerInfo infoPlayer1;
    public PlayerInfo infoPlayer2;

    #endregion

    private void Awake()
    {
        #region Setup manager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one PlayerInfoManager in the scene");
        }
        #endregion  

        
        // Have to change this after !!!!!
        //infoPlayer1 = GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerInfo>();
        //infoPlayer2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<PlayerInfo>();
    }

    public void Start()
    {
        infoPlayer1.numPlayer = 1;
        infoPlayer2.numPlayer = 2;
        int randomWeapon = Random.Range(0, 2);

        if(randomWeapon == 1)
        {
            infoPlayer1.choosenWeapon = 1;
            infoPlayer2.choosenWeapon = 0;
        }
        else
        {
            infoPlayer1.choosenWeapon = 0;
            infoPlayer2.choosenWeapon = 1;
        }

    }

    public void AddPlayerScore(PlayerInfo wichPlayer, float scoreToAdd)
    {
        wichPlayer.AddScore(scoreToAdd);
    }
}
