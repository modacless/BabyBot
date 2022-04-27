using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Vector3 explosionCercleRange;
    public float timeBeforeExplose;
    public float damage;

    private void Start()
    {
        StartCoroutine(LerpScale(transform.localScale, explosionCercleRange, timeBeforeExplose));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Enemy"))
        {
            //Apply damage
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

        Destroy(gameObject);
    }
}
