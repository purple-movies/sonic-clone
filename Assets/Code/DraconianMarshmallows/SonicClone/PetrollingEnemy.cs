using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrollingEnemy : MonoBehaviour
{
    [SerializeField] Transform startPatrolPoint;
    [SerializeField] Transform endPatrolPoint;

    Vector3 startPosition;
    Vector3 nextTargetPosition; 

    float tmpTime; 
    float timeToReachTarget = 5;

    void Start()
    {
        startPosition = transform.position = startPatrolPoint.transform.position;
        nextTargetPosition = endPatrolPoint.position;
    }

    // TODO:: plug into UpdateManager. 
    void Update()
    {
        tmpTime += Time.deltaTime / timeToReachTarget;
        transform.position = Vector2.Lerp(startPosition, nextTargetPosition, tmpTime);

        if (transform.position.Equals(startPatrolPoint.position))
        {
            setDestination(endPatrolPoint.position);
            //nextTargetPosition = endPatrolPoint.position;
        }
        else if (transform.position.Equals(endPatrolPoint.position))
        {
            setDestination(startPatrolPoint.position);
            //nextTargetPosition = startPatrolPoint.position;
        }
    }

    void setDestination(Vector3 destination)
    {
        tmpTime = 0;
        startPosition = transform.position;
        //timeToReachTarget = time;
        nextTargetPosition = destination; 
    }
}
