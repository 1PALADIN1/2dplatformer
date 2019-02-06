using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;               //менюшка паузы
    [SerializeField]
    private bool _isGameScene = true;       //игровая ли сцена с участием игрока?

    private float _backupScaleTime;         //сохраняем таймскейл
    private PlayerMove _playerMove;         //компонента перемещения игрока
    private bool _isGameFinish = false;
    private GameObject _finishObject;

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
        if (_isGameScene) _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        _finishObject = GameObject.FindGameObjectWithTag("FinishGame");
        if (_finishObject != null)
            _finishObject.SetActive(false);
    }

    void Update ()
    {
        if (_isGameScene)
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

        if (_isGameFinish && Input.GetKeyDown(KeyCode.E))
        {
            GotoLevel("MainMenu");
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

    /// <summary>
    /// Переход на уровень по названию
    /// </summary>
    /// <param name="levelName">Название уровня</param>
    public void GotoLevel(string levelName)
    {
        if (GamePaused) Time.timeScale = _backupScaleTime;

        if (levelName.Equals("Finish")) FinishGame();
        else
            SceneManager.LoadScene(levelName);
    }

    private void FinishGame()
    {
        _isGameScene = false;
        _isGameFinish = true;
        _finishObject.SetActive(true);
    }
}
