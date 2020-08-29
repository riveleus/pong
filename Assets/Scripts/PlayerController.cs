using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public KeyCode upButton;
	public KeyCode downButton;
    public float speed;
    public float yBoundary;
    private Rigidbody2D rb;
    private int _score;
	public int score { get { return _score; } }
    private ContactPoint2D _lastContactPoint;
    public ContactPoint2D lastContactPoint { get { return _lastContactPoint; } }
	public PlayerController otherPlayer;
	public Ball ball;
	public GameManager gameManager;

    void Start()
    {
    	rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    	Vector2 velocity = rb.velocity;

    	if(Input.GetKey(upButton))
    	{
    		velocity.y = speed;
    	}
		else if(Input.GetKey(downButton))
    	{
    		velocity.y = -speed;
    	}
		else
    	{
    		velocity.y = 0;
    	}

    	rb.velocity = velocity;

    	Vector3 position = transform.position;

    	if(position.y > yBoundary)
    	{
    		position.y = yBoundary;
    	}
		else if(position.y < -yBoundary)
    	{
    		position.y = -yBoundary;
    	}

    	transform.position = position;
    }

    public void IncrementScore()
    {
    	_score++;
    }

    public void ResetScore()
    {
    	_score = 0;
    }

	public void GetPowerUp()
	{
		StartCoroutine(PowerUp());
	}

	IEnumerator PowerUp()
	{
		Vector2 scale = transform.localScale;
		scale.y += 1;
		transform.localScale = scale;
		
		yield return new WaitForSeconds(10);

		scale.y -= 1;
		transform.localScale = scale;

		yield return null;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
    	if(collision.gameObject.name.Equals("Ball"))
    	{
    		_lastContactPoint = collision.GetContact(0);
    	}
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "FireBall")
		{
			otherPlayer.IncrementScore();

			if(otherPlayer.score < gameManager.maxScore)
			{
				ball.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
				other.gameObject.SendMessage("SpawnBall", 0, SendMessageOptions.RequireReceiver);
			}
		}
	}
}
