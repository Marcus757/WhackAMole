using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonVRPlayer : Player {
    public void Start()
    {
        PointerInputModule pointerInputModule = EventSystem.current.GetComponent<PointerInputModule>();
        if (pointerInputModule is StandaloneInputModule)
            return;

        if (pointerInputModule is OVRInputModule)
            Destroy(pointerInputModule);

        EventSystem.current.gameObject.AddComponent<StandaloneInputModule>();
    }
    
    //public override void ResetGame()
    //{
    //    OVRInput.RecenterController();
    //    base.ResetGame();
    //}

    public override bool IsResetGamePressed()
    {
        return Input.GetKeyDown(KeyCode.R) ? true : false;
    }

    public override bool IsEnterPressed()
    {
        return Input.GetMouseButtonDown(0) ? true : false;
    }

    public override bool IsItemGrabbed(GameObject item)
    {
        return Input.GetMouseButtonDown(1) ? true : false;
    }
}
