using UnityEngine;

public class Player : MonoBehaviour {
    
    public static float PLAYER_SPAWNING_DEPTH = -0.3f;
    public float respawnTimeInSeconds = 7.0f;
    
    public int playerId;

	public void KillPlayer(){
        // Drop item
        
        // Disappear
        gameObject.SetActive(false);
        
        // Animate spawning point


        // Respawn in X seconds
        Invoke("RespawnOnStart", respawnTimeInSeconds);
        
    }
    
    public void RespawnOnStart(){
        
        var spawningPoint = GameObject.FindGameObjectWithTag("spawnPoint" + playerId);
        
        this.transform.position = spawningPoint.transform.position + new Vector3(0, 0, PLAYER_SPAWNING_DEPTH);
        gameObject.SetActive(true);
    }
}
