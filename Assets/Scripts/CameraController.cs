using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject watchObject;          //объект, за которым должна следить камера

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (watchObject != null)
        {
            transform.position = new Vector3(watchObject.transform.position.x, watchObject.transform.position.y, transform.position.z);
        }
	}
}
