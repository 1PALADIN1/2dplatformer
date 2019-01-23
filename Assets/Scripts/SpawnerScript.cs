using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    [SerializeField]
    private GameObject spawnObject;

	void Start () {
        Spawn();
	}

    public void Spawn()
    {
        if (spawnObject != null)
            Instantiate(spawnObject, transform);
    }
}
