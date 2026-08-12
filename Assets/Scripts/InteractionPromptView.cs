using UnityEngine;
using TMPro;

public class InteractionPromptView : MonoBehaviour
{
    private TMP_Text interactText;
    void Awake()
    {
        interactText = GetComponent<TMP_Text>();
    }

    public void UpdatePrompt(PlayerInteraction.InteractionTargetType target)
    {
        switch (target)
        {
            case PlayerInteraction.InteractionTargetType.None:
                interactText.text = "";
                break;
            case PlayerInteraction.InteractionTargetType.TeaStation:
                interactText.text = "TeaStation(E)";
                break;
            case PlayerInteraction.InteractionTargetType.Customer:
                interactText.text = "Customer(E)";
                break;
        }
    }

}