using UnityEngine;

[RequireComponent(typeof(Player))]
public class MovePlayer : MonoBehaviour {
    
    public float maxSpeed = 10.0f;
    public float minSpeed = 5.0f;
    
    private float speed;
    
    private Rigidbody2D playerRigidbody;

    private Animator playerAnimator;
    
	private int _innerPlayerNumber;
	private int playerNumber {
		get {
			if(_innerPlayerNumber == 0){
				_innerPlayerNumber = GetComponent<Player>().playerId;
			}
			return _innerPlayerNumber;
		}
	}
	private bool wasPickedUp = false;
	private bool shouldGoSlower = false;
	GameObject item;
	Collider2D itemCollider;

	public KeyCode DropButton;

	// Use this for initialization
	void Start () {
		Debug.Log(playerNumber);
	    playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
		wasPickedUp = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        string playerIdentifier = "player" + playerNumber;
        var movement = new Vector3(Input.GetAxisRaw(playerIdentifier + "Horizontal"), Input.GetAxisRaw(playerIdentifier + "Vertical"));

        AdjustPlayerSpeed();
        movement = movement * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
        //playerRigidbody.AddForce(movement);

        setAnimation(movement);

        if (wasPickedUp)
        {
            item.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.7f, gameObject.transform.position.z);
        }

        if (Input.GetKeyDown(DropButton) && wasPickedUp)
        {
            DropItem();
        }
    }

    private void AdjustPlayerSpeed()
    {
        if (shouldGoSlower)
        {
            speed = minSpeed;
        }
        else
        {
            speed = maxSpeed;
        }
    }



	void DropItem() {

		if ( item != null)
		{
			item.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
			wasPickedUp = false;
			shouldGoSlower = false;
			item.GetComponent<PickableItems>().IsPicked = false;
		}
	}

    void setAnimation(Vector3 movement) {
        
        if (movement.x == 0 && movement.y == 0)
            playerAnimator.SetInteger("Direction", 0);
        else if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
        {
            if (movement.x > 0)
                playerAnimator.SetInteger("Direction", 1);
            else if(movement.x < 0)
                playerAnimator.SetInteger("Direction", 2);
        }
        else
        {
            if (movement.y > 0)
                playerAnimator.SetInteger("Direction", 4);
            else if(movement.y < 0)
                playerAnimator.SetInteger("Direction", 3);
        }
        
    }

	void OnTriggerEnter2D(Collider2D other) 
	{
		// When the player picks up the item it is placed over his head

		GameObject somethingOnTheWay = other.gameObject;

		if (somethingOnTheWay.CompareTag ("Pick Up") && !wasPickedUp && !somethingOnTheWay.GetComponent<PickableItems>().IsPicked)
		{
			item = somethingOnTheWay;
			shouldGoSlower = true;
			wasPickedUp=true;

			item.GetComponent<PickableItems>().IsPicked = true;

		}

		if (IsMyHome(somethingOnTheWay)) 
		{
			DropItem();
			//CheckVictoryConditions();
		}
	}

	string HomeName()
	{
		return "spawnPoint" + playerNumber;
	}

	bool IsMyHome(GameObject gameObject) {

		return gameObject.CompareTag( HomeName() );

	}
		

	private void CheckVictoryConditions()
	{
		GameObject home = GameObject.FindGameObjectWithTag( HomeName() );

	
	}

	/*
	void OnTriggerExit2D (Collider2D other) 
	{
		// When the player picks up the item it is placed over his head
		if (other.gameObject.CompareTag ("Pick Up") && !wasPickedUp)
		{
			goSlower = false;
			wasPickedUp = false;
		} 
	}*/
}
