using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public event EventHandler OnPlayerGrabbdObject;


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
           
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerGrabbdObject?.Invoke(this, EventArgs.Empty);
        }
            
    }
   
}
