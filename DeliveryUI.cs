using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryUI : MonoBehaviour
{

    private const string POPUP = "Popup";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failureSprite;
        

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSucces += DeliveryManager_OnRecipeSucces;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        gameObject.SetActive(false);


    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = failureColor;
        iconImage.sprite = failureSprite;
        messageText.text = "DELIVERY\nFAILED";
    }   

    private void DeliveryManager_OnRecipeSucces(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
    }
}
