using UnityEngine;

public class Player : MonoBehaviour {
    
    public const float PLAYER_SPAWNING_DEPTH = -0.3f;
    private const string DEATH_TAG = "death";
    
    public int playerId;
    
    private GameObject deathObject;
    
    private int deathCount = 0;
    
    void Awake(){
        deathObject = GameObject.FindGameObjectWithTag(DEATH_TAG + playerId);
        DisappearSpirit();
    }

	public void KillPlayer(){
        
        // Drop item
        GetComponent<MovePlayer>().DropItem();
        
        deathObject.transform.position = transform.position;
        deathObject.SetActive(true);
        
        
        // Disappear
        gameObject.SetActive(false);
        Invoke("DisappearSpirit", 2.0f);
        
        // Animate spawning point
        // Wait X seconds
        ++deathCount;
        
        // Respawn
        Invoke("RespawnOnStart", 4.0f);
        
    }
    
    public void DisappearSpirit(){
        if(deathObject != null){
            deathObject.SetActive(false);
        }
    }
    
    public void RespawnOnStart(){
        
        deathObject.SetActive(false);
        var spawningPoint = GameObject.FindGameObjectWithTag("spawnPoint" + playerId);
        
        this.transform.position = spawningPoint.transform.position + new Vector3(0, 0, PLAYER_SPAWNING_DEPTH);
        gameObject.SetActive(true);
    }
}
