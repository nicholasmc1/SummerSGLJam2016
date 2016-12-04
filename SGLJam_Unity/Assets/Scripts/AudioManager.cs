using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
    private AudioSource ambSource;
    private AudioSource pumpSource;
	private AudioSource whooshSource;
	[HideInInspector]
	public AudioSource chargeSource;
	[HideInInspector]
	public AudioSource fireSource;
	[HideInInspector]
	public AudioSource blinkSource;
	[HideInInspector]
	public AudioSource deathSource;
	// Audio Clips
    public AudioClip ambClip;
    public AudioClip pumpClip;
	public AudioClip whooshClip;
	public AudioClip chargeClip;
	public AudioClip fireClip;
	public AudioClip blinkClip;
	public AudioClip velocityDeathClip;

    void Awake()
    {
        if (AudioManager._instance == null)
        {
            _instance = this;
			// Create audio sources
            ambSource = gameObject.AddComponent<AudioSource>();
            pumpSource = gameObject.AddComponent<AudioSource>();
			whooshSource = gameObject.AddComponent<AudioSource> ();
			chargeSource = gameObject.AddComponent<AudioSource> ();
			fireSource = gameObject.AddComponent<AudioSource> ();
			blinkSource = gameObject.AddComponent<AudioSource> ();
			deathSource = gameObject.AddComponent<AudioSource> ();
			// Attach clips to sources
            ambSource.clip = ambClip;
            ambSource.loop = true;
			ambSource.volume = 0.8f;
            pumpSource.clip = pumpClip;
            pumpSource.loop = true;
			whooshSource.clip = whooshClip;
			whooshSource.loop = true;
			chargeSource.clip = chargeClip;
			chargeSource.loop = true;
			fireSource.clip = fireClip;
			blinkSource.clip = blinkClip;
			deathSource.clip = velocityDeathClip;
			// Play sounds
            ambSource.Play();
            pumpSource.Play();
			chargeSource.Play ();
			chargeSource.volume = 0;

        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void FastMusic(float vol)
    {
        pumpSource.volume = vol;
		whooshSource.volume = vol;
    }
}
