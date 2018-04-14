using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<OVRGrabbable>().isGrabbed && !GetComponent<ParticleSystem>().isPlaying)
            GetComponent<ParticleSystem>().Play();

        if (GetComponent<OVRGrabbable>().isGrabbed && GetComponent<ParticleSystem>().isPlaying)
            GetComponent<ParticleSystem>().Stop();
	}
}
