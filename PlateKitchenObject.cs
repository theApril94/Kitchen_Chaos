using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngridientAddedEventArgs> OnIngridientAdded;
    public class OnIngridientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;



    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();    
    }


    public bool TryAddIngridient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenSOList.Contains(kitchenObjectSO))
        {
            //not a vaolid ingirdient
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //already ahs this type of kitchenobjectso
            return false;
        }
        else 
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngridientAdded?.Invoke(this, new OnIngridientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
        
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }



}
