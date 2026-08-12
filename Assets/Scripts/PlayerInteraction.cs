using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public enum InteractionTargetType
    {
        None,
        TeaStation,
        Customer
    }

    private bool nearTeaStation, nearCustomer;
    private InteractionTargetType interactTarget;
    private bool haveTea;
    private int money;
    private const int DeliveryReward = 10;
    public int Money => money;
    private TeaStation currentTeaStation;
    private Customer currentCustomer;
    [SerializeField] private InteractionPromptView interactText;

    void Awake()
    {
        haveTea = false;
        money = 0;
        interactTarget = InteractionTargetType.None;
        nearTeaStation = false;
        nearCustomer = false;
        Debug.Assert(interactText != null, "InteractText disconnect");
    }
    private void UpdateInteractTarget(InteractionTargetType type)
    {
        interactTarget = type;
        interactText.UpdatePrompt(interactTarget);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<TeaStation>(out var teaStation))
        {
            UpdateInteractTarget(InteractionTargetType.TeaStation);
            nearTeaStation = true;
            currentTeaStation = teaStation;
            Debug.Log("靠近茶站，可以按 E 交互");
        }
        else if (other.TryGetComponent<Customer>(out var customer))
        {
            UpdateInteractTarget(InteractionTargetType.Customer);
            nearCustomer = true;
            currentCustomer = customer;
            Debug.Log("靠近顾客，按E交互");
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "TeaStation")
        {
            nearTeaStation = false;
            if (nearCustomer)
            {
                UpdateInteractTarget(InteractionTargetType.Customer);
            }
            else
            {
                UpdateInteractTarget(InteractionTargetType.None);
            }
            currentTeaStation = null;
            Debug.Log("离开茶站");
        }
        else if (other.gameObject.name == "Customer")
        {
            nearCustomer = false;
            if (nearTeaStation)
            {
                UpdateInteractTarget(InteractionTargetType.TeaStation);
            }
            else
            {
                UpdateInteractTarget(InteractionTargetType.None);
            }
            currentCustomer = null;
            Debug.Log("离开顾客");
        }
    }

    public void OnInteract(InputValue value)
    {
        if (!value.isPressed)
        {
            return;
        }
        if (interactTarget == InteractionTargetType.TeaStation)
        {
            if (haveTea == true)
            {
                Debug.Log("茶已经取过了");
            }
            else if (haveTea == false)
            {
                Debug.Log("开始与茶站交互");
                bool gotTea = currentTeaStation.Interact();
                if (gotTea == true)
                {
                    haveTea = true;
                    Debug.Log("取茶成功");
                }
            }
        }
        else if (interactTarget == InteractionTargetType.Customer && value.isPressed)
        {
            if (haveTea == true)
            {
                Debug.Log("交付茶");
                bool completed = currentCustomer.OfferTea();
                if (completed == true)
                {
                    haveTea = false;
                    money += DeliveryReward;
                    Debug.Log($"订单完成，获得 {DeliveryReward} 金钱，当前金钱：{money}");
                }
                else
                {
                    Debug.Log("该顾客已经完成订单");
                }
            }
            else
            {
                Debug.Log("未取茶");
            }
        }
    }
}
