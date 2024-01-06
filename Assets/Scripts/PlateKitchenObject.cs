using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngrediantAddedEventArgs> OnIngrediantAdded;
    public class OnIngrediantAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList= new List<KitchenObjectSO>();
    }

    public bool TryAddIngreadient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }

        if (kitchenObjectSOList.Contains(kitchenObjectSO))
            {
                return false;
            }
            else
            {
                kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngrediantAdded?.Invoke(this, new OnIngrediantAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
                return true;
            }
        }
      

       public List<KitchenObjectSO> GetKitchenObjectSOList() { return kitchenObjectSOList; }
    }

