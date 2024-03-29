using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        Instance= this;
        soundEffectsButton.onClick.AddListener(() =>
       {
           SoundManager.Instance.ChangeVolume();
           UpdateVisual();
       });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
              UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameResumed += OnGameResumed;
        UpdateVisual();
        Hide();
    }

    private void OnGameResumed(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "SOUND EFFECTS: " + Mathf.Round(SoundManager.Instance.GetVolume() *10f);
        musicText.text = "MUSIC: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
