using System.Collections;
using UnityEngine;

public class Foundation : MonoBehaviour
{
    public bool IsReady() { return isReady; }
    private bool isReady = false;

    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject outline;
    [SerializeField]
    private GameObject icon;

    private Coroutine glowing;
    private MultiplierSystem multiplierSystem;
    private const int FOUNDATION_ID = 1000; // Todo or FOuntationOrderId?

    private void Start()
    {
        multiplierSystem = GetComponent<MultiplierSystem>();
    }

    public void Button_Donate()
    {
        Debug.Log("Button_Donate clicked");

        DeliveryBoy.Instance.DonateTo(this);
        Deactivate();
        button.SetActive(false);
    }

    public void GetDonation(int money)
    {
        // DeliveryBoy.Instance.GetBlessedWithPunya(money * multiplierSystem.GetMultiplier());
        DeliveryBoy.Instance.GetBlessedWithPunya();

        multiplierSystem.IncreaseMultiplier();

        UIAnimationManager.Instance.SpawnPunyaFlyingImageGenerator(transform);
    }

    public void Activate()
    {
        isReady = true;
        StartGlowing();
    }

    public void Deactivate()
    {
        isReady = false;
        StopGlowing();
    }

    private void StartGlowing()
    {
        DeliveryBoy.Instance.GetArrowManager().SpawnArrow(transform, FOUNDATION_ID, "golden");

        icon.SetActive(true);
        glowing = StartCoroutine(GlowingIEnumerator());
    }

    public int GetOrderId()
    {
        return FOUNDATION_ID;
    }

    public void StopGlowing()
    {
        StopCoroutine(glowing);
        icon.SetActive(false);
        outline.SetActive(false);
    }

    private IEnumerator GlowingIEnumerator()
    {
        while(true)
        {
            outline.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            outline.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
    }
}