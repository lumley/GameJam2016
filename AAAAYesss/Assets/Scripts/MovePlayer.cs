using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    public int playerNumber;
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
    }
}
