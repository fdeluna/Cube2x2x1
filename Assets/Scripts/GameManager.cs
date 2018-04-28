using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { START, PLAYING, WIN };

    [Header("UI")]
    public Text NotificationTitle;
    public Text NotificationContent;
    public Text ScoresTitle;
    public Text HighScore;
    public GameObject Reset;

    [Header("SFX")]
    public AudioClip rotateSFX;
    public AudioClip BackgroundMusic;

    [HideInInspector]
    public GameState State;


    private CubeController _cubeController;
    private float _gameTime;
    private float _highscore;

    private void Awake()
    {
        _cubeController = FindObjectOfType(typeof(CubeController)) as CubeController;
        _highscore = PlayerPrefs.GetFloat("HighScore");
        HighScore.text = GetTimeHHMMSS(_highscore);
    }

    void OnEnable()
    {
        _cubeController.OnCubeSolve -= Win;
        _cubeController.OnCubeSolve += Win;
    }

    void OnDisable()
    {
        _cubeController.OnCubeSolve -= Win;
    }

    void Start()
    {
        State = GameState.START;
        NotificationTitle.text = NotificationsStrings.START_TITLE;
        NotificationContent.text = NotificationsStrings.START_CONTENT;
    }

    void Update()
    {

        switch (State)
        {
            case GameState.START:
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    State = GameState.PLAYING;
                    NotificationTitle.text = NotificationsStrings.TIMER;
                    StartCoroutine(Timer());
                }
                break;
            case GameState.WIN:
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    SceneManager.LoadScene("MainScene");
                }
                break;
        }
    }

    IEnumerator Timer()
    {
        while (State == GameState.PLAYING)
        {
            _gameTime += Time.fixedDeltaTime;

            int seconds = (int)(_gameTime % 60);
            int minutes = (int)(_gameTime / 60) % 60;
            int hours = (int)(_gameTime / 3600) & 24;

            NotificationContent.text = GetTimeHHMMSS(_gameTime);
            yield return null;
        }
    }

    void Win()
    {
        State = GameState.WIN;
        Reset.SetActive(true);
        NotificationTitle.text = NotificationsStrings.WIN;
        if (_highscore == 0 || _highscore > _gameTime)
        {
            PlayerPrefs.SetFloat("HighScore", _gameTime);
            NotificationContent.text = NotificationsStrings.NEW_HIGSCORE;
            HighScore.text = GetTimeHHMMSS(_gameTime);
            NotificationContent.color = Color.red;
            ScoresTitle.color = Color.red;
            HighScore.color = Color.red;
        }
    }

    string GetTimeHHMMSS(float time)
    {
        int seconds = (int)(time % 60);
        int minutes = (int)(time / 60) % 60;
        int hours = (int)(time / 3600) & 24;

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
