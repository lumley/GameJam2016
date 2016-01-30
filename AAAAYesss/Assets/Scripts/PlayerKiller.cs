using UnityEngine;
using System.Collections;

public class PlayerKiller : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
        if(other.tag.StartsWith("player")){
            // Kill player
            other.GetComponent<Player>().KillPlayer();
        }
    }
}
