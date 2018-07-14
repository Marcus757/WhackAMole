using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePosition : MonoBehaviour
{
    public bool freezeX;
    public bool freezeY;
    public bool freezeZ;

    // Update is called once per frame
	void Update ()
    {
        float x = freezeX ? 0 : transform.localPosition.x;
        float y = freezeY ? 0 : transform.localPosition.y;
        float z = freezeX ? 0 : transform.localPosition.z;
        transform.localPosition = new Vector3(x, y, z);
    }
}
