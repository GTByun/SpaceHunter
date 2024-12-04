using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip[] sound = new AudioClip[5];
    private AudioSource adsc;
    private Common c;

    void Start()
    {
        adsc = GetComponent<AudioSource>();
        c = GetComponent<Common>();
        adsc.clip = sound[(int)transform.position.z];
        c.Set_cooltime(0, adsc.clip.length + 0.02f);
        if (!c.IsOriginal)
        {
            adsc.Play();
        }
    }

    void Update()
    {
        if (!c.IsOriginal)
        {
            if (c.Cooltime_Trigger(0, true, false))
            {
                Destroy(gameObject);
            }
        }
    }
}
