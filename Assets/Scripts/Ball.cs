using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	
	public float speed = 0;
	
	public void Awake(){
		speed = Random.Range(4f, 10f);
		
		int z = Random.Range(0, 360);
		// int y = Random.Range(0, 360);
		
		transform.eulerAngles = new Vector3(0f, 0f, z);
		
		// Console.WriteLine(transform.eulerAngles);
		// Debug.Log(speed);
	}
	
	public void Update(){
	}
	
	public void FixedUpdate(){
		Move();
	}
	
	public void OnCollisionEnter(Collision c){
		// Debug.Log(c.transform.parent.tag);
	
		if(c.transform.tag == "Boundary" || c.transform.parent.tag == "Paddle"){
			Debug.Log(c.transform.parent.tag);
			Rebound(c.contacts[0]);
		} else if(c.transform.parent.tag == "Goal1"){
			Score(1);
		} else if(c.transform.parent.tag == "Goal2"){
			Score(2);
		} 
	}
	
	private void Move(){
		rigidbody.velocity = transform.up * speed;
	}
	
	private void Rebound(ContactPoint c){

		// transform.eulerAngles = new Vector3(0f, 0f, 360f - transform.eulerAngles.z);
	
		transform.eulerAngles = new Vector3(0f, 0f, Random.Range(transform.eulerAngles.z, transform.eulerAngles.z + 180f));
		
		// Vector3 inDirection = transform.up;
		// inDirection.z = 0;
		// Vector3 outDirection = Vector3.Reflect(inDirection, c.normal);
		// outDirection.z = 0;
		// Quaternion rotation = Quaternion.FromToRotation(transform.up, outDirection) * transform.rotation;
		// rigidbody.MoveRotation(rotation);
	}
	
	private void Score(int player){
		if(player == 1){
			
		} else if(player == 2){
			
		}
	}
	
}
