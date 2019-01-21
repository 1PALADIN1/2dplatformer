using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //выход из игры
		if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }

        //перезагрузка сцены
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
