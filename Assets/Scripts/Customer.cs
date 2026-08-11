using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private Color needsTeaColor = Color.red;
    [SerializeField] private Color satisfiedColor = Color.green;

    private SpriteRenderer spriteRenderer;
    private bool needsTea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        needsTea = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = needsTea ? needsTeaColor : satisfiedColor;
    }

    // Update is called once per frame
    public bool OfferTea()
    {
        if (needsTea == false)
        {
            return false;
        }

        needsTea = false;
        spriteRenderer.color = needsTea ? needsTeaColor : satisfiedColor;
        return true;
    }
}
