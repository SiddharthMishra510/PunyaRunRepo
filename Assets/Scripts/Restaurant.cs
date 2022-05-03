using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : MonoBehaviour
{
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private GameObject outline;
    [SerializeField]
    private SpriteRenderer outlineRenderer;

    private List<Order> orders = new List<Order>();

    private bool toGlow;
    private Coroutine glowing = null;

    public List<int> GetOrdersIds()
    {
        List<int> orderIds = new List<int>();

        foreach(Order order in orders)
        {
            orderIds.Add(order.Id);
        }

        return orderIds;
    }

    public bool HasOrders()
    {
        return orders.Count > 0;
    }

    public void PlaceOrder(Order order)
    {
        DeliveryBoy.Instance.GetArrowManager().SpawnArrow(this.transform, order.Id, "red");

        DeliveryBoy.Instance.TaskList.AddTask(this);

        // Debug.Log("order placed by houseId: " + order.House.HouseName + " in " + this.gameObject + " with orderId: " + order.Id);
        orders.Add(order);

        StartGlowing();
    }

    public void Button_OrderPickup()
    {
        Debug.Log("order picked up by boy");
        AudioManager.Instance.Play("RestaurantPickup");
        
        DeliveryBoy.Instance.PickUpOrders(orders, this);

        orders.Clear();
        StopGlowing();
        button.SetActive(false);
    }

    private void StartGlowing()
    {
        toGlow = true;
        icon.SetActive(true);
        glowing = StartCoroutine(GlowingIEnumerator());
    }

    private void StopGlowing()
    {
        icon.SetActive(false);
        toGlow = false;
    }

    private void DisableOutline()
    {
        outline.SetActive(false);
    }

    private IEnumerator GlowingIEnumerator()
    {
        while (toGlow)
        {
            // Debug.Log("COROUTINE: outline.SetActive(true)");
            outline.SetActive(true);
            // Debug.Log("COROUTINE: WaitForSeconds(0.5f)");
            yield return new WaitForSeconds(0.5f);
            if (toGlow == false)
            {
                break;
            }
            // Debug.Log("COROUTINE: outline.SetActive(false)");
            outline.SetActive(false);
            // Debug.Log("COROUTINE: WaitForSeconds(0.2f)");
            yield return new WaitForSeconds(0.2f);
        }
        outline.SetActive(true);
        outlineRenderer.color = Color.yellow;
        yield return new WaitForSeconds(1f);
        outlineRenderer.color = Color.white;
        outline.SetActive(false);
    }
}