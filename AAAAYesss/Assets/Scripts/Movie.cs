using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movie : MonoBehaviour {

	Image myImageComponent;
	public Sprite image1; //Drag your first sprite here in inspector.
	public Sprite image2; //Drag your second sprite here in inspector.
	public Sprite image3; //Drag your first sprite here in inspector.
	public Sprite image4; //Drag your second sprite here in inspector.
	public Sprite image5; //Drag your first sprite here in inspector.
	public Sprite image6; //Drag your first sprite here in inspector.
	public Sprite image7; //Drag your first sprite here in inspector.

	private int countPage;

	// Use this for initialization
	void Start () {
		countPage = 0;
		myImageComponent = GetComponent<Image>(); //Our image component is the one attached to this gameObject.
		myImageComponent.sprite = image1;
		RectTransform rt = GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2( Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.Space) ||
			Input.GetKeyUp(KeyCode.JoystickButton0))
		{
			countPage = countPage+1;
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow) ||
			Input.GetKeyUp(KeyCode.JoystickButton1))
		{
			countPage = countPage-1;
		}

		if(countPage==0){
			myImageComponent.sprite = image1;
		} else if(countPage==1){
			myImageComponent.sprite = image2;
		} else if(countPage==2){
			myImageComponent.sprite = image3;
		} else if(countPage==3){
			myImageComponent.sprite = image4;
		} else if(countPage==4){
			myImageComponent.sprite = image5;
		} else if(countPage==5){
			myImageComponent.sprite = image6;
		} else if(countPage==6){
			myImageComponent.sprite = image7;
		} else if(countPage==7){
			//GAME STARTS
			SceneManager.LoadScene("MainScene");
		}

	}

	void onGUI(){
		
	}
}
