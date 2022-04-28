using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerHealth : MonoBehaviour
{
    public int wichPlayer;
    public Slider hpSlider;

    private void Update()
    {
        HpUpdate();
    }

    private void HpUpdate()
    {
        if (wichPlayer == 1)
        {
            hpSlider.value = PlayerInfoManager.instance.infoPlayer1.actualHealth / PlayerInfoManager.instance.infoPlayer1.maxHp;
        }
        else
        {
            hpSlider.value = PlayerInfoManager.instance.infoPlayer2.actualHealth / PlayerInfoManager.instance.infoPlayer2.maxHp;
        }
    }

}
