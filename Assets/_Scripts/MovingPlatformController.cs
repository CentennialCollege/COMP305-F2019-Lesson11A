using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public float speedFactor = 2.5f;
    public float height = 6.0f;
    public bool isMoving = false;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            Move();
        }
    }

    void Move()
    {
        timer += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(timer, height) * speedFactor, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isMoving = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isMoving = false;
        }
    }

}
