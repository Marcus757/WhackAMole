using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRPlayer : Player {
    public override void ResetGame()
    {
        OVRInput.RecenterController();
        base.ResetGame();
    }

    public override bool IsResetGamePressed()
    {
        if (Input.GetKeyDown(KeyCode.R))
            return true;

        return false;
    }

    public override bool IsHammerGrabbed()
    {
        if (Input.GetMouseButtonDown(1))
            return true;

        return false;
    }

    public override bool IsEnterPressed()
    {
        if (Input.GetMouseButtonDown(0))
            return true;

        return false;
    }
}
