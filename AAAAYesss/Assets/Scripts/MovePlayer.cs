﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class MovePlayer : MonoBehaviour {

    public float speed = 5.0f;
    
    private Rigidbody2D playerRigidbody;

    private Animator playerAnimator;
    
    private int playerNumber;
	private bool wasPickedUp = false;
	private bool goSlower = false;

	// Use this for initialization
	void Start () {
        playerNumber = GetComponent<Player>().playerId;
	    playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        string playerIdentifier = "player" + playerNumber;
        var movement = new Vector3(Input.GetAxisRaw(playerIdentifier + "Horizontal"), Input.GetAxisRaw(playerIdentifier + "Vertical"));
        
		if(goSlower){
			speed=3.0f;
		} else {
			speed = 5.0f;
		}
        movement = movement * speed * Time.deltaTime;

        playerRigidbody.MovePosition (transform.position + movement);
        //playerRigidbody.AddForce(movement);

        setAnimation(movement);
    }

    void setAnimation(Vector3 movement) {
        
        if (movement.x == 0 && movement.y == 0)
            playerAnimator.SetInteger("Direction", 0);
        else if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
        {
            if (movement.x > 0)
                playerAnimator.SetInteger("Direction", 1);
            else if(movement.x < 0)
                playerAnimator.SetInteger("Direction", 2);
        }
        else
        {
            if (movement.y > 0)
                playerAnimator.SetInteger("Direction", 4);
            else if(movement.y < 0)
                playerAnimator.SetInteger("Direction", 3);
        }
        
    }

	void OnTriggerEnter2D(Collider2D other) 
	{
		// When the player picks up the item it is placed over his head
		if (other.gameObject.CompareTag ("Pick Up") && !wasPickedUp)
		{
			goSlower = true;
			wasPickedUp=true;
		} 
	}

	void OnTriggerExit2D (Collider2D other) 
	{
		// When the player picks up the item it is placed over his head
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			goSlower = false;
			wasPickedUp = false;
		} 
	}
}
