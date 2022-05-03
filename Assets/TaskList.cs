using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    [SerializeField]
    private TaskListUIManager taskListUIManager;

    private List<Restaurant> pickUpList = new List<Restaurant>();
    private List<House> deliverToList = new List<House>();

    public bool AreTasksOver()
    {
        if(pickUpList.Count == 0 && deliverToList.Count == 0)
        {
            return true;
        }

        return false;
    }

    public void AddTask(Restaurant restaurant)
    {
        pickUpList.Add(restaurant);
        taskListUIManager.UpdatePickupTexts(pickUpList);
    }

    public void AddTask(House house)
    {
        deliverToList.Add(house);
        taskListUIManager.UpdateDeliverToTexts(deliverToList);
    }

    public void RemoveTask(Restaurant restaurant)
    {
        pickUpList.Remove(restaurant);
        taskListUIManager.UpdatePickupTexts(pickUpList);
    }

    public void RemoveTask(House house)
    {
        deliverToList.Remove(house);
        taskListUIManager.UpdateDeliverToTexts(deliverToList);
    }
}
