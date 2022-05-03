using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DeliveryBoy : MonoBehaviour
{
    public static DeliveryBoy Instance;

    public TaskList TaskList;
    public ArrowManager GetArrowManager() { return arrowManager; }

    [SerializeField]
    private TextMeshProUGUI moneyField;
    [SerializeField]
    private TextMeshProUGUI punyaField;

    private List<Order> ordersRetained = new List<Order>();
    private ArrowManager arrowManager;
    private int myCurrentMoney = 0;
    private int myCurrentPunya = 0;
    private const int FOUNDATION_ID = 1000;
    private const int MONEY_ADD_AMOUNT = 50;
    private const int PUNYA_ADD_AMOUNT = 50;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        arrowManager = GetComponent<ArrowManager>();
        moneyField.text = myCurrentMoney.ToString();
        punyaField.text = myCurrentPunya.ToString();
    }

    public bool ContainsOrderFor(House house)
    {
        foreach(Order order in ordersRetained)
        {
            if(order.House == house)
            {
                print("ContainsOrderFor: " + house.name);
                return true;
            }
        }

        return false;
    }

    public void PickUpOrders(List<Order> orders, Restaurant restaurant)
    {
        foreach(Order order in orders)
        {
            arrowManager.SpawnArrow(order.House.transform, order.Id, "green");
            order.House.StartGlowing();
            Instance.TaskList.AddTask(order.House);
        }

        foreach(Order order in orders)
        {
            Instance.TaskList.RemoveTask(restaurant);
            arrowManager.RemoveArrow(order.Id);
        }

        ordersRetained.AddRange(orders);
    }

    public void CheckForDelivery(House house)
    {
        Debug.Log("CheckForDelivery");
        if(house.HasAnOrderPlaced)
        {
            Debug.Log("HasAnOrderPlaced");
            foreach(Order order in ordersRetained)
            {
                if(order.Id == house.OrderId)
                {
                    DeliverOrder(order);
                    break;
                }
            }
        }
    }

    public void DonateTo(Foundation foundation)
    {
        Debug.Log("Donating...");
        foundation.GetDonation(myCurrentMoney);
        ResetMoney();
        arrowManager.RemoveArrow(FOUNDATION_ID);
        FoodServiceSystem.Instance.Reset();
    }
  
    public void GetBlessedWithPunya()
    {
        AudioManager.Instance.Play("Blessing");
    }

    private void DeliverOrder(Order order)
    {
        arrowManager.RemoveArrow(order.Id);
        order.House.TakeDelivery();

        Instance.TaskList.RemoveTask(order.House);
        ordersRetained.Remove(order);

        if(TaskList.AreTasksOver())
        {
            // Debug.Log("Finish");
            FoodServiceSystem.Instance.TasksCompleted();
        }
    }

    public void IncreasePunya()
    {
        myCurrentPunya += PUNYA_ADD_AMOUNT;

        PlayerPrefs.SetInt("TotalPunyaEarned", PlayerPrefs.GetInt("TotalPunyaEarned", 0) + PUNYA_ADD_AMOUNT);

        UIParticleEffectManager.Instance.PlayPunyaPE();
        punyaField.text = myCurrentPunya.ToString();
    }

    private void ResetPunya()
    {
        myCurrentPunya = 0;
        punyaField.text = myCurrentPunya.ToString();
    }

    public void IncreaseMoney()
    {
        myCurrentMoney += MONEY_ADD_AMOUNT;
        moneyField.text = myCurrentMoney.ToString();
        UIParticleEffectManager.Instance.PlayCoinPE();
    }

    private void ResetMoney()
    {
        myCurrentMoney = 0;
        moneyField.text = myCurrentMoney.ToString();
    }

    public int GetCurrentPunya()
    {
        return myCurrentPunya;
    }
}