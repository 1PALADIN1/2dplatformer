using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс для отрисовки шлейфа пальца по экрану
/// </summary>
public class FingerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _touchObject;

    private Vector3 _tmp;
    private float _z;

    private void Start()
    {
        _touchObject.GetComponent<ParticleSystem>().Pause();
        _touchObject.SetActive(false);
        _tmp = new Vector3();
        _z = _touchObject.transform.position.z;
    }

    private void Update ()
    {
		if (Input.touchCount > 0)
        {
            var touchPhase = Input.touches[0].phase;

            switch (touchPhase)
            {
                case TouchPhase.Began:
                    _touchObject.SetActive(true);
                    _touchObject.GetComponent<ParticleSystem>().Play();
                    break;
                case TouchPhase.Ended:
                    _touchObject.SetActive(false);
                    _touchObject.GetComponent<ParticleSystem>().Stop();
                    break;
                case TouchPhase.Moved:
                    _tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    _tmp.z = _z;
                    _touchObject.transform.position = _tmp;
                    break;
            }
        }
	}
}
