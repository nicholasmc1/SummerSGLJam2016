using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour 
{
    public Transform respawn;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.transform.position = respawn.position;
        }

    }
}
