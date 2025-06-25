using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Button retryButton;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "Puntos: " + score;

        retryButton.onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Cambia el nombre si tu escena principal se llama distinto
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Score", ScoreManager.Instance.score);
            SceneManager.LoadScene("GameOver");
        }
    }

}
