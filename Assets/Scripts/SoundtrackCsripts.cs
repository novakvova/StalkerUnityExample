using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackCsripts : MonoBehaviour
{
    private AudioSource source;

    public AudioClip[] clips;

    private int count = 0;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        count = Random.Range(0, clips.Length);
        source.PlayOneShot(clips[count]);
    }

    private void Update()
    {
        if (!source.isPlaying)
        {
            int buf = count;
            while(count == buf)
                count = Random.Range(0, clips.Length);
            
            source.PlayOneShot(clips[count]);
        }
    }
}
