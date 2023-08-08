using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float zPosition = -10f;

    [SerializeField]
    private float timeOffset;

    [SerializeField]
    private Vector2 positionOffset;

    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    private float leftLimit, rightLimit, bottomLimit, topLimit;

    void Update()
    {
        //Camera´s start position
        Vector3 startPosition = transform.position;

        //player´s current position
        Vector3 endPosition = player.transform.position;
        endPosition.x += positionOffset.x;
        endPosition.y += positionOffset.y;
        endPosition.z = zPosition;

        //Test 1: Lerp
        //transform.position = Vector3.Lerp(startPosition, endPosition, timeOffset * Time.deltaTime);


        //Test 2: SmoothDamp (using this currently)
        transform.position = Vector3.SmoothDamp(startPosition, endPosition, ref velocity, timeOffset);

        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z
        );
    }

    private void OnDrawGizmos()
    {
        //draw a box around camera boundary
        Gizmos.color = Color.red;

        //top line
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));

        //right line
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));

        //bottom line
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));

        //left line
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));



    }
}
