using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess, OnRecipeFailed;
   
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int maxRecipesInWaitingList = 4;
    private int successfullRecipesDelivered = 0;

    private void Awake()
    {
        Instance = this;    
        waitingRecipeList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer < 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (GameManager.Instance.isGamePlaying() && waitingRecipeList.Count < maxRecipesInWaitingList)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach(RecipeSO recipe in waitingRecipeList)
        {
            if(recipe.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {//has same number of ingredients

                bool plateContentMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in recipe.kitchenObjectSOList)
                {//ingredients in recipes
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {//ingredients on plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {//ingredient matches
                            ingredientFound = true;
                            break;
                        }

                    }
                    if (!ingredientFound)
                    {//ingredient is missing from the plate
                        plateContentMatchesRecipe = false;
                    }
                }
                if (plateContentMatchesRecipe)
                {//correct recipe
                    Debug.Log("Correct Recipe was delivered");
                    waitingRecipeList.Remove(recipe);
                    successfullRecipesDelivered++;
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
       OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
    public List<RecipeSO> GetWaitingRecipeList()
    {
        return waitingRecipeList;
    }
    public int GetSuccessfullRecipesDelivered()
    {
        return successfullRecipesDelivered;
    }

}
