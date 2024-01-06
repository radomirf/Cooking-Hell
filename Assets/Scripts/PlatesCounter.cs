using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateDestroyed;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimerMax = 4f;
    private float spawnPlateTimer;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
           spawnPlateTimer = 0f;
            if (GameManager.Instance.isGamePlaying() && platesSpawnAmount <platesSpawnAmountMax)
            {
                platesSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(platesSpawnAmount> 0)
            {
                platesSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateDestroyed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
