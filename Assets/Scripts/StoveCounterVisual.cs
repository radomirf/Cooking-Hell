using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnOffVisual;
    [SerializeField] private GameObject particlesVisual;


    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStaTeChanged;

    }
    private void StoveCounter_OnStaTeChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Fried || e.state== StoveCounter.State.Frying;
        stoveOnOffVisual.SetActive(showVisual);
        particlesVisual.SetActive(showVisual);  
    }
}
