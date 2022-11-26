using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_InputField nameField;

    private void Start()
    {
        startButton.onClick.AddListener(StartBtnClicked);
    }

    private void StartBtnClicked()
    {
        if (nameField.text.Length < 2)
            return;
    }
}