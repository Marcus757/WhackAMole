using UnityEngine;
using UnityEngine.EventSystems;

public class OculusRiftPlayer : Player {
    public void Start()
    {
        PointerInputModule pointerInputModule = EventSystem.current.GetComponent<PointerInputModule>();
        if (pointerInputModule is OVRInputModule)
            return;

        if (pointerInputModule is StandaloneInputModule)
            Destroy(pointerInputModule);

        EventSystem.current.gameObject.AddComponent<OVRInputModule>();
        EventSystem.current.GetComponent<OVRInputModule>().rayTransform = Camera.main.transform;
    }

    public override bool IsResetGamePressed()
    {
        return OVRInput.GetDown(OVRInput.RawButton.Start) ? true : false;
    }
    
    public override bool IsEnterPressed()
    {
        return OVRInput.GetDown(OVRInput.RawButton.A) ? true : false;
    }

    public override bool IsItemGrabbed(GameObject item)
    {
        OVRGrabber[] grabbers = GameObject.FindObjectsOfType<OVRGrabber>();
        foreach (var grabber in grabbers)
        {
            if (grabber.grabbedObject != null && grabber.grabbedObject.GetType() == item.GetType())
                return true;
        }

        return false;
    }
}
