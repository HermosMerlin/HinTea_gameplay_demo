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
            ToastSystem.Show("Near tea station, press E");
        }
        else if (other.TryGetComponent<Customer>(out var customer))
        {
            UpdateInteractTarget(InteractionTargetType.Customer);
            nearCustomer = true;
            currentCustomer = customer;
            ToastSystem.Show("Near customer, press E");
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
            ToastSystem.Show("Left tea station");
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
            ToastSystem.Show("Left customer");
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
                ToastSystem.Show("Tea already in hand");
            }
            else if (haveTea == false)
            {
                ToastSystem.Show("Interacting with tea station");
                bool gotTea = currentTeaStation.Interact();
                if (gotTea == true)
                {
                    haveTea = true;
                    ToastSystem.Show("Tea acquired");
                }
            }
        }
        else if (interactTarget == InteractionTargetType.Customer && value.isPressed)
        {
            if (haveTea == true)
            {
                ToastSystem.Show("Delivering tea");
                bool completed = currentCustomer.OfferTea();
                if (completed == true)
                {
                    haveTea = false;
                    money += DeliveryReward;
                    ToastSystem.Show($"Order complete, +{DeliveryReward} coins, total {money}");
                }
                else
                {
                    ToastSystem.Show("Customer already served");
                }
            }
            else
            {
                ToastSystem.Show("No tea in hand");
            }
        }
    }
}
