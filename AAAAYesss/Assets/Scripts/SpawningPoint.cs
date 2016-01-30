using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawningPoint : MonoBehaviour {

	public GameObject gameObjectToSpawn;
    
    void Start(){
        
        if(gameObjectToSpawn != null)
		{
            Instantiate(gameObjectToSpawn, transform.position, Quaternion.identity);
        }
    }

	bool IsCollected( List<ItemType> whatWeNeed )
	{
		return false;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{

		if (other.gameObject.CompareTag ("Pick Up"))
		{
			ItemType type = other.gameObject.GetComponent<PickableItems>().ItemType;
		}

	}

	void OnTriggerExit2D(Collider2D other) 
	{

		if (other.gameObject.CompareTag ("Pick Up"))
		{

		}

	}
}
