using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player1;
    private Rigidbody2D rbPlayer1;

    public PlayerController player2;
    private Rigidbody2D rbPlayer2;

    public Ball ball;
    private Rigidbody2D ballRb;
    private CircleCollider2D ballCollider;

    public int maxScore;

    private bool isDebugWindowShown = false;

    public Trajectory trajectory;

    private void Start()
    {
    	rbPlayer1 = player1.GetComponent<Rigidbody2D>();
    	rbPlayer2 = player2.GetComponent<Rigidbody2D>();
    	ballRb = ball.GetComponent<Rigidbody2D>();
    	ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    void OnGUI()
    {
    	GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.score);
    	GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.score);

    	if(GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
    	{
    		player1.ResetScore();
    		player2.ResetScore();
    		ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
    	}

    	if(player1.score == maxScore)
    	{
    		GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
    		ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
    	}
		else if(player1.score == maxScore)
    	{
    		GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
    		ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
    	}

    	if(isDebugWindowShown)
    	{
    		Color oldColor = GUI.backgroundColor;
    		GUI.backgroundColor = Color.red;
    		float ballMass = ballRb.mass;
    		Vector2 ballVelocity = ballRb.velocity;
    		float ballSpeed = ballRb.velocity.magnitude;
    		Vector2 ballMomentum = ballMass * ballVelocity;
    		float ballFriction = ballCollider.friction;

    		float impulsePlayer1X = player1.lastContactPoint.normalImpulse;
    		float impulsePlayer1Y = player1.lastContactPoint.tangentImpulse;
    		float impulsePlayer2X = player2.lastContactPoint.normalImpulse;
    		float impulsePlayer2Y = player2.lastContactPoint.tangentImpulse;

    		string debugText = 
    			"Ball mass = " + ballMass + "\n" + 
    			"Ball velocity = " + ballVelocity + "\n" +
    			"Ball speed = " + ballSpeed + "\n" +
    			"Ball momentum = " + ballMomentum + "\n" +
    			"Ball friction = " + ballFriction + "\n" +
    			"Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
    			"Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

    		GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
    		guiStyle.alignment = TextAnchor.UpperCenter;
    		GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);
    		GUI.backgroundColor = oldColor;
    	}

    	if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
    	{
    		isDebugWindowShown = !isDebugWindowShown;
    		trajectory.enabled = !trajectory.enabled;
    	}
    }
}
