using UnityEngine;

public class TeaStation : MonoBehaviour
{
    [SerializeField] private int water = 3;
    public int Water => water;
    private bool isReady = false;

    public bool Interact()
    {
        if (isReady == true)
        {
            Debug.Log($"出茶一份,水剩余{water}份");
            isReady = false;
            return true;
        }
        else if (isReady == false)
        {
            if (water > 0)
            {
                water--;
                isReady = true;
                Debug.Log($"制茶中,水剩余{water}份");
            }
            else
            {
                Debug.Log("水已耗尽");
            }
        }
        return false;
    }

}
