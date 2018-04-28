using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { START, PLAYING, WIN };

    [Header("UI")]
    public Text NotificationTitle;
    public Text NotificationContent;
    public GameObject ScoresPanel;

    [Header("SFX")]
    public AudioClip rotateSFX;
    public AudioClip BackgroundMusic;

    [HideInInspector]
    public GameState State;


    private CubeController _cubeController;
    private float _gameTime;

    private void Awake()
    {
        _cubeController = FindObjectOfType(typeof(CubeController)) as CubeController;
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

    // Update is called once per frame
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

            NotificationContent.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
            yield return null;
        }
    }

    void Win()
    {
        State = GameState.WIN;
    }
}
