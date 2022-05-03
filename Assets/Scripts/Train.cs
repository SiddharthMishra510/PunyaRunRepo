using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField]
    private float speed = 4;
    [SerializeField]
    private Transform endingLocation;

    private Vector3 startLocation;
    private Vector3 initialScale;

    private void Start()
    {
        startLocation = transform.position;
        initialScale = transform.localScale;
        StartCoroutine(StartTrain());
    }

    private float CalculateTime(Vector3 position)
    {
        float distance = Vector2.Distance(position, transform.position);
        return distance / speed;
    }

    private IEnumerator StartTrain()
    {
        float time;

        while(true)
        {
            time = CalculateTime(endingLocation.position);
            transform.LeanMoveX(endingLocation.position.x,time);
            yield return new WaitForSeconds(time);

            initialScale.x *= -1;
            transform.localScale = initialScale;

            time = CalculateTime(startLocation);
            transform.LeanMoveX(startLocation.x, time);
            yield return new WaitForSeconds(time);

            initialScale.x *= -1;
            transform.localScale = initialScale;
        }
    }
}