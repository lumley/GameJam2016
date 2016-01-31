using UnityEngine;

public class ItemPool : MonoBehaviour {
    
    public GameObject[] gameObjectsToSpawn;
    private int objectToSpawnIndex;
    private Collider2D colliderComponent;
    
    void Start(){
        colliderComponent = GetComponent<Collider2D>();
        objectToSpawnIndex = gameObjectsToSpawn.Length - 1;
        InvokeRepeating("InvokeItem", 5.0f, 15.0f);
    }
	
	private void InvokeItem(){
        
        float randomX = Random.Range(colliderComponent.bounds.min.x, colliderComponent.bounds.max.x);
        float randomY = Random.Range(colliderComponent.bounds.min.y, colliderComponent.bounds.max.y);
        Vector3 positionToSpawn = new Vector3(randomX, randomY, Player.PLAYER_SPAWNING_DEPTH);
        var instantiation = (GameObject)Instantiate(gameObjectsToSpawn[objectToSpawnIndex], positionToSpawn, Quaternion.identity);
        
        instantiation.transform.parent = transform;

        --objectToSpawnIndex; 
        
        if(objectToSpawnIndex <= 0){
            objectToSpawnIndex = gameObjectsToSpawn.Length - 1;
            CancelInvoke();
        }
    }
}
