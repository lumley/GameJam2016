using UnityEngine;

public class SpawningPoint : MonoBehaviour {

	public GameObject gameObjectToSpawn;
    
    void Start(){
        
        if(gameObjectToSpawn != null){
            Instantiate(gameObjectToSpawn, transform.position + new Vector3(0, 0, Player.PLAYER_SPAWNING_DEPTH), Quaternion.identity);
        }
    }
}
