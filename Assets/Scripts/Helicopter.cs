using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField]
    private List<Transform> LandingPos;
    [SerializeField]
    private List<Transform> FarPos;
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float waitingTimeAtHeliPad = 3;


    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(StartHelicopterJourney());
    }
    IEnumerator StartHelicopterJourney()
    {
        float time;
        while (true)
        {
            yield return new WaitForSeconds(1);

            time = FindAndMoveToLandingPad();
            yield return new WaitForSeconds(time);
            Landing();
            yield return new WaitForSeconds(2);

            yield return new WaitForSeconds(waitingTimeAtHeliPad);

            time = FindAndMoveToFarPlace();
            yield return new WaitForSeconds(time);



        }
    }

    private float CalculateTime(Vector3 position)
    {
        float distance = Vector2.Distance(position, transform.position);
        return distance / speed;
    }
    private void Landing()
    {
        transform.LeanMoveY(transform.position.y - 2, 2);
    }

    public float FindAndMoveToLandingPad()
    {
        int randomHelipad = Random.Range(0, LandingPos.Count);
        float time = CalculateTime(LandingPos[randomHelipad].position);
        transform.LeanMove(LandingPos[randomHelipad].position + Vector3.up * 2, time);

        if (transform.position.x < LandingPos[randomHelipad].position.x)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }

        return time;
    }

    public float FindAndMoveToFarPlace()
    {
        float time;
        int randomFarPos = Random.Range(0, FarPos.Count);
        time = CalculateTime(FarPos[randomFarPos].position);
        if (transform.position.x < FarPos[randomFarPos].position.x)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
        transform.LeanMove(FarPos[randomFarPos].position, time);

        return time;
    }

}
