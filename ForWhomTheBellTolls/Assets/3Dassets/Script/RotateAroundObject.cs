using System;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    [Range(0, 25)]
    public float distanceFromObject;
    public Transform objectToRotateAround;
    
    [Range(0, 360)]
    public float degreePerSecond;
    
    private void Update()
    {
        var pointPosition = objectToRotateAround.position;
        
        Vector3 direction = transform.position - pointPosition;
        direction.Normalize();

        transform.position = pointPosition + direction * distanceFromObject;
        transform.RotateAround(pointPosition, new Vector3(0, 1, 0), Time.deltaTime * degreePerSecond);
    }
}