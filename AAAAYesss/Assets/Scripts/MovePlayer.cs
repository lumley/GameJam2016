using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    public int playerNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        switch (playerNumber){
            case 1:
                transform.position += new Vector3(Input.GetAxisRaw("player1Horizontal"), Input.GetAxisRaw("player1Vertical"));
                break;
            case 2:
                transform.position += new Vector3(Input.GetAxisRaw("player2Horizontal"), Input.GetAxisRaw("player2Vertical"));
                break;
            case 3:
                transform.position += new Vector3(Input.GetAxisRaw("player3Horizontal"), Input.GetAxisRaw("player3Vertical"));
                break;
            case 4:
                transform.position += new Vector3(Input.GetAxisRaw("player4Horizontal"), Input.GetAxisRaw("player4Vertical"));
                break;
        }
    }
}
