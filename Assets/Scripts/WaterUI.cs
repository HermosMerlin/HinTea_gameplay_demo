using TMPro;
using UnityEngine;

public class WaterUI : MonoBehaviour
{
    [SerializeField] private TeaStation teaStation;
    private TMP_Text waterText;

    void Awake()
    {
        waterText = GetComponent<TMP_Text>();
    }

    void Start()
    {
        waterText.text = $"Water: {teaStation.Water}";
    }

    void Update()
    {
        waterText.text = $"Water: {teaStation.Water}";
    }
}