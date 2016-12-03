using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour 
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("D.E.Dead");
        }

    }
}
