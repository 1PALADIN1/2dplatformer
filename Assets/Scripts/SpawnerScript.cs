using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    [SerializeField]
    private GameObject _spawnObject;            //объект, который спаунется
    [SerializeField]
    private float _spawnTime = 2.0f;            //время спауна

    private GameObject _watchObject;            //переменная для наблюдения за объектом
    private bool _canInvokeSpawn = false;       //можно ли спаунить

    private void Start()
    {
        //сразу же создаём противника
        Spawn();
    }

    private void Update()
    {
        //если не за кем наблюдать, то создаём объект 
        if (_watchObject == null && _canInvokeSpawn)
        {
            Invoke("Spawn", _spawnTime);
            _canInvokeSpawn = false;
        }
    }

    /// <summary>
    /// Спаун объектов spawnObject
    /// </summary>
    public void Spawn()
    {
        if (_spawnObject != null)
        {
            //создаём новый объект и запоминаем на него ссылку
            _watchObject = Instantiate(_spawnObject, transform);
            _canInvokeSpawn = true;
        }
    }
}
