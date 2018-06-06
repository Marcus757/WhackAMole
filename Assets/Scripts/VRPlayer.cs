using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRPlayer : Player {
    public void Start()
    {
        PointerInputModule pointerInputModule = EventSystem.current.GetComponent<PointerInputModule>();
        if (pointerInputModule is OVRInputModule)
            return;

        if (pointerInputModule is StandaloneInputModule)
            Destroy(pointerInputModule);

        gameObject.AddComponent<OVRInputModule>();
    }
    public override void ResetGame()
    {
        base.ResetGame();
    }

    public override bool IsResetGamePressed()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.Start))
            return true;

        return false;
    }

    public override bool IsHammerGrabbed()
    {
        OVRGrabber[] grabbers = GameObject.FindObjectsOfType<OVRGrabber>();
        foreach (var grabber in grabbers)
        {
            if (grabber.grabbedObject != null && grabber.grabbedObject.GetComponent<Hammer>() != null)
                return true;
        }

        return false;
    }

    public override bool IsEnterPressed()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
            return true;

        return false;
    }
}
