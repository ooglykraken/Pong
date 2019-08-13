using UnityEngine;
using System.Collections;

public class BallMotion : MonoBehaviour {

	private Transform blocks;
	private bool isPlayer = false;

	// Use this for initialization
	public void Awake() {
		gameObject.name = gameObject.name.Split ("("[0])[0];

		if(GameObject.FindObjectsOfType<BallMotion>().Length == 1){
			isPlayer = true;
		}

		transform.eulerAngles = new Vector3 (0f, 0f, Random.Range (0f, 90f) - 45f);//Random.Range(0, 4) * 90) - 45);
		Move();


	}

	private IEnumerator Double() {
		yield return new WaitForSeconds(1);

		PowerDrop ();
	}

	public void FixedUpdate() {

		if (isPlayer && blocks != null && blocks.childCount == 0) {
			blocks = null;
			// Gameplay.Instance ().OnStageComplete();
		}

		if(transform.eulerAngles.z < 135f && transform.eulerAngles.z > 45f){
			if(Random.Range (0,1) == 1){
				transform.eulerAngles = new Vector3(0f, 0f, Random.Range (0f, 45f));
			} else{
				transform.eulerAngles = new Vector3(0f, 0f, Random.Range (135f, 180f));
				}
		}else if(transform.eulerAngles.z < 315f && transform.eulerAngles.z > 225f){
			if(Random.Range (0,1) == 1){
				transform.eulerAngles = new Vector3(0f, 0f, Random.Range (180f, 225f));
			} else{
				transform.eulerAngles = new Vector3(0f, 0f, Random.Range (315f, 360f));
						                                    }
		}
		float startTime = Time.time;
		float startRotation = transform.rotation.z;

		if((Time.time - startTime) > 4 && transform.rotation.z == startRotation){
			transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 180f);
		}

		Move();
	}
	
	public void OnCollisionEnter(Collision c) {

		//if (!isSetByPaddle) {
			Vector3 inDirection = transform.up;
			inDirection.z = 0;
			Vector3 outDirection = Vector3.Reflect(inDirection, c.contacts[0].normal);
			outDirection.z = 0;
			Quaternion rotation = Quaternion.FromToRotation(transform.up, outDirection) * transform.rotation;
			rigidbody.MoveRotation(rotation);
		//}

		if (c.gameObject.name == "Block") {
			Destroy (c.gameObject);
			// Gameplay.Instance ().OnScore();
			// MovePaddle.Instance ().PaddleExtend ();
			if (Random.Range(0,10) <= 3 && GameObject.FindObjectsOfType<BallMotion>().Length == 1) {
				StartCoroutine (Double ());
			}
		}
		else if (c.gameObject.name == "Bottom"){
			//store player score
			if (isPlayer)
			{
				Application.LoadLevel ("GameOver");
			}
			else{
				DestroyObject (gameObject);
			}
		}else if (c.gameObject.name == "EasterEgg"){
			Application.LoadLevel("EasterEgg");
		}
		
		                                             
	}
	private void PowerDrop (){
		BallMotion ball = (Instantiate (Resources.Load ("Ball", typeof(GameObject)) as GameObject) as GameObject).GetComponent<BallMotion>();
		ball.transform.position = new Vector3(0, 0, 10);
		ball.transform.localScale *= .5f;

	}                               

	private void Move() {
		if(isPlayer){
			rigidbody.velocity = transform.up * 11f;
		}else{
			rigidbody.velocity = transform.up * 16f;
		}
	}

	public void ResetBlocks() {
		blocks = GameObject.Find ("Blocks").transform;
	}

	private static BallMotion instance = null;
	public static BallMotion Instance() {
		if (instance == null)
			instance = GameObject.FindObjectOfType<BallMotion> ();
		return instance;
	}
}
