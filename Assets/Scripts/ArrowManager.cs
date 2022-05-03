using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    private List<LookAtArrow> arrows = new List<LookAtArrow>();

    public void ShowArrowsFor(List<int> ordersIds)
    {
        foreach(int orderId in ordersIds)
        {
            foreach(LookAtArrow arrow in arrows)
            {
                if(arrow.orderId == orderId)
                {
                    arrow.Enabled(true);
                }
            }
        }
    }

    public void ShowArrowsFor(int orderId)
    {
        foreach(LookAtArrow arrow in arrows)
        {
            if(arrow.orderId == orderId)
            {
                arrow.Enabled(true);
            }
        }
    }

    public void HideArrowsFor(List<int> ordersIds)
    {
        foreach(int orderId in ordersIds)
        {
            foreach(LookAtArrow arrow in arrows)
            {
                if(arrow.orderId == orderId)
                {
                    arrow.Enabled(false);
                }
            }
        }
    }

    public void HideArrowsFor(int orderId)
    {
        foreach(LookAtArrow arrow in arrows)
        {
            if(arrow.orderId == orderId)
            {
                arrow.Enabled(false);
            }
        }
    }
    public void SpawnArrow(Transform target, int orderId, string color)
    {
        var transform1 = transform;
        GameObject arrowGO = Instantiate(arrowPrefab, transform1.position, Quaternion.identity, transform1);
        LookAtArrow lookAtArrow = arrowGO.GetComponent<LookAtArrow>();

        lookAtArrow.Initialize(target, orderId, color);

        arrows.Add(lookAtArrow);
    }

    public void RemoveArrow(int orderId)
    {
        Debug.Log("removing arrow...");
        foreach(var curArrow in arrows)
        {
            if(curArrow.orderId == orderId)
            {
                arrows.Remove(curArrow);
                curArrow.Destroy();
                break;
            }
        }
    }
}