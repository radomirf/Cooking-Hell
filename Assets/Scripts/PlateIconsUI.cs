using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject PlateKitchenObject;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        PlateKitchenObject.OnIngrediantAdded += PlateKitchenObject_OnIngrediantAdded;
    }

    private void PlateKitchenObject_OnIngrediantAdded(object sender, PlateKitchenObject.OnIngrediantAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == iconTemplate) continue;

            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in PlateKitchenObject.GetKitchenObjectSOList())
        {
           Transform iconTransform =  Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateSingleIconUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
