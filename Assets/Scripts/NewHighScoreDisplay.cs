using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHighScoreDisplay : MonoBehaviour {
    public bool areInitialsEntered = false;

    // Use this for initialization
    void Start () {
        GameController gameController = GameObject.FindObjectOfType<GameController>();
        Player player = gameController.player;
        GetComponent<Canvas>().worldCamera = player.GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAreInitialsEntered()
    {
        areInitialsEntered = true;
    }

}
