using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;               //менюшка паузы

    private float _backupScaleTime;         //сохраняем таймскейл
    private PlayerMove _playerMove;         //компонента перемещения игрока

    /// <summary>
    /// На паузе ли игра (свойство)
    /// </summary>
    private bool GamePaused
    {
        get
        {
            return Time.timeScale == 0;
        }
    }

    private void Start()
    {
        _backupScaleTime = Time.timeScale;
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ExitGame();

        if (Input.GetKeyDown(KeyCode.R))
            ReloadLevel();

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 0)
                ContinueGame();
            else
                PauseGame();
        }
    }

    /// <summary>
    /// Перезагрузка сцены
    /// </summary>
    public void ReloadLevel()
    {
        if (GamePaused) Time.timeScale = _backupScaleTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void ExitGame()
    {
        if (GamePaused) Time.timeScale = _backupScaleTime;
        Application.Quit();
    }

    /// <summary>
    /// Ставит игру на паузу
    /// </summary>
    public void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            _backupScaleTime = Time.timeScale;
            //останавилваем время
            Time.timeScale = 0;
            if (_menu != null) _menu.SetActive(true);
            if (_playerMove != null) _playerMove.BlockControl = true;
        }
    }

    /// <summary>
    /// Продолжение игры
    /// </summary>
    public void ContinueGame()
    {
        Time.timeScale = _backupScaleTime;
        if (_menu != null) _menu.SetActive(false);
        if (_playerMove != null) _playerMove.BlockControl = false;
    }
}
