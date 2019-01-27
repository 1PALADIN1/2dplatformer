using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrike : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;          //пуля
    [SerializeField]
    private GameObject mina;            //мина

    [SerializeField]
    private Transform startPosition;    //откуда будут появлятся пули
    
	void Update () {
        //выстрелы
		if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
        {
            if (bullet != null)
            {
                var b = Instantiate(bullet, startPosition.position, Quaternion.identity);
                b.GetComponent<BulletController>().direction = gameObject.GetComponent<PlayerMove>().Direction;
            }
        }

        //мина
        if (Input.GetKeyDown(KeyCode.I) || Input.GetMouseButtonDown(1))
        {
            if (mina != null)
            {
                Instantiate(mina, startPosition.position, Quaternion.identity);
            }
        }
	}
}
