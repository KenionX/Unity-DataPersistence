using System;
using System.IO;
using Models;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }

    public string playerName;
    public string highScorePlayerName;
    private int _highScore;

    public int HighScore
    {
        get => _highScore;
        set
        {
            if (value < _highScore)
                return;
            _highScore = value;
            highScorePlayerName = playerName;
            SaveHighScore();
        }
    }

    private void SaveHighScore()
    {
        if (playerName is null)
            return;

        var saveModel = new PersistentHighScoreModel()
        {
            HighScore = HighScore,
            PlayerName = highScorePlayerName
        };

        var json = JsonUtility.ToJson(saveModel);
        File.WriteAllText($"{Application.persistentDataPath}/highscore.json", json);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        var path = $"{Application.persistentDataPath}/highscore.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var model = JsonUtility.FromJson<PersistentHighScoreModel>(json);
            _highScore = model.HighScore;
            highScorePlayerName = model.PlayerName;
        }
        
        Instance = this;
    }
}