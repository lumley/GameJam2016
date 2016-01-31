using UnityEngine;
using System.Collections;

public class PickableItems : MonoBehaviour {

	private bool wasPickedUp = false;
	GameObject player;
	private float finalPositionx = 0f;
	private float finalPositiony = 0f;

	public bool IsPicked;

	public ItemType ItemType;

	// Update is called once per frame
	void Update () {
		if(wasPickedUp)
		{
			gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+0.7f, player.transform.position.z);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		// When the player picks up the item it is placed over his head
		if (other.gameObject.CompareTag ("Player") && !wasPickedUp)
		{
			player = other.gameObject;
			wasPickedUp=true;
		}
	}
}
