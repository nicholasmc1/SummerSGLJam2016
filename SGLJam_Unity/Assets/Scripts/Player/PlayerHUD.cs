using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

    public Image velocityMeter;
    public Image chargeMeter;
    public Canvas UICanvas;
    public float v = 0;
    public float charge = 0;
    public float t = 0.5f;

	void Start () {
		
	}

	void Update () {

        if (PlayerCore._instance.playerState == PlayerCore.inputState.ragdoll)
        {
            UICanvas.enabled = false;
        }
        else
        {
            UICanvas.enabled = true;
        }
    

        v = PlayerCore._instance.gameObject.GetComponent<Rigidbody>().velocity.magnitude;

        if (v <= 20)
        {
            velocityMeter.fillAmount = Mathf.Lerp(velocityMeter.fillAmount, (v / 20.0f), Time.deltaTime * 8);
        }
        else
        {
            velocityMeter.fillAmount = 1.0f;
        }

        charge = PlayerCore._instance.weapon.maxSpeed - PlayerCore._instance.weapon.speed;

        chargeMeter.fillAmount = 1 - charge / 10;
	}


}
