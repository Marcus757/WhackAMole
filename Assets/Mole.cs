using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour {
    public float visibleHeight = 0.2f;
    public float hiddenHeight = -0.3f;
    public float speed = 4f;
    public float disappearDuration = .5f; 
    
    private Vector3 targetPosition;
    private float disappearTimer = 0f;
    private bool isVisible = false;

    private void Awake()
    {
        targetPosition = new Vector3(
            transform.localPosition.x,
            hiddenHeight,
            transform.localPosition.z
            );

        transform.localPosition = targetPosition;
    }

    // Update is called once per frame
    void Update () {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0f)
        {
            Hide();
        }

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.deltaTime * speed
            );
	}

    public void OnHit()
    {
        Hide();
    }

    public void Hide()
    {
        targetPosition = new Vector3(
            transform.localPosition.x,
            hiddenHeight,
            transform.localPosition.z
            );
        isVisible = false;
    }

    public void Rise()
    {
        targetPosition = new Vector3(
            transform.localPosition.x,
            visibleHeight,
            transform.localPosition.z
            );

        disappearTimer = disappearDuration;
        isVisible = true;
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Mole hit!");
        if (collision.collider.GetComponent<Hammer>() == null)
            return;

        Mole mole = transform.GetComponent<Mole>();

        if (!mole.IsVisible())
            return;

        mole.OnHit();
        GameObject.FindObjectOfType<Player>().score++;
        Debug.Log(GameObject.FindObjectOfType<Player>().score);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Mole hit!");
    //    if (other.GetComponent<Hammer>() == null)
    //        return;

    //    Mole mole = transform.GetComponent<Mole>();

    //    if (!mole.IsVisible())
    //        return;

    //    mole.OnHit();
    //    GameObject.FindObjectOfType<Player>().score++;
    //    Debug.Log(GameObject.FindObjectOfType<Player>().score);
    //}
}
