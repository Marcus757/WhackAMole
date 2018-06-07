using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRPlayer : Player {
    public OVRGazePointer ovrGazePointerPrefab;

    public void Start()
    {
        PointerInputModule pointerInputModule = EventSystem.current.GetComponent<PointerInputModule>();
        if (pointerInputModule is OVRInputModule)
            return;

        if (pointerInputModule is StandaloneInputModule)
            Destroy(pointerInputModule);

        EventSystem.current.gameObject.AddComponent<OVRInputModule>();
        EventSystem.current.GetComponent<OVRInputModule>().rayTransform = Camera.main.transform;

        OVRGazePointer ovrGazePointer = (OVRGazePointer)Instantiate(ovrGazePointerPrefab);
        ovrGazePointer.rayTransform = Camera.main.transform;
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
