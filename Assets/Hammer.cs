using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {
    OVRGrabbable grabbable;
    ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
        grabbable = GetComponent<OVRGrabbable>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!grabbable.isGrabbed && !particleSystem.isPlaying)
            particleSystem.Play();

        if (grabbable.isGrabbed && particleSystem.isPlaying)
            particleSystem.Stop();
	}
}
