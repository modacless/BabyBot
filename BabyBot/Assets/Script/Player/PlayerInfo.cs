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
    public GameObject pistolModel;
    public GameObject[] machineGunModel;
    public GameObject[] launcherModel;
    public GameObject[] firePoints;
    [HideInInspector] public int choosenWeapon;

    #endregion

    private void Start()
    {
        Init();
        //DisplayWeaponModel(false, 0);

    }
    private void Init()
    {
        scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[0];
        actualHealth = startHealh;
    }


    private void DisplayWeaponModel()
    {
        if(actualWeapon == allWeapon[0])
        {
            if (numberOfUpgrade > 1) machineGunModel[numberOfUpgrade - 1].SetActive(false);
            else pistolModel.SetActive(false);
            machineGunModel[numberOfUpgrade].SetActive(true);
            actualWeapon.firePoint = firePoints[1 + numberOfUpgrade];
        }
        else
        {
            if(numberOfUpgrade > 1) launcherModel[numberOfUpgrade - 1].SetActive(false);
            else pistolModel.SetActive(false);
            launcherModel[numberOfUpgrade].SetActive(true);
            actualWeapon.firePoint = firePoints[5 + numberOfUpgrade];
        }
    }

    public void AddScore(float scoreToAdd)
    {
        //Debug.Log("Score");
        if (actualScoreUpgrade <= scoreNeedForNextUpgrade)
        {
            actualScoreUpgrade += scoreToAdd;
        }
       
        actualTotalScore += scoreToAdd;
    }

    public void TriggerWeaponUpgrade(InputAction.CallbackContext context)
    {
        if (actualScoreUpgrade >= scoreNeedForNextUpgrade && numberOfUpgrade < eachScoreNeedForUpgrade.Length-1)
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

                DisplayWeaponModel();

                numberOfUpgrade += 1;
                scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[numberOfUpgrade];

                // Trigger the weapon trade

            }
        }
    }


    public void DamagePlayer(int damage)
    {
        
        actualHealth -= damage;
        Debug.Log(actualHealth);

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
