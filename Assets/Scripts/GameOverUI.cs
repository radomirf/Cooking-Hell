using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredTextNumber;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private TextMeshProUGUI highScoreTextNumber;

    private void Start()
    {
        Hide();
        GameManager.Instance.OnStateChanged += OnStateChanged;
    }


    private void Awake()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
    private void OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.isGameOver())
        {
            int currentScore = DeliveryManager.Instance.GetSuccessfullRecipesDelivered();
            recipesDeliveredTextNumber.text = currentScore.ToString();
           
            int highScore = PlayerPrefs.GetInt("hiScore");
            if (PlayerPrefs.HasKey("hiScore"))
{
                if (currentScore > highScore)
                {
                    highScore = currentScore;
                    PlayerPrefs.SetInt("hiScore", highScore);
                    PlayerPrefs.Save();
                  
                }
            }
            else
            {   
                    highScore = currentScore;
                    PlayerPrefs.SetInt("hiScore", highScore);
                    PlayerPrefs.Save();
            }
            highScoreTextNumber.text = highScore.ToString();
            Show();
        }
        else
        {

            Hide();
        }
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }


}
