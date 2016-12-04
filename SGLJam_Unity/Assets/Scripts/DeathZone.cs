using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour 
{
    private ParticleSystem par;

    void Awake()
    {
        par = GetComponentInChildren<ParticleSystem>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
			PlayerCore._instance.Die ();

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
