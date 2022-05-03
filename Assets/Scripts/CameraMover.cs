using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;

    void LateUpdate()
    {
        Vector3 newPosition = followTarget.position;
        newPosition.z = transform.position.z;
        newPosition.x = Mathf.Clamp(newPosition.x, -94, 91);
        newPosition.y = Mathf.Clamp(newPosition.y, -72, 72);

        transform.position = newPosition;
    }
}
