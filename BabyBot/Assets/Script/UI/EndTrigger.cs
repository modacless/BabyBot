using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    private int numOfPlayer;
    public GameObject endMenu;
    public TextMeshProUGUI counterText;
    public float cooldown;

    private float actualTime;
    private bool lockSysteme;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            numOfPlayer += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            numOfPlayer -= 1;
        }
    }


    private void Start()
    {
        actualTime = cooldown;
        endMenu.SetActive(false);
    }

    private void Update()
    {
        if (numOfPlayer == 2)
        {
            endMenu.SetActive(true);

            if (lockSysteme == false)
            {
                lockSysteme = true;
                StartCoroutine(Wait());
            }
        }

        if (lockSysteme == true)
        {
            actualTime -= Time.deltaTime;
            counterText.text = Mathf.Round(actualTime).ToString();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(cooldown);
        SceneManager.LoadScene(0);
        Debug.Log("load");
    }
}
