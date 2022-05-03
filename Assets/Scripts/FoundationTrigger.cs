using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationTrigger : MonoBehaviour
{
    [SerializeField]
    private Foundation foundation;
    [SerializeField]
    private GameObject DonateButton;

    private void OnTriggerEnter2D()
    {
        // Debug.Log("OnTriggerEnter2D " + gameObject);

        if(foundation.IsReady())
        {
            ShowDonateUI();
            // DeliveryBoy.Instance.CheckForDelivery(foundation);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        HideDonateUI();
    }

    private void HideDonateUI()
    {
        DonateButton.SetActive(false);

        DeliveryBoy.Instance.GetArrowManager().ShowArrowsFor(foundation.GetOrderId());
    }

    private void ShowDonateUI()
    {
        DonateButton.SetActive(true);

        DeliveryBoy.Instance.GetArrowManager().HideArrowsFor(foundation.GetOrderId());
    }
}