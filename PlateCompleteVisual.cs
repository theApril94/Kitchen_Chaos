using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable] public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitcheObjectSOGameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitcheObjectSOGameObjectList)
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
            
    }


    private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitcheObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }

    }
}
