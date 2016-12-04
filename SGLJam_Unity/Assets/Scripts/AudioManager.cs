using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
    private AudioSource ambSource;
    private AudioSource pumpSource;
    public AudioClip ambClip;
    public AudioClip pumpClip;
    void Awake()
    {
        if (AudioManager._instance == null)
        {
            _instance = this;
            ambSource = this.gameObject.AddComponent<AudioSource>();
            pumpSource = this.gameObject.AddComponent<AudioSource>();
            ambSource.clip = ambClip;
            ambSource.loop = true;
            pumpSource.clip = pumpClip;
            ambSource.loop = true;
            ambSource.Play();
            pumpSource.Play();

        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void UpadteMusicVol(float vol)
    {
        pumpSource.volume = vol;
    }
}
