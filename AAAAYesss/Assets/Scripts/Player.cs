using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    public int playerId;

	public void KillPlayer(){
        // Drop item
        
        // Disappear
        gameObject.SetActive(false);
        
        // Animate spawning point
        // Wait X seconds
        
        // Respawn
        Invoke("RespawnOnStart", 2.0f);
        
    }
    
    public void RespawnOnStart(){
        
        var spawningPoint = GameObject.FindGameObjectWithTag("spawnPoint" + playerId);
        
        this.transform.position = spawningPoint.transform.position;
        gameObject.SetActive(true);
    }
}
