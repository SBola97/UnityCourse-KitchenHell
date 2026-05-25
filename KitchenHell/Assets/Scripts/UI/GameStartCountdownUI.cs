using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator animator;
        private int previousCountdownNumber;
    private const string NUMBER_POPUP = "NumberPopup";

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        GameHandler.Instance.OnStateChanged += GameHandler_OnStateChanged;
        Hide();
    }

    private void GameHandler_OnStateChanged(object sender, EventArgs e){
        if(GameHandler.Instance.IsCountdownToStartActive()){
            Show();
        }
        else{
            Hide();
        }
    }
    private void Update() {
        int countdownNumber = Mathf.CeilToInt(GameHandler.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();
        if (countdownNumber != previousCountdownNumber){
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountDownSound();
        }
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }   
}
