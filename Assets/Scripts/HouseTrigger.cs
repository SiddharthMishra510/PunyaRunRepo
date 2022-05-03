using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject DeliverOrderButton;
    [SerializeField]
    private House house;

    private void OnTriggerEnter2D()
    {
        if(house.HasAnOrderPlaced)
        {
            if(DeliveryBoy.Instance.ContainsOrderFor(house))
            {
                ShowDeliverOrderUI();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        HideDeliverOrderUI();
    }

    private void HideDeliverOrderUI()
    {
        DeliverOrderButton.SetActive(false);

        DeliveryBoy.Instance.GetArrowManager().ShowArrowsFor(house.OrderId);
    }

    private void ShowDeliverOrderUI()
    {
        DeliverOrderButton.SetActive(true);

        DeliveryBoy.Instance.GetArrowManager().HideArrowsFor(house.OrderId);
    }

    /* // Old code
    private void OnTriggerEnter2D()
    {
    // Debug.Log("OnTriggerEnter2D" + gameObject);
    var house = transform.parent.GetComponent<House>();
    DeliveryBoy.Instance.CheckForDelivery(house);
    }*/
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        