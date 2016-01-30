using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    
    public int PlayerCount { get; set; }

	void Awake  () {
        
        if(Instance == null){
            Instance = this;
        }else if(Instance != this){
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(this);
	}
    
    public void StartGameWithTwoPlayers(){
        InitializeGameWithPlayers(2);
    }
    
    public void StartGameWithThreePlayers(){
        InitializeGameWithPlayers(3);
    }
    
    public void StartGameWithFourPlayers(){
        InitializeGameWithPlayers(4);
    }
    
    private void InitializeGameWithPlayers(int playerCount){
        PlayerCount = playerCount;
        SceneManager.LoadScene("GameScene");
    }
}
