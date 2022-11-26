using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TextMeshProUGUI highscoreText;

    private void Start()
    {
        startButton.onClick.AddListener(StartBtnClicked);
        var pManager = PersistentManager.Instance;
        if (pManager.highScorePlayerName == null)
        {
            highscoreText.text = "";
            return;
        }
        
        highscoreText.text = $"Best Score: {pManager.highScorePlayerName}({pManager.HighScore})";
    }

    private void StartBtnClicked()
    {
        if (nameField.text.Length < 2)
            return;

        PersistentManager.Instance.playerName = nameField.text;
        SceneManager.LoadScene(1);
    }
}