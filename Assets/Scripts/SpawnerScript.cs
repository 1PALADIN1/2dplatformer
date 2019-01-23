using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    [SerializeField]
    private GameObject spawnObject;         //объект, который спаунется

    private GameObject watchObject;         //переменная для наблюдения за объектом

    private void Update()
    {
        //если не за кем наблюдать, то создаём объект 
        if (watchObject == null)
        {
            Spawn();
        }
    }

    /// <summary>
    /// Спаун объектов spawnObject
    /// </summary>
    public void Spawn()
    {
        if (spawnObject != null)
        {
            //создаём новый объект и запоминаем на него ссылку
            watchObject = Instantiate(spawnObject, transform);
        }
    }
}
