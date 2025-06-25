using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton para acceder desde otros scripts

    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        // Asegurarse de que solo haya una instancia
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoint()
    {
        score++;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Puntos: " + score;
    }
}
