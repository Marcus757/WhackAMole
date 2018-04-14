using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour {
    public float visibleHeight = 0.2f;
    public float hiddenHeight = -0.3f;
    public float speed = 4f;
    public float disappearDuration; 
    
    private Vector3 targetPosition;
    private float disappearTimer = 0f;
    private bool isVisible = false;


    private float disapperDecrement = .1f;
    public float minimumDisappearDuration = 1.25f;

    private void Awake()
    {
        targetPosition = new Vector3(
            transform.localPosition.x,
            hiddenHeight,
            transform.localPosition.z
            );

        transform.localPosition = targetPosition;
    }

    private void Start()
    {
        ChangeDisappearDuration();        
    }

    // Update is called once per frame
    void Update () {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0f)
            Hide();

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
        disappearDuration -= disapperDecrement;

        if (disappearDuration < minimumDisappearDuration)
            disappearDuration = minimumDisappearDuration;

        isVisible = true;
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hammer>() == null)
            return;

        Mole mole = transform.GetComponent<Mole>();

        if (!mole.IsVisible())
            return;

        mole.OnHit();
        GameObject.FindObjectOfType<Player>().score++;
    }

    private void ChangeDisappearDuration()
    {
        switch (GameController.level)
        {
            case 1:
                goto default;
            case 2:
                minimumDisappearDuration = .85f;
                break;
            case 3:
                minimumDisappearDuration = .45f;
                break;
            default:
                minimumDisappearDuration = 1.25f;
                break;
        }
    }
}
