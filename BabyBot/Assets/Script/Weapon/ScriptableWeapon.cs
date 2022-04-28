using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableWeapon", menuName = "player/ScriptableWeapon", order = 1)]
public class ScriptableWeapon : ScriptableObject
{
    [Header("Stats")]
    public GameObject bullet;
    public Vector3 sizeBullet;
    public int numberBulletSpawned;
    public float shootConeAngle;
    public int magazineAmmo;
    public float reloadTime;
    public float fireRate;
    public float bulletDamage;
    public float bulletLifeTime;
    public float bulletSpeed;

    public bool needPreheated;
    public float finalCadence;
    public float timeBeforeNeedPreheated;
/*    public bool isLaser;
    public float laserDistance;*/
    public AnimationCurve preheatedCurve;
}