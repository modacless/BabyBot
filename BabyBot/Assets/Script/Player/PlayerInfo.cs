using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInfo : MonoBehaviour
{
    #region Variable

    [Header("Start Value")]
    public float startHealh;
    
    [Header("Score Need For Upgrade")]
    public float[] eachScoreNeedForUpgrade;
    [HideInInspector]public float scoreNeedForNextUpgrade;

    [HideInInspector]public float actualTotalScore;
    [HideInInspector]public float actualHealth;

    [HideInInspector]public float actualScoreUpgrade;

    [HideInInspector]public int numberOfUpgrade;


    public InputActionReference oui;
    #endregion


    private void Start()
    {
        Init();
    }
    private void Init()
    {
        scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[0];
        actualHealth = startHealh;
    }


    public void AddScore(float scoreToAdd)
    {
        if (actualScoreUpgrade <= scoreNeedForNextUpgrade)
        {
            actualScoreUpgrade += scoreToAdd;
        }
       
        actualTotalScore += scoreToAdd;
    }

    public void TriggerWeaponUpgrade(InputAction.CallbackContext context)
    {
        if (actualScoreUpgrade >= scoreNeedForNextUpgrade)
        {
            if (context.started)
            {
                Debug.Log("WEAPON UPGRADE !!");

                actualScoreUpgrade = 0;

                numberOfUpgrade += 1;
                scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[numberOfUpgrade];
            }
        }
    }

    public void DebugScore(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);

        if (context.started)
        {
            AddScore(1);
        }
    }




    public void DamagePlayer(int damage)
    {
        actualHealth -= damage;

        if (actualHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Player Dead");
    }


}
