using UnityEngine;

public class TeaStation : MonoBehaviour
{
    [SerializeField] private int water = 3;
    public int Water => water;
    private bool isReady = false;

    public bool Interact()
    {
        if (isReady)
        {
            ToastSystem.Show($"Tea ready, water left: {water}");
            isReady = false;
            return true;
        }

        if (water > 0)
        {
            water--;
            isReady = true;
            ToastSystem.Show($"Brewing tea, water left: {water}");
        }
        else
        {
            ToastSystem.Show("Water depleted");
        }

        return false;
    }

}
