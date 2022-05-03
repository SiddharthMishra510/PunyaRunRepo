using UnityEngine;

public class RestaurantTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject GetOrderButton;
    [SerializeField]
    private Restaurant restaurant;

    private void OnTriggerEnter2D()
    {
        if(restaurant.HasOrders())
        {
            ShowGetOrderUI();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        HideGetOrderUI();
    }
    
    private void HideGetOrderUI()
    {
        GetOrderButton.SetActive(false);

        DeliveryBoy.Instance.GetArrowManager().ShowArrowsFor(restaurant.GetOrdersIds());
    }

    private void ShowGetOrderUI()
    {
        GetOrderButton.SetActive(true);

        DeliveryBoy.Instance.GetArrowManager().HideArrowsFor(restaurant.GetOrdersIds());
    }
}