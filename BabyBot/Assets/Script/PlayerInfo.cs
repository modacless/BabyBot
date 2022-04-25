using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    #region Variable

    [Header("Start Value")]
    public float startHealh;
    public float startScore;
    
    [Header("Score Need For Upgrade")]
    public float[] eachScoreNeedForUpgrade;
    private float scoreNeedForNextUpgrade;

    private float actualScore;
    private float actualHealth;

    private int numberOfUpgrade;

    #endregion


    private void Start()
    {
        actualHealth = startHealh;
        actualScore = startScore;
    }


    public void AddScore(float scoreToAdd)
    {
        actualScore += scoreToAdd;

        if (actualScore >= scoreNeedForNextUpgrade)
        {
            TriggerWeaponUpgrade();
        }
    }
    private void TriggerWeaponUpgrade()
    {
        // Trigger Ui of upgrade

        numberOfUpgrade += 1;
        scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[numberOfUpgrade + 1];
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
