using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableWeapon", menuName = "player/ScriptableWeapon", order = 1)]
public class ScriptableWeapon : ScriptableObject
{
    [Header("Stats")]
    public int maxAmo;
    public float cadence;
    public float damage;
    public float reloadTime;
    public float BulletLifeTime;
    public float BulletSpeed;
}