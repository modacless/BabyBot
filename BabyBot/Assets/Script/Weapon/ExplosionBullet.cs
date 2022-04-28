using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public GameObject explosion;

    protected override void OnDestroy()
    {
        //Audio
        AudioManager Audio = AudioManager.AMInstance;
        float pitch = Random.Range(0.8f, 1.2f);
        int index = Random.Range(0, (Audio.waterImpactsArray.Length - 1));
        Audio.PlaySFX(Audio.waterImpactsArray[index], Audio.waterImpactsVolume, pitch);
        //----
        GameObject explos = Instantiate(explosion, transform.position, explosion.transform.rotation);
        explos.GetComponent<Bullet>().InitBullet(0, 0, 0, 0, Vector3.zero, fromPlayer);
    }
}
