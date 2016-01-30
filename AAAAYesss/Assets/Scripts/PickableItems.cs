using UnityEngine;
using System.Collections;

public class PickableItems : MonoBehaviour {

	private bool wasPickedUp = false;
	GameObject player;
	private float finalPositionx = 0f;
	private float finalPositiony = 0f;

	// Use this for initialization
	void Start () {
		wasPickedUp = false;

	}

	// Update is called once per frame
	void Update () {
		if(wasPickedUp)
		{
			gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+0.7f, player.transform.position.z);
		}

		// TODO change the button input
		if(Input.GetKeyDown(KeyCode.Space) && wasPickedUp){


			gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
			wasPickedUp= false;
		}
	
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		// When the player picks up the item it is placed over his head
		if (other.gameObject.CompareTag ("Player") && !wasPickedUp)
		{
			player = other.gameObject;
			wasPickedUp=true;
			//player.SetHeldIten(this);
		}

		// When the item is brounght to the spawn point, it goes at the center of the spawn
		if(other.gameObject.CompareTag ("spawnPoint1") || other.gameObject.CompareTag ("spawnPoint2") || other.gameObject.CompareTag ("spawnPoint3") || other.gameObject.CompareTag ("spawnPoint4")){
			// adding values for the items to be placed in different locations
			finalPositionx=finalPositionx+0.5f;
			finalPositiony=finalPositiony+0.5f;
			other.gameObject.transform.position = new Vector3(gameObject.transform.position.x+finalPositionx, gameObject.transform.position.y+finalPositiony, gameObject.transform.position.z);
			wasPickedUp = false;
			//player.SetHeldIten(null);

		}
		
	}
}
