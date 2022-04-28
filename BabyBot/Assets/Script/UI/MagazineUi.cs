using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MagazineUi : MonoBehaviour
{
    public Slider magazineSlider;
    public PlayerInfo wichPlayer;
    public TextMeshProUGUI ammoText;

    private void Update()
    {
        ammoText.text = ("x " + wichPlayer.actualWeapon.actualAmo.ToString());
        magazineSlider.value = wichPlayer.actualWeapon.actualAmo / wichPlayer.actualWeapon.magazineAmmo;
    }

}
