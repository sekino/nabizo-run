using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private GameObject player;
	private int speed = 10;

	// Use this for initialization
	void Start () {
		// プレイヤーを取得
		player = GameObject.FindWithTag ("UnityChan");
		print (player);
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D> ();
		// プレイヤーが向いている方向に飛ばす
		rigidbody2D.velocity = new Vector2 (speed * player.transform.lossyScale.x, rigidbody2D.velocity.y);
		Vector2 temp = transform.localScale;
		temp.x = player.transform.localScale.x;
		transform.localScale = temp;
		// 5秒後に消滅
		Destroy(gameObject, 5);
		print (player.transform.localScale.x);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
