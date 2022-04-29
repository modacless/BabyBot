using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Bullet
{
    public AudioSource impactSource;
    public GameObject soundEffectPrefab;

    public Vector3 explosionCercleRange;
    public float timeBeforeExplose;
    public float explosionDamage;

    protected override void Start()
    {
        StartCoroutine(LerpScale(transform.localScale, explosionCercleRange, timeBeforeExplose));

        //Audio
        
        //-----
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<EnemySensors>().TakeDamage((int)explosionDamage,fromPlayer);
        }
    }

    IEnumerator LerpScale(Vector3 startScale, Vector3 endScale, float lerpTime)
    {
        float startTime = Time.time;
        float endTime = startTime + lerpTime;

        while (Time.time < endTime)
        {
            float timeProgressed = (Time.time - startTime) / lerpTime;  
            transform.localScale = Vector3.Lerp(startScale, endScale, timeProgressed);

            yield return new WaitForFixedUpdate();
        }

        GameObject soundEffect = Instantiate(soundEffectPrefab);
        Destroy(gameObject);
    }

    protected override void Update()
    {
        
    }

    protected override void FixedUpdate()
    {
        
    }

    protected override void OnDestroy()
    {

    }
}
