using UnityEngine;

public class SpawningPoint : MonoBehaviour {

	public GameObject gameObjectToSpawn;
    
    void Start(){
        
        if(gameObjectToSpawn != null){
            Instantiate(gameObjectToSpawn, transform.position, Quaternion.identity);
        }
    }
}
