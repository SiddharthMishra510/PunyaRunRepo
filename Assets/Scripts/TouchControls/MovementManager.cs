using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    Left,
    Right,
    Up,
    Down,
    None
}

public class MovementManager : MonoBehaviour
{

    private Directions currentDirection = Directions.None;
    private Directions nextDirection = Directions.None;
    public void UpdateMovement(Directions newDirection)
    {
        if (currentDirection == newDirection)
            return;
        if (IsPerpendicular(currentDirection, newDirection))
        {
            nextDirection = newDirection;
        }
        else
        {
            nextDirection = newDirection;
            Move();
        }
    }


    public void OnEnterIntersectionOfRoad()
    {
        if (true) // write condition for validating move 
        {
            Move();
        }
    }

    private void Move()
    {
        if(nextDirection == Directions.None)
        {
            return;
        }
        // Move player in actual
        currentDirection = nextDirection;
        nextDirection = Directions.None;
    }


    private bool IsPerpendicular(Directions direction1, Directions direction2)
    {
        bool direction1IsHorizontal;
        bool direction2IsHorizontal;

        if (direction1 == Directions.Left || direction1 == Directions.Right)
        {
            direction1IsHorizontal = true;
        }
        else
        {
            direction1IsHorizontal = false;
        }

        if (direction2 == Directions.Left || direction2 == Directions.Right)
        {
            direction2IsHorizontal = true;
        }
        else
        {
            direction2IsHorizontal = false;
        }


        if (direction1IsHorizontal == direction2IsHorizontal)
        {
            return false;
        }
        else
        {
            return true;
        }


    }
}
