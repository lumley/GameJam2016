using UnityEngine;

public class ItemPool : MonoBehaviour {
    
    public float spawnSpeedInSeconds = 15.0f;
    public float initialSpawnDelaySeconds = 5.0f;
    public GameObject[] gameObjectsToSpawn;
    private int objectToSpawnIndex;
    private Collider2D colliderComponent;
    
    void Start(){
        colliderComponent = GetComponent<Collider2D>();
        objectToSpawnIndex = gameObjectsToSpawn.Length - 1;
        Shuffle(gameObjectsToSpawn);
        InvokeRepeating("InvokeItem", initialSpawnDelaySeconds, spawnSpeedInSeconds);
    }
    
    public static void Shuffle<T>(T[] list)  
    {  
        int n = list.Length;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
	
	private void InvokeItem(){
        
        float randomX = Random.Range(colliderComponent.bounds.min.x, colliderComponent.bounds.max.x);
        float randomY = Random.Range(colliderComponent.bounds.min.y, colliderComponent.bounds.max.y);
        Vector3 positionToSpawn = new Vector3(randomX, randomY, Player.PLAYER_SPAWNING_DEPTH);
        var instantiation = (GameObject)Instantiate(gameObjectsToSpawn[objectToSpawnIndex], positionToSpawn, Quaternion.identity);
        
        instantiation.transform.parent = transform;

        --objectToSpawnIndex; 
        
        if(objectToSpawnIndex < 0){
            objectToSpawnIndex = gameObjectsToSpawn.Length - 1;
            CancelInvoke();
        }
    }
}
