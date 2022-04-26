using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(1);
        }
    }

    public void AddScore(float scoreToAdd)
    {
        actualTotalScore += scoreToAdd;
        actualScoreUpgrade += scoreToAdd;

        if (actualScoreUpgrade >= scoreNeedForNextUpgrade)
        {
            TriggerWeaponUpgrade();
        }
    }
    private void TriggerWeaponUpgrade()
    {
        Debug.Log("WEAPON UPGRADE !!");

        actualScoreUpgrade = 0;

        numberOfUpgrade += 1;
        scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[numberOfUpgrade];
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
