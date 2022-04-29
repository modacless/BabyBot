using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInfo : MonoBehaviour
{
    #region Variable

    public int numPlayer;
    [Header("Player Stats")]
    public float maxHp;
    public float respawnTime;
    public bool playerInLife = true;
    
    [Header("Score Need For Upgrade")]
    public float[] eachScoreNeedForUpgrade;
    [HideInInspector]public float scoreNeedForNextUpgrade;

    [HideInInspector] public float actualTotalScore;
    [HideInInspector] public float actualHealth;

    [HideInInspector]public float actualScoreUpgrade;

    [HideInInspector]public int numberOfUpgrade;

    public Weapon actualWeapon;
    public Weapon[] allWeapon;
    public GameObject pistolModel;
    public GameObject[] machineGunModel;
    public GameObject[] launcherModel;
    public GameObject[] firePoints;
    [HideInInspector] public int choosenWeapon;

    private PlayerMovement playerMovementScript;
    private CapsuleCollider colliderSelf;

    #endregion

    private void Start()
    {
        Init();
        //DisplayWeaponModel(false, 0);
        playerMovementScript = GetComponent<PlayerMovement>();
        colliderSelf = GetComponent<CapsuleCollider>();

    }
    private void Init()
    {
        scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[0];
        actualHealth = maxHp;
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
                playerMovementScript.playerAnimationsScript.Reload(false, 1);
                numberOfUpgrade += 1;
                scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[numberOfUpgrade];

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
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        playerInLife = false;
        actualWeapon.enabled = false;
        playerMovementScript.enabled = false;
        colliderSelf.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        actualWeapon.ResetAmmoWeapon();

        yield return new WaitForSeconds(respawnTime);

        playerInLife = true;
        actualWeapon.enabled = true;
        playerMovementScript.enabled = true;
        colliderSelf.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);

        actualHealth = maxHp;
    }
}
