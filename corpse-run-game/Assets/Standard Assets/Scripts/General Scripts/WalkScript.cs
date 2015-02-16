using UnityEngine;
using System.Collections;

public class WalkScript : MonoBehaviour {
	
	// game objects
	private GameObject objPlayer;
	private GameObject objCamera;
	private Animator animation;
	
	// input variables
	private Vector3 inputRotation;
	private Vector3 inputMovement;
	
	// identity variables
	private bool IsPlayer;
	public float moveSpeed = 100f;

	private float lastInputX;
	private float lastInputY;
	
	// calculation variables
	private Vector3 tempVector;
	private Vector3 tempVector2;
	
	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objCamera = (GameObject)GameObject.FindWithTag ("MainCamera");
		animation = GetComponent<Animator>();
		
		if (gameObject.tag == "Player") 
		{
			IsPlayer = true;
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		FindInput();
		ProcessMovement();
		
		if (IsPlayer == true) 
		{
			HandleCamera();
		}
	}
	
	void FindInput()
	{
		if (IsPlayer == true) 
		{
			FindPlayerInput ();
		} 
	}
	
	void FindPlayerInput()
	{
		// find vector to move
		inputMovement = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"),1 );

		// these are used in FindFacing() 
		lastInputX = inputMovement.x;
		lastInputY = inputMovement.y;
		
		// find vector to the mouse
		tempVector2 = new Vector3 (Screen.width * 0.5f, 0, Screen.height * 0.5f);
		
		// find position of the mouse on screen
		tempVector = Input.mousePosition;
		
		
		Debug.Log (tempVector);
		inputRotation = tempVector - tempVector2;

		FindFacing (); 
	}
	
	void ProcessMovement()
	{
		rigidbody2D.AddForce (inputMovement.normalized * moveSpeed * Time.deltaTime);
		transform.rotation = Quaternion.LookRotation(inputRotation);
		transform.eulerAngles = new Vector3(0,transform.eulerAngles.y + 180,0);
		transform.position = new Vector3(transform.position.x,transform.position.y,1);
	}
	
	void HandleCamera()
	{
		objCamera.transform.position = new Vector3(transform.position.x, transform.position.y,-1);
		
	}

	// Changes the animation sprites based on last button press
	void FindFacing()
	{
		
		if (lastInputX > 0)
		{
			animation.SetFloat("LastFaceX", 1f );
		}
		if (lastInputX < 0)
		{
			animation.SetFloat("LastFaceX", -1f );
		}
		if (lastInputX == 0)
		{
			animation.SetFloat("LastFaceX", 0f );
		}
		if (lastInputY > 0)
		{
			animation.SetFloat("LastFaceY", 1f );
		}
		if (lastInputY < 0)
		{
			animation.SetFloat("LastFaceY", -1f );
		}
		if (lastInputY == 0)
		{
			animation.SetFloat("LastFaceY", 0f );
		}
		
	}

}























