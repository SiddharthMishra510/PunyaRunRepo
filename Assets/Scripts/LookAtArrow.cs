using System;
using UnityEngine;

public class LookAtArrow : MonoBehaviour
{
    public Transform target;
    public int orderId;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Update()
    {
        transform.right = target.position - transform.position;
    }

    private void SetGreenColor()
    {
        spriteRenderer.color = Color.green;
    }

    private void SetRedColor()
    {
        spriteRenderer.color = new Color(1f, 0.98f, 0.1f);
    }

    private void SetGoldenColor()
    {
        spriteRenderer.color = new Color(1f, 0.6f, 0f);
    }

    public void Initialize(Transform target, int orderId, string color)
    {
        this.target = target;
        this.orderId = orderId;

        if(color.Equals("green"))
        {
            SetGreenColor();
        }
        else if(color.Equals("red"))
        {
            SetRedColor();
        }
        else if(color.Equals("golden"))
        {
            SetGoldenColor();
        }
    }

    public void Enabled(bool b)
    {

        /* if(b)
        {
            print("Arrow enabled.");
        }
        else
        {
            print("Arrow disabled.");
        } */

        spriteRenderer.enabled = b;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}