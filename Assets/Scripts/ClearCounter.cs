using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{ 
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   
    public override void Interact(Player player)
    { 
        if (!HasKitchenObject())
        { //no KitchenObject on Counter
            if (player.HasKitchenObject())
            {//Player carries a KitchenObject
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }else
            {
                //player isnt carrying anything
            }
        }
        else
        { //KitchenObject On Counter
            if (player.HasKitchenObject())
            { //Player is carrying a kitchenObject
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {//player has a plate
                    if (plateKitchenObject.TryAddIngreadient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                { //player carrying something else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //theres a plate on the counter
                        if (plateKitchenObject.TryAddIngreadient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }else
            {  //player isnt carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
  

}
