using UnityEngine;

public abstract class Player : MonoBehaviour {
    public int score;
    
    public abstract bool IsItemGrabbed(GameObject item);
    public abstract bool IsEnterPressed();
    public abstract bool IsResetGamePressed();
}
