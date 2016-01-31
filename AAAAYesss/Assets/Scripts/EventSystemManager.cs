using UnityEngine;
using System.Collections;
using System;

public class EventSystemManager : MonoBehaviour {

    public int playerCount;


    void OnTriggerEnter2D()
    {
        Debug.Log("AD");
        startGame();
    }

    private void startGame()
    {
        switch (playerCount)
        {
            case 2:
                GameManager.Instance.StartGameWithTwoPlayers();
                break;
            case 3:
                GameManager.Instance.StartGameWithThreePlayers();
                break;
            case 4:
                GameManager.Instance.StartGameWithFourPlayers();
                break;
        }
    }
}
