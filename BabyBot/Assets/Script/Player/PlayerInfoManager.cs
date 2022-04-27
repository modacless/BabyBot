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

    public void AddPlayerScore(int wichPlayer, float scoreToAdd)
    {
        if (wichPlayer == 1)
        {
            infoPlayer1.AddScore(scoreToAdd);
        }
        else
        {
            infoPlayer2.AddScore(scoreToAdd);
        }
    }
}
