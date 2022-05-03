using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskListUIManager : MonoBehaviour
{
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

    float tasListUIIntitialXPos;
    private bool isOpen = true;
    private readonly int maxTextCountAllowed = 5;

    private void Start()
    {
        tasListUIIntitialXPos = taskListUI.transform.position.x;
    }

    public void UpdatePickupTexts(List<Restaurant> pickUpList)
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

    public void UpdateDeliverToTexts(List<House> deliverToList)
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