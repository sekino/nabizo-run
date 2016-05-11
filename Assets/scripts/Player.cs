using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 4f;
	public float jumpPower = 700;
	public LayerMask groundLayer;
	public GameObject mainCamera;
	public GameObject bullet;
	private Rigidbody2D rigidbody2D;
	private Animator anim;
	private bool isGrounded;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update() {
		isGrounded = Physics2D.Linecast (
			transform.position + transform.up * 1,
			transform.position - transform.up * 0.05f,
			groundLayer
		);

		// スペースキーを押したとき
		if (Input.GetKeyDown ("space")) {
			if (isGrounded) {
				// Jumpアニメーション実行
				anim.SetBool ("Dash", false);
				anim.SetTrigger ("Jump");
				isGrounded = false;
				rigidbody2D.AddForce (Vector2.up * jumpPower);
			}
		}

		float velY = rigidbody2D.velocity.y;
		bool isJumping = velY > 0.1f ? true : false;
		bool isFalling = velY < -0.1f ? true : false;
		print (isFalling);
		anim.SetBool ("isJumping", isJumping);
		anim.SetBool ("isFalling", isFalling);

		if (Input.GetKeyDown ("left ctrl")) {
			anim.SetTrigger ("Shot");
			Instantiate (bullet, transform.position + new Vector3 (0f, 1.2f, 0f), transform.rotation);
		}
	}

	void FixedUpdate () {
		float x = Input.GetAxisRaw ("Horizontal");

		if (x != 0) {
			// 入力方向に移動
			rigidbody2D.velocity = new Vector2 (x * speed, rigidbody2D.velocity.y);
			// 画像反転
			Vector2 temp = transform.localScale;
			temp.x = x;
			transform.localScale = temp;
			// wait -> dash
			anim.SetBool ("Dash", true);

			// 画面中央から左に4移動した位置を超えたとき
			if (transform.position.x > mainCamera.transform.position.x - 4) {
				Vector3 cameraPos = mainCamera.transform.position;
				cameraPos.x = transform.position.x + 4;
				mainCamera.transform.position = cameraPos;
			}

			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
			// ユニティちゃんのposition
			Vector2 pos = transform.position;
			// ユニティちゃんのx座標を制限
			pos.x = Mathf.Clamp (pos.x, min.x + 0.5f, max.x);
			transform.position = pos;

		} else {
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
			anim.SetBool ("Dash", false);
		}
	}
}
