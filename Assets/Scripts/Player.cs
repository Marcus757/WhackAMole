using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public int score = 0;
    public static int totalScore = 0;
    public Hammer hammer;

    // Use this for initialization
    void Start() {
        if (GameController.level == 1)
            totalScore = 0;
    }

    public virtual void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (GameController.level == 1)
            Player.totalScore = 0;
    }

    public virtual bool IsResetGamePressed()
    {
        return false;
    }

    public virtual bool IsHammerGrabbed()
    {
        return false;
    }   
    
    public virtual bool IsEnterPressed()
    {
        return false;
    }


    // Update is called once per frame
    //void Update()
    //{
    //    if (!Input.GetKeyDown(KeyCode.Space))
    //        return;

    //    RaycastHit hit;

    //    if (Physics.Raycast(transform.position, transform.forward, out hit) == false)
    //        return;

    //    if (hit.transform.GetComponent<Mole>() == null)
    //        return;

    //    Mole mole = hit.transform.GetComponent<Mole>();

    //    if (!mole.IsVisible())
    //        return;

    //    mole.OnHit();
    //    hammer.Hit(mole.transform.position);
    //    score++;
    //}
}
