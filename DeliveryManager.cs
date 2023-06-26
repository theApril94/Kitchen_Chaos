using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSucces;
    public event EventHandler OnRecipeFailed;
     
    public static DeliveryManager Instance { get; private set; }



    [SerializeField] private RecipeListSO recipeListSO;


    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int succesfullReciepsAmount;


    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>(); 
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;


            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax) 
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
               
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);

            }
           
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];


            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // has the same number of ingridients
                bool plateContentsmatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //cycling through all ingridients in the recipe
                    bool ingridientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycling through all ingridients on the plate

                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //ingrideints matches!
                            ingridientFound = true;
                            break;
                        }
                    }

                    if (!ingridientFound)
                    {
                        // this recipe ingridient was not found on the plate

                        

                        plateContentsmatchesRecipe = false;
                    }
                }

                if (plateContentsmatchesRecipe)
                {
                    succesfullReciepsAmount++;

                    // player delivered hte correct recipe

                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSucces?.Invoke(this, EventArgs.Empty);
                    return;
                }

            }
        }

        //no matches found
        //player did not deliver correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);

    }


    public List<RecipeSO> GetWaitingReciepSOList()
    {
        return waitingRecipeSOList;
    }


    public int GetSuccessfulRecipesAmount()
    { 
        return succesfullReciepsAmount;
    }
}

