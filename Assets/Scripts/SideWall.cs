using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    public PlayerController player;

    [SerializeField]
    private GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
    	if(other.name == "Ball")
    	{
    		player.IncrementScore();

    		if(player.score < gameManager.maxScore)
    		{
    			other.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
				player.GetPowerUp();
    		}
    	}

		if(other.name == "FireBall")
		{
			other.gameObject.SendMessage("SpawnBall", 0, SendMessageOptions.RequireReceiver);
		}
    }
}
