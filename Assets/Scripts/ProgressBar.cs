
using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject progressBarGameObject;

    private IProgress progress;


    private void Start()
    {
        progress = progressBarGameObject.GetComponent<IProgress>();
        if (progress == null)
        {
            Debug.LogError("Game obj" + progressBarGameObject + " doesnt have a component that implements IProgress");
        }
        progress.OnProgressChanged += Progress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void Progress_OnProgressChanged(object sender, IProgress.OnProgressChangedEventArgs e)
    {
        Show();
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0f || e.progressNormalized == 1f ) {
            Hide();

        }else
        {
            Show();
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
