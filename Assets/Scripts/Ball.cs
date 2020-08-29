using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    public float xInitialForce = 50;
    public float yInitialForce = 15;

    private Vector2 _trajectoryOrigin;
    public Vector2 trajectoryOrigin { get { return _trajectoryOrigin; } }

    void Start()
    {
    	rb = GetComponent<Rigidbody2D>();
    	RestartGame();
    	_trajectoryOrigin = transform.position;
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
    		rb.AddForce(force * xInitialForce);
    	}
        else
    	{
    		Vector2 force = new Vector2(xInitialForce, yRandomInitialForce).normalized;
    		rb.AddForce(force * xInitialForce);
    	}
    }

    void RestartGame()
    {
    	ResetBall();
    	Invoke("PushBall", 2);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
    	_trajectoryOrigin = transform.position;
    }
}
