using UnityEngine;

public class Player : MonoBehaviour {
    
    public const float PLAYER_SPAWNING_DEPTH = -0.3f;
    
    public int playerId;

	public void KillPlayer(){
        // Drop item
        GetComponent<MovePlayer>().DropItem();
        
        // Disappear
        gameObject.SetActive(false);
        
        // Animate spawning point
        // Wait X seconds
        
        // Respawn
        Invoke("RespawnOnStart", 2.0f);
        
    }
    
    public void RespawnOnStart(){
        
        var spawningPoint = GameObject.FindGameObjectWithTag("spawnPoint" + playerId);
        
        this.transform.position = spawningPoint.transform.position + new Vector3(0, 0, PLAYER_SPAWNING_DEPTH);
        gameObject.SetActive(true);
    }
}
