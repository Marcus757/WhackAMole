using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIDisplay : MonoBehaviour {
	public void ConvertToVR () {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        Destroy(gameObject.GetComponent<GraphicRaycaster>());
    }
}
