using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    private List<Restaurant> pickUpList = new List<Restaurant>();
    private List<House> deliverToList = new List<House>();

    [SerializeField]
    private List<TextMeshProUGUI> pickUpTexts;
    [SerializeField]
    private List<TextMeshProUGUI> deliverToTexts;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject taskListUI;
    [SerializeField]
    private RectTransform hidePosition;
    private bool isOpen = true;
    float tasListUIIntitialXPos;

    private readonly int maxTextCountAllowed = 5;

    private void Start()
    {
        tasListUIIntitialXPos = taskListUI.transform.position.x;
    }
    public bool AreTasksOver()
    {
        if (pickUpList.Count == 0 && deliverToList.Count == 0)
        {
            return true;
        }
        return false;
    }

    public void AddTask(Restaurant restaurant)
    {
        pickUpList.Add(restaurant);
        UpdatePickupTexts();
    }

    public void AddTask(House house)
    {
        deliverToList.Add(house);
        UpdateDeliverToTexts();
    }

    public void RemoveTask(Restaurant restaurant)
    {
        pickUpList.Remove(restaurant);
        UpdatePickupTexts();
    }

    public void RemoveTask(House house)
    {
        deliverToList.Remove(house);
        UpdateDeliverToTexts();
    }

    private void UpdatePickupTexts()
    {
        foreach (var textField in pickUpTexts)
        {
            textField.text = "";
        }

        int maxIteration = pickUpList.Count;
        for (int i = 0; i < Mathf.Clamp(maxIteration, 0, maxTextCountAllowed); i++)
        {
            pickUpTexts[i].text = "(" + (i + 1) + ") " + pickUpList[i].name;
        }
    }

    private void UpdateDeliverToTexts()
    {
        foreach (var textField in deliverToTexts)
        {
            textField.text = "";
        }

        // Debug.Log("COUNT deliverToList: " + deliverToList.Count);
        int maxIteration = deliverToList.Count;
        for(int i = 0; i < Mathf.Clamp(maxIteration, 0, maxTextCountAllowed); i++)
        {
            deliverToTexts[i].text = "(" + (i + 1) + ") " + deliverToList[i].HouseName;
        }
    }

    public void OnArrowClick()
    {
        arrow.SetActive(false);

        if (isOpen)
        {
            isOpen = false;
            taskListUI.transform.LeanMoveX(hidePosition.position.x, 0.5f).setEaseInOutCubic();
            arrow.transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            isOpen = true;
            taskListUI.transform.LeanMoveX(tasListUIIntitialXPos, 0.5f).setEaseInOutCubic();
            arrow.transform.localScale = new Vector2(1, 1);
        }
        Invoke("EnableArrow", 0.5f);
    }
    private void EnableArrow()
    {
        arrow.SetActive(true);
    }
}