using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableWeapon", menuName = "player/ScriptableWeapon", order = 1)]
public class ScriptableWeapon : ScriptableObject
{
    [Header("Stats")]
    public int maxAmo;
    public float basicCadence;
    public float reloadTime;
    public float bulletDamage;
    public float bulletLifeTime;
    public float bulletSpeed;
    public bool needPreheated;
    public float finalCadence;
    public float timeBeforeNeedPreheated;
    public AnimationCurve preheatedCurve;
}