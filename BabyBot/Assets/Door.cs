using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject child;

    // Start is called before the first frame update
    public void OpenDoor()
    {
        Destroy(gameObject);
    }


    public void SetHerselfOff()
    {
        child.SetActive(false);
    }
    public void SetHerselfOn()
    {
        child.SetActive(true);
    }
}
