using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetTransformLink : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;
    // TODO:: To make this reusable you'd want to have flags for which offsets to store. 

    private float yOffset;
    private Vector3 offetPosition;

    private void Start()
    {
        // This currently assumes we're offsetting on the Y axis. 
        yOffset = transform.position.y - parentTransform.position.y;

        Debug.Log("offset : " + yOffset);
    }

    // TODO:: plug this into our update manager. 
    private void Update()
    {
        offetPosition = parentTransform.position;
        offetPosition.y += yOffset;
        transform.position = offetPosition;
    }
}
