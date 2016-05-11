using UnityEngine;
using System.Collections;

public class Hone : MonoBehaviour {

	Rigidbody2D rigidbody2D;
	public int speed = -3;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
	}
}
