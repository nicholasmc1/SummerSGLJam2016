using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour 
{

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
			PlayerCore._instance.Die ();
        }

        if (col.gameObject.layer == 9)
            Destroy(col.gameObject);

    }
}
