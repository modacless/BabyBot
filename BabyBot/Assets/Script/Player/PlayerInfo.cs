using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInfo : MonoBehaviour
{
    #region Variable

    [Header("Player Stats")]
    public float startHealh;
    public float maxHp;
    
    [Header("Score Need For Upgrade")]
    public float[] eachScoreNeedForUpgrade;
    [HideInInspector]public float scoreNeedForNextUpgrade;

    public float actualTotalScore;
    [HideInInspector]public float actualHealth;

    [HideInInspector]public float actualScoreUpgrade;

    [HideInInspector]public int numberOfUpgrade;

    public Weapon actualWeapon;
    public Weapon[] allWeapon;
    public int choosenWeapon;

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
        Debug.Log("Score");
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
                actualScoreUpgrade = 0;

                if(numberOfUpgrade == 0)
                {
                    actualWeapon.enabled = false;
                    actualWeapon = allWeapon[choosenWeapon];
                    actualWeapon.enabled = true;
                }
                else
                {
                    actualWeapon.UpgradeWeapon(numberOfUpgrade);
                }

                numberOfUpgrade += 1;
                scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[numberOfUpgrade];

                // Trigger the weapon trade

            }
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
