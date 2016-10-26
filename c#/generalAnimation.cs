using UnityEngine;
using System.Collections;

public class generalAnimation : MonoBehaviour {
	public float speed;
	public float rotateX, rotateY, rotateZ // less than one.


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update (){
		this.transform.Rotate(new Vector3(rotateX, rotateY, rotateZ), speed*Time.deltaTime, Space.Self);
	}
}
