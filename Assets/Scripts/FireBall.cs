using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D rb;

    public float xInitialForce = 50;
    public float yInitialForce = 20;
    public float speed;

    void Start()
    {
    	rb = GetComponent<Rigidbody2D>();
    	Invoke("SpawnBall", 5);
    }

    void ResetBall()
    {
    	transform.position = Vector2.zero;
    	rb.velocity = Vector2.zero;
    }

    void PushBall()
    {
    	float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
    	float randomDirection = Random.Range(0, 2);

    	if(randomDirection < 1.0f)
    	{
            Vector2 force = new Vector2(-xInitialForce, yRandomInitialForce).normalized;
    		rb.AddForce(force * speed);
    	}
        else
    	{
    		Vector2 force = new Vector2(xInitialForce, yRandomInitialForce).normalized;
    		rb.AddForce(force * speed);
    	}
    }

    void SpawnBall()
    {
    	ResetBall();
    	PushBall();
    }
}
