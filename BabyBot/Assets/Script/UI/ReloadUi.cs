using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadUi : MonoBehaviour
{
    public GameObject globalUiReload;
    public Slider reloadSlider;

    public PlayerInfo wichPlayer;

    private void Update()
    {
        if (wichPlayer.actualWeapon._isReloading == true)
        {
            globalUiReload.SetActive(true);
            reloadSlider.value = wichPlayer.actualWeapon.actualReloadTime / wichPlayer.actualWeapon._reloadTime;
        }
        else
        {
            globalUiReload.SetActive(false);
        }
    }

}
