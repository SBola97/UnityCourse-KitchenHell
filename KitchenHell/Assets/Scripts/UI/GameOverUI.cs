using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    private void Start(){
        GameHandler.Instance.OnStateChanged += GameHandler_OnStateChanged;
        Hide();
    }

    private void GameHandler_OnStateChanged(object sender, EventArgs e){
        if (GameHandler.Instance.IsGameOver()){
            Show();
            recipesDeliveredText.text = Mathf.Ceil(DeliveryManager.Instance.GetSuccessfulRecipesAmount()).ToString();
        }
        else{
            Hide();
        }
    }
    private void Update(){
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }

}
