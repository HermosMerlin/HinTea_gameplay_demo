using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private bool nearTeaStation, nearCustomer;
    private bool haveTea;
    private int money;
    public int Money => money;
    private TeaStation currentTeaStation;
    private Customer currentCustomer;

    void Awake()
    {
        haveTea = false;
        money = 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "TeaStation")
        {
            nearTeaStation = true;
            currentTeaStation = other.GetComponent<TeaStation>();
            Debug.Log("靠近茶站，可以按 E 交互");
        }
        else if (other.gameObject.name == "Customer")
        {
            nearCustomer = true;
            currentCustomer = other.GetComponent<Customer>();
            Debug.Log("靠近顾客，按E交互");
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "TeaStation")
        {
            nearTeaStation = false;
            currentTeaStation = null;
            Debug.Log("离开茶站");
        }
        else if (other.gameObject.name == "Customer")
        {
            nearCustomer = false;
            currentCustomer = null;
            Debug.Log("离开顾客");
        }
    }

    public void OnInteract(InputValue value)
    {
        if (nearTeaStation && value.isPressed)
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
        else if (nearCustomer && value.isPressed)
        {
            if (haveTea == true)
            {
                Debug.Log("交付茶");
                bool completed = currentCustomer.OfferTea();
                if (completed == true)
                {
                    haveTea = false;
                    money += 10;
                    Debug.Log($"订单完成，获得 10 金钱，当前金钱：{money}");
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
