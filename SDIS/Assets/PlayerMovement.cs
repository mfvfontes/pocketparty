using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private bool jump;
	private Vector3 move;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		jump = Input.GetButton ("Jump");


	}

	private void FixedUpdate ()	{

	}
}
