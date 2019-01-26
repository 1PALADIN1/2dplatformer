using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrike : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;          //пуля
    [SerializeField]
    private Transform startPosition;    //откуда будут появлятся пули
    
	void Update () {
		if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
        {
            if (bullet != null)
            {
                var b = Instantiate(bullet, startPosition.position, Quaternion.identity);
                b.GetComponent<BulletController>().direction = gameObject.GetComponent<PlayerMove>().Direction;
            }
                
        }
	}
}
