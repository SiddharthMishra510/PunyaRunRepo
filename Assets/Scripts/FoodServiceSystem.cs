using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodServiceSystem : MonoBehaviour
{
    public static FoodServiceSystem Instance;

    [SerializeField]
    private Foundation foundation;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private List<Restaurant> restaurants;
    [SerializeField]
    private List<House> houses;

    private int successNum = 0;

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
        Reset();
    }

    public Restaurant GetRandomRestaurant()
    {
        if(restaurants.Count == 0)
        {
            Debug.LogWarning("No restaurants.");
            return null;
        }
        else
        {
            int randomRestaurantIndex = Random.Range(0, restaurants.Count);
            print("randomRestaurantIndex: " + randomRestaurantIndex);
            return restaurants[randomRestaurantIndex];
        }   
    }

    private void GenerateOrders(int numOfOrders)
    {
        // Debug.Log("Number of orders: " + numOfOrders);

        #region Some basic checks
        if(numOfOrders > restaurants.Count)
        {
            Debug.LogWarning("Number of orders > Number of hotels by " + (numOfOrders - restaurants.Count).ToString());
        }
        else if(numOfOrders > houses.Count)
        {
            Debug.LogWarning("Number of orders > Number of houses by " + (numOfOrders - houses.Count).ToString());
        }
        #endregion

        // Select 'numOfOrders' houses from the houses list and place orders by them.

        List<House> housesWithNoOrdersPlaced = new List<House>(houses);

        for(int i = 0; i < numOfOrders; i++)
        {
            print("i : " + i);
            House hungryHouse = housesWithNoOrdersPlaced[Random.Range(0, housesWithNoOrdersPlaced.Count)];
            housesWithNoOrdersPlaced.Remove(hungryHouse);
            hungryHouse.GetHungry();
        }
    }

    public void Reset()
    {
        GameAnalytics.Instance.FoundationSubmissionEvent();
        timer.StartTimer();
        GenerateOrders(GameManager.Instance.GetNumOfOrdersForSuccessNum(successNum));
    }

    public void TasksCompleted()
    {
        successNum++;
        timer.StopTimer();
        foundation.Activate();
        GameAnalytics.Instance.TaskCompleteEvent();
    }
    public int GetSuccessNum()
    {
        return successNum;
    }
}