using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Vector3 explosionCercleRange;
    public float timeBeforeExplose;
    public float damage;

    protected virtual void Start()
    {
        StartCoroutine(LerpScale(transform.localScale, explosionCercleRange, timeBeforeExplose));
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<EnemySensors>().TakeDamage((int)damage);
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

    protected virtual void OnDestroy()
    {

    }
}
