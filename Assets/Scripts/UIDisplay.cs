using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIDisplay : MonoBehaviour {

	// Use this for initialization
	public virtual void Start () {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        Destroy(gameObject.GetComponent<GraphicRaycaster>());
    }
}
