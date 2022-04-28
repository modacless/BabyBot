using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public GameObject explosion;

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation);


        //Audio
        AudioManager Audio = AudioManager.AMInstance;
        float pitch = Random.Range(0.8f, 1.2f);
        int index = Random.Range(0, (Audio.waterImpactsArray.Length - 1));
        Audio.PlaySFX(Audio.waterImpactsArray[index], 1, pitch);
        //----
    }
}
