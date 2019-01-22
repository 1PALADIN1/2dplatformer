using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float moveSpeed = 5.0f;      //скорость перемещения
    public int direction = 1;           //направление движения

	void Start () {
        //уничтожение объекта через 3 секунды после появления
        Destroy(gameObject, 3.0f);
	}
	
	void Update () {
        //перемещение по оси X
        transform.Translate(direction * moveSpeed * Time.deltaTime, 0, 0, Space.Self);
	}
}
