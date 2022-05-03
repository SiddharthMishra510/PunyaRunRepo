using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    public string HouseName;
    public bool HasAnOrderPlaced = false;
    public int houseId;
    public int OrderId;

    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject outline;
    [SerializeField]
    private SpriteRenderer outlineRenderer;
    [SerializeField]
    private GameObject icon;

    private Coroutine glowing = null;

    public void GetHungry()
    {
        // Generate an order with a 4 digit orderId;
        OrderId = Random.Range(0, 999);
        Order order = new Order(OrderId, this);

        // Select a random restaurant from the list of restaurants
        Restaurant restaurant = FoodServiceSystem.Instance.GetRandomRestaurant();
        
        restaurant.PlaceOrder(order);
        HasAnOrderPlaced = true;
    }

    public void Button_DeliverOrder()
    {
        DeliveryBoy.Instance.CheckForDelivery(this);
    }

    public void TakeDelivery()
    {
        AudioManager.Instance.Play("Delivery");
        TakeDeliveryGfx();
        HasAnOrderPlaced = false;
        StopGlowing();
        button.SetActive(false);
    }

    private void TakeDeliveryGfx()
    {
        UIAnimationManager.Instance.SpawnCoinFlyingImageGenerator(transform);
        // Instantiate(EmoticonManager.Instance.GetRandomEmoticon(), emoticonHolder.transform)
    }

    public void StartGlowing()
    {
        icon.SetActive(true);
        glowing = StartCoroutine(GlowingIEnumerator());
    }

    public void StopGlowing()
    {
        StopCoroutine(glowing);
        icon.SetActive(false);
        outline.SetActive(true);
        outlineRenderer.color = Color.green;
        Invoke("DisableOutline", 1);
    }

    private void DisableOutline()
    {
        outline.SetActive(false);
    }

    private IEnumerator GlowingIEnumerator()
    {
        while (true)
        {
            outline.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            outline.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
    }
}