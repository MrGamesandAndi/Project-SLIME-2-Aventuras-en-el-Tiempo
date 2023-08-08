using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApatosaurusController : MonoBehaviour
{
    public float speed;
    private Transform currentPoint;
    public Transform[] points;
    public int selectedPoint;
    private bool canMove = false;
    private bool movingRight = true;
    private Transform player;


    void Start()
    {
        currentPoint = points[selectedPoint];
    }

    void Update()
    {
        if(canMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, currentPoint.position, Time.deltaTime * speed);

            if (gameObject.transform.position == currentPoint.position)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                selectedPoint++;

                if (selectedPoint == points.Length)
                {
                    selectedPoint = 0;
                    gameObject.transform.eulerAngles = new Vector3(0, -180, 0);
                    player.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = !movingRight;
                }
                currentPoint = points[selectedPoint];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Transform>();
            canMove = true;
            gameObject.GetComponent<Animator>().SetTrigger("Move");
            player.SetParent(gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.eulerAngles = new Vector3(0, 0, 0);
            player.SetParent(null);
        }
    }
}
