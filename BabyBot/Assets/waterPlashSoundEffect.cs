using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPlashSoundEffect : MonoBehaviour
{
    public AudioSource waterPlashSource;

    private void Start()
    {
        AudioManager Audio = AudioManager.AMInstance;
        float pitch = Random.Range(0.8f, 1.2f);
        int index = Random.Range(0, (Audio.waterImpactsArray.Length - 1));
        Audio.PlaySFX(Audio.waterImpactsArray[index], waterPlashSource, pitch);

        StartCoroutine(existanceTime());
    }

    IEnumerator existanceTime()
    {
        yield return new WaitForSeconds(2);

        Destroy(this.gameObject);
    }
}
