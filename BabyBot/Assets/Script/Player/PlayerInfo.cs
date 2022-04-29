using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    #region Variable

    public int numPlayer;
    [Header("Player Stats")]
    public float maxHp;
    //public float respawnTime;
    public float timeForRevive;
    [HideInInspector] public float currentReviveTime = 0;
    [HideInInspector] public bool isReviving = false;
    [HideInInspector] public bool isPressingRevive = false;
    public bool playerInLife = true;

    [Header("UI")]
    public GameObject reviveUI;
    private Image cooldownUi;

    [Header("Score Need For Upgrade")]
    public float[] eachScoreNeedForUpgrade;
    [HideInInspector]public float scoreNeedForNextUpgrade;

    [HideInInspector] public float actualTotalScore;
    [HideInInspector] public float actualHealth;

    [HideInInspector]public float actualScoreUpgrade;

    public int numberOfUpgrade;

    public Weapon actualWeapon;
    public Weapon[] allWeapon;
    public GameObject pistolModel;
    public GameObject[] machineGunModel;
    public GameObject[] launcherModel;
    public GameObject[] firePoints;
    [HideInInspector] public int choosenWeapon;

    private PlayerMovement playerMovementScript;
    private CapsuleCollider colliderSelf;
    private Rigidbody rb;

    #endregion

    private void Start()
    {
        Init();
        //DisplayWeaponModel(false, 0);
        playerMovementScript = GetComponent<PlayerMovement>();
        colliderSelf = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        cooldownUi = reviveUI.transform.GetChild(3).GetComponent<Image>();
        cooldownUi.gameObject.SetActive(false);

        GetComponent<PlayerInput>().actions["Revive"].started += Revive;
        GetComponent<PlayerInput>().actions["Revive"].canceled += Revive;

    }
    private void Init()
    {
        scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[0];
        actualHealth = maxHp;
    }

    private void Update()
    {
        if (!playerInLife)
        {
            WaitForRevive();
        }
    }

    private void DisplayWeaponModel()
    {
        if(actualWeapon == allWeapon[0])
        {
            if (numberOfUpgrade > 0) machineGunModel[numberOfUpgrade - 1].SetActive(false);
            else pistolModel.SetActive(false);
            machineGunModel[numberOfUpgrade].SetActive(true);
            actualWeapon.firePoint = firePoints[1 + numberOfUpgrade];
        }
        else
        {
            if(numberOfUpgrade > 0) launcherModel[numberOfUpgrade - 1].SetActive(false);
            else pistolModel.SetActive(false);
            launcherModel[numberOfUpgrade].SetActive(true);
            actualWeapon.firePoint = firePoints[5 + numberOfUpgrade];
        }

        actualWeapon.playerMovementScript = playerMovementScript;
        actualWeapon.ResetAmmoWeapon();
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
        if (actualScoreUpgrade >= scoreNeedForNextUpgrade && numberOfUpgrade < eachScoreNeedForUpgrade.Length)
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
                    actualWeapon.UpgradeWeapon(numberOfUpgrade-1);
                }

                DisplayWeaponModel();
                
                numberOfUpgrade += 1;
                if (numberOfUpgrade < eachScoreNeedForUpgrade.Length - 1) scoreNeedForNextUpgrade = eachScoreNeedForUpgrade[numberOfUpgrade];
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

    [ContextMenu("Die")]
    private void Die()
    {
        PlayerDead(true);
    }

    public virtual void Revive(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressingRevive = true;
        }
        if (context.canceled)
        {
            isPressingRevive = false;
        }
    }

    private void WaitForRevive()
    {
        reviveUI.SetActive(true);

        if (isReviving)
        {
            currentReviveTime += Time.deltaTime;
            cooldownUi.gameObject.SetActive(true);
        }
        else
        {
            currentReviveTime = 0;
            cooldownUi.gameObject.SetActive(false);
        }

        cooldownUi.fillAmount = currentReviveTime / timeForRevive;

        if (currentReviveTime >= timeForRevive)
        {
            PlayerDead(false);
            reviveUI.SetActive(false);
            cooldownUi.gameObject.SetActive(false);
        }
    }

    private void PlayerDead(bool enable)
    {
        switch (enable)
        {
            case true:
                playerInLife = false;
                actualHealth = maxHp;
                actualWeapon.enabled = false;
                playerMovementScript.enabled = false;
                colliderSelf.enabled = false;
                rb.constraints = RigidbodyConstraints.FreezePosition;
                actualWeapon.ResetAmmoWeapon();
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 80));
                transform.position += new Vector3(1.5f, 0, 0);
                break;
            case false:
                playerInLife = true;
                actualWeapon.enabled = true;
                playerMovementScript.enabled = true;
                colliderSelf.enabled = true;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                transform.position -= new Vector3(1.5f, 0, 0);
                break;
        }
    }

    /*IEnumerator Respawn()
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
    }*/
}
