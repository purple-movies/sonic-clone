using DraconianMarshmallows.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:: use a super-class to standarize ?
[RequireComponent(typeof(Collider2D))]
public class PetrollingEnemy : MonoBehaviour
{
    [SerializeField] Transform startPatrolPoint;
    [SerializeField] Transform endPatrolPoint;

    [SerializeField] GameObject embeddedDeathParticles;

    PlayerController playerController;

    Vector3 startPosition;
    Vector3 nextTargetPosition; 

    float tmpTime; 
    float timeToReachTarget = 5;

    private void Start()
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
        }
        else if (transform.position.Equals(endPatrolPoint.position))
        {
            setDestination(startPatrolPoint.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger : " + collision.CompareTag("Armor"));

        if (getPlayerController().Rolling && collision.CompareTag("Armor"))
            die();
    }

    private void die()
    {
        embeddedDeathParticles.transform.SetParent(transform.parent, true);
        embeddedDeathParticles.SetActive(true);
        gameObject.SetActive(false);
    }

    void setDestination(Vector3 destination)
    {
        tmpTime = 0;
        startPosition = transform.position;
        //timeToReachTarget = time;
        nextTargetPosition = destination;
    }

    private PlayerController getPlayerController()
    {
        if (playerController) return playerController;

        playerController = PlayerController.Instance as PlayerController;
        return playerController;
    }
}
