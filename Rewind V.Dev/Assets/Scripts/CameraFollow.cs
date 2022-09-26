using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private Transform mouseTarget;

    private float smoothSpeed = 0.925f;

    private float mouseSensitivity;
    private GameObject player;


    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void LateUpdate()
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer > 1)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }

            


        


    }

    public void SetTarget()
    {
        target = GameObject.Find("Player").transform;
    }


}
