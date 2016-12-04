using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour 
{
    private ParticleSystem par;

    private AudioSource _source;
    public AudioClip waterDeathSound;
    public float waterDeathSoundVol = 1;

    void Awake()
    {
        par = GetComponentInChildren<ParticleSystem>();
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = waterDeathSound;
        _source.volume = waterDeathSoundVol;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            PlayerCore._instance.Die();
            _source.Play();
        }

        else if (col.gameObject.layer == 8)
            PlayEffect(col.transform.position);

        if (col.gameObject.layer == 9)
        {
            PlayEffect(col.transform.position);
            Destroy(col.gameObject);
        }

    }

    void PlayEffect(Vector3 pos)
    {
        //call splash sound
        par.transform.position = pos;
        par.Play();
    }
}
