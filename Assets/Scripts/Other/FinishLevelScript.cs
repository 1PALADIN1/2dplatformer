using UnityEngine;

public class FinishLevelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameController;
    [SerializeField]
    private string _nextLevel;              //следующий уровень

    private GameController _controller;

	private void Start ()
    {
        //методы управления игровым процессом
        if (_gameController != null)
            _controller = _gameController.GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //если игрок дошёл до финиша
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (_controller != null)
            {
                if (_nextLevel.Equals("Finish")) return;
                _controller.GotoLevel(_nextLevel);
            }
        }
    }
}
