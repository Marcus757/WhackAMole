using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonVRPlayer : Player {
    public void Start()
    {
        //PointerInputModule pointerInputModule = EventSystem.current.GetComponent<PointerInputModule>();
        //if (pointerInputModule is StandaloneInputModule)
        //    return;

        //if (pointerInputModule is OVRInputModule)
        //    Destroy(pointerInputModule);

        //gameObject.AddComponent<StandaloneInputModule>();

        PointerInputModule pointerInputModule = EventSystem.current.GetComponent<PointerInputModule>();
        if (pointerInputModule is OVRInputModule)
            return;

        if (pointerInputModule is StandaloneInputModule)
            Destroy(pointerInputModule);

        EventSystem.current. .AddComponent<OVRInputModule>();
    }
    public void Update()
    {
        //RaycastHit vHit = new RaycastHit();
        //Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
    }

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
