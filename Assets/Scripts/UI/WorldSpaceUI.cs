using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WorldSpaceUI : MonoBehaviour {
	public void ConvertToVR () {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        var graphicRaycaster = gameObject.GetComponent<GraphicRaycaster>();

        if (graphicRaycaster != null)
            Destroy(gameObject.GetComponent<GraphicRaycaster>());
    }
}
