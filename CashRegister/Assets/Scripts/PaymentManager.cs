using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;

public class PaymentManager : MonoBehaviour
{
    public TextMeshProUGUI forPayText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI restText;
    public GameObject enterCashButton;
    public GameObject cashPanel;
    public GameObject errorPanel;
    public GameObject errorPanel2;
    public GameObject errorPanel3;
    private float forPay;
    private float cashFloat;
    private double restFloat;
    private string forPayString;
    private string cashString;
    private string restString;

    private bool alrEntered = false, isComma = false, inPanel = false;
    private int cashAmount = 0, afterComma = 0, limitAmount = 0;

    public static bool newReceipt = false;

    private void Awake() {
        forPay = SummaryManager.staticSum;
    }

    private void Start(){
        cashPanel.SetActive(false);
        errorPanel.SetActive(false);
        errorPanel2.SetActive(false);
        errorPanel3.SetActive(false);

        forPayString = forPay.ToString();
        forPayText.text = forPayString + " zł";
    }

    IEnumerator error(){
        inPanel = true;
        errorPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        errorPanel.SetActive(false);
        inPanel = false;
    }

    IEnumerator error2(){
        inPanel = true;
        errorPanel2.SetActive(true);
        yield return new WaitForSeconds(2);
        errorPanel2.SetActive(false);
        inPanel = false;
    }

    IEnumerator error3(){
        inPanel = true;
        errorPanel3.SetActive(true);
        yield return new WaitForSeconds(2);
        errorPanel3.SetActive(false);
        inPanel = false;
    }

    public void enterCash(){
        if(inPanel == false){
            isComma = false;
            afterComma  = 0;
            limitAmount = 0;
            cashAmount = 0;
            if(alrEntered == false){
                alrEntered = true;
                cashPanel.SetActive(true);
            }else{
                cashText.text = "";
                cashPanel.SetActive(true);
            }
            inPanel = true;
        }
    }
    
    public void finish(){
        if(inPanel == false){
            if(cashText.text == ""){
                StartCoroutine(error3());
            }else{
                SceneManager.LoadScene(1);
                newReceipt = true;
            }
        }
    }

    public void apply(){
        cashString = cashText.text;
        cashFloat = float.Parse(cashString);
        if(cashFloat == 0){
            StartCoroutine(error());
        }else if(cashFloat < forPay){
            StartCoroutine(error2());    
        }else{
            restFloat = cashFloat - forPay;
            restFloat = Math.Round(restFloat, 2);
            restString = restFloat.ToString();
            restText.text = restString + " zł";
            cashText.text += " zł";
            cashString = cashText.text;
            cashPanel.SetActive(false);
            inPanel = false;
            if(alrEntered == true){
                buttonText.text = "Zmień otrzymaną kwotę";
            }
        }
    }

    public void zero(){
        if(isComma == true){
            if(limitAmount != 2){
                cashText.text += "0";
                limitAmount++;
                afterComma++;
            }
        }else if(cashAmount < 3){
            cashText.text += "0";
            cashAmount++;
        }
    }

    public void one(){
        if(isComma == true){
            if(limitAmount != 2){
                cashText.text += "1";
                limitAmount++;
                afterComma++;
            }
        }else if(cashAmount < 3){
            cashText.text += "1";
            cashAmount++;
        }
    }

    public void two(){
        if(isComma == true){
            if(limitAmount != 2){
                cashText.text += "2";
                limitAmount++;
                afterComma++;
            }
        }else if(cashAmount < 3){
            cashText.text += "2";
            cashAmount++;
        }
    }

    public void three(){
        if(isComma == true){
            if(limitAmount != 2){
                cashText.text += "3";
                limitAmount++;
                afterComma++;
            }
        }else if(cashAmount < 3){
            cashText.text += "3";
            cashAmount++;
        }
    }

    public void four(){
        if(isComma == true){
            if(limitAmount != 2){
                cashText.text += "4";
                limitAmount++;
                afterComma++;
            }
        }else if(cashAmount < 3){
            cashText.text += "4";
            cashAmount++;
        }
    }

    public void five(){
        if(isComma == true){
            if(limitAmount != 2){
                cashText.text += "5";
                limitAmount++;
                afterComma++;
            }
        }else if(cashAmount < 3){
            cashText.text += "5";
            cashAmount++;
        }
    }

    public void six(){
        if(isComma == true){
            if(limitAmount != 2){
                cashText.text += "6";
                limitAmount++;
                afterComma++;
            }
        }else if(cashAmount < 3){
            cashText.text += "6";
            cashAmount++;
        }
    }

    public void seven(){
        if(isComma == true){
            if(limitAmount != 2){
                cashText.text += "7";
                limitAmount++;
                afterComma++;
            }
        }else if(cashAmount < 3){
            cashText.text += "7";
            cashAmount++;
        }
    }

    public void eight(){
        if(isComma == true){
            if(limitAmount != 2){
                cashText.text += "8";
                limitAmount++;
                afterComma++;
            }
        }else if(cashAmount < 3){
            cashText.text += "8";
            cashAmount++;
        }
    }

    public void nine(){
        if(isComma == true){
            if(limitAmount != 2){
                cashText.text += "9";
                limitAmount++;
                afterComma++;
            }
        }else if(cashAmount < 3){
            cashText.text += "9";
            cashAmount++;
        }
    }

    public void comma(){
        if(isComma == false){
            cashText.text += ",";
            isComma = true;
            afterComma = 0;
            limitAmount = 0;
        }
    }
    
    public void returnF(){
        if(cashText.text == "" ){
            cashPanel.SetActive(false);
            isComma = false;
            inPanel = false;
        }else{
            cashText.text = cashText.text.Remove(cashText.text.Length - 1);
            if(isComma == true){
                afterComma--;
                limitAmount--;
                if(afterComma < 0){
                    isComma = false;
                    limitAmount = 0;
                }
                if(afterComma == 0){
                    isComma = true;
                }
            }else{
                cashAmount--;
            }
        }
    }
}