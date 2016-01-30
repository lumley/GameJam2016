using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    private const int MAX_PLAYER_COUNT = 4; 

	void Awake(){
        var gameManager = GameManager.Instance;
        
        var playerCount = gameManager != null ? gameManager.PlayerCount : 2;
        
        for (int i=0; i< 4; ++i){
             string spawningPointTag = "spawnPoint" + (i + 1);
             var spawningPoint = GameObject.FindGameObjectWithTag(spawningPointTag);
             spawningPoint.SetActive(i < playerCount);
        }
        
    }
}
