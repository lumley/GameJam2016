using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    public int playerNumber;
    public int velocityBorder; //determines teh maximum value of velocity for the player to be considered standing/idling
    public float speed = 5.0f;
    
    private Rigidbody2D playerRigidbody;

	// Use this for initialization
	void Start () {
	   playerRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        string playerIdentifier = "player" + playerNumber;
        var movement = new Vector3(Input.GetAxisRaw(playerIdentifier + "Horizontal"), Input.GetAxisRaw(playerIdentifier + "Vertical"));
        
        movement = movement.normalized * speed * Time.deltaTime;
        
        playerRigidbody.MovePosition (transform.position + movement);

        setAnimation(playerIdentifier);
        Debug.Log(Input.GetAxisRaw(playerIdentifier + "Horizontal"));
    }

    void setAnimation(string playerIdentifier) {
        //Vertical
        if(Input.GetAxisRaw(playerIdentifier + "Vertical") > 0)
            this.gameObject.GetComponent<Animator>().SetInteger("Direction", 4);
        if(Input.GetAxisRaw(playerIdentifier + "Vertical") < 0)
            this.gameObject.GetComponent<Animator>().SetInteger("Direction", 3);
        //Horizontal
        if (Input.GetAxisRaw(playerIdentifier + "Horizontal") > 0)
            this.gameObject.GetComponent<Animator>().SetInteger("Direction", 1);
        if(Input.GetAxisRaw(playerIdentifier + "Horizontal") < 0)
            this.gameObject.GetComponent<Animator>().SetInteger("Direction", 2);
        //when player moves diagonally, only the horizontal part of it is animated (modifiable by order of //Vertical and //Horizontal
        float velocityX = playerRigidbody.velocity.x;
        float velocityY = playerRigidbody.velocity.y;
        float realVelocity = Mathf.Sqrt((velocityX * velocityX) + (velocityY * velocityY)); //Pythagoras
        if(
           realVelocity < velocityBorder ||
           (Input.GetAxisRaw(playerIdentifier + "Horizontal") == 0 &&
           Input.GetAxisRaw(playerIdentifier + "Vertical") == 0)
          )
            this.gameObject.GetComponent<Animator>().SetInteger("Direction", 0);
    }
}
