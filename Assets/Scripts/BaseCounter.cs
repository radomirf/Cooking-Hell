using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler ObjectPlaced;

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public static void ResetStaticData()
    {
        ObjectPlaced = null;
    }
    public virtual void Interact(Player player)
    {
        Debug.LogError("counter interact is not implemented");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            ObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
       
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return this.kitchenObject != null;
    }
    public virtual void InteractAlternate(Player player)
    {
       
    }
}
