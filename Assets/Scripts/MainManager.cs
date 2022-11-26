using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainManager : MonoBehaviour
{
    public Brick brickPrefab;
    public int lineCount = 6;
    public Rigidbody ball;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    public GameObject gameOverText;
    
    private bool _mStarted = false;
    private int _mPoints;
    
    private bool _mGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        SetBestScore();
        
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void SetBestScore()
    {
        var persistent = PersistentManager.Instance;
        bestScoreText.text = $"Best Score: {persistent.highScorePlayerName}({persistent.HighScore})";
    }

    private void Update()
    {
        if (!_mStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _mStarted = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                ball.transform.SetParent(null);
                ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (_mGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        _mPoints += point;
        scoreText.text = $"Score : {_mPoints}";
    }

    public void GameOver()
    {
        _mGameOver = true;
        gameOverText.SetActive(true);
        PersistentManager.Instance.HighScore = _mPoints;
    }
}
