using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        Destroy(gameObject.GetComponent<GraphicRaycaster>());
        gameObject.AddComponent<OVRRaycaster>().sortOrder = 20;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
