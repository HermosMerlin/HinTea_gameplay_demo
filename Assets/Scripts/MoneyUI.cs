using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;
    private TMP_Text moneyText;

    void Awake()
    {
        moneyText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        moneyText.text = $"Money: {playerInteraction.Money}";
    }
}