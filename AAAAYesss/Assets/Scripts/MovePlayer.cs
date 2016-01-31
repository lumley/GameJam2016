using UnityEngine;

[RequireComponent(typeof(Player))]
public class MovePlayer : MonoBehaviour {
    private const string ITEM_TAG = "item";
    private const string CANCEL_BUTTON = "Cancel";
    public float maxSpeed = 10.0f;
    public float minSpeed = 5.0f;
    
    private float speed;
    
    private Rigidbody2D playerRigidbody;

    private Animator playerAnimator;
    
	private int playerNumber;
    
    private string lastCarryingItemName = "";
    
	private GameObject carryingItem;

	// Use this for initialization
	void Start () {
        playerNumber = GetComponent<Player>().playerId;
	    playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
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

        var currentItem = carryingItem;
        if (currentItem != null)
        {
            currentItem.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.7f, gameObject.transform.position.z);
        }

        if (Input.GetButton(playerIdentifier + CANCEL_BUTTON) && currentItem != null)
        {
            DropItem();
        }
    }

    private void AdjustPlayerSpeed()
    {
        speed = carryingItem != null ? minSpeed : maxSpeed;
    }

	public void DropItem() {
        var currentItem = carryingItem;
        if(currentItem != null){
            lastCarryingItemName = currentItem.name;
            currentItem.transform.position = gameObject.transform.position;
            currentItem.GetComponent<Collider2D>().enabled = true;
        }
        carryingItem = null;
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
		if (other.gameObject.CompareTag(ITEM_TAG) && carryingItem == null)
		{
            if(other.name.Equals(lastCarryingItemName)){
                lastCarryingItemName = "";
            }else{
                other.GetComponent<Collider2D>().enabled = false;
                carryingItem = other.gameObject;
            }
		}
	}
}
