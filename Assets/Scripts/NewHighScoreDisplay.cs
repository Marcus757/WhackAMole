using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHighScoreDisplay : MonoBehaviour {
    public static bool areInitialsEntered = false;
    private

    // Use this for initialization
    void Start () {
        GameController gameController = GameObject.FindObjectOfType<GameController>();
        Player player = gameController.player;

        if (player is NonVRPlayer)
            GetComponent<Canvas>().worldCamera = player.GetComponentInChildren<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAreInitialsEntered()
    {
        if (!string.IsNullOrEmpty(GetComponentInChildren<InputField>().text))
            areInitialsEntered = true;
    }

}
