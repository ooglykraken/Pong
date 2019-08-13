using UnityEngine;
using System.Collections;

public class Paddle1 : MonoBehaviour {

	public void Update(){
		if(Input.GetKey("w")){
			Move(1);
		} else if (Input.GetKey("s")){
			Move(-1);
		}
	}
	
	private void Move(int direction){
		transform.position = transform.position + (new Vector3(0f, .5f, 0f) * direction);
	}
}
