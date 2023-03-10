using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class AdminSummaryManager : MonoBehaviour
{
    private int transationAmount;
    private string transationAmountString;

    private float allSum;
    private string allSumString;

    private float profit;
    private string profitString;

    public TextMeshProUGUI transationAmountText;
    public TextMeshProUGUI allSumText;
    public TextMeshProUGUI profitText;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI buttText;
    public TextMeshProUGUI receiptNumberText;

    public GameObject receiptPanel;
    public GameObject textsPanel;
    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject exitButt;

    public TextMeshProUGUI[] products = new TextMeshProUGUI[15];
    public TextMeshProUGUI[] amounts = new TextMeshProUGUI[15];
    public TextMeshProUGUI[] prices = new TextMeshProUGUI[15];

    private string path, date;
    private static int p = 1;
    private int i, x, j = 0, arrLength;
    private bool onReceipts = false;

    void Awake(){
        transationAmount = SummaryManager.transationAmount;
        allSum = SummaryManager.allSum;
        allSum += 300;
        profit = allSum - 300;
    }

    private void Start(){
        transationAmountString = transationAmount.ToString();
        transationAmountText.text = transationAmountString;

        allSumString = allSum.ToString();
        allSumText.text = allSumString + " zł";

        profitString = profit.ToString();
        profitText.text = profitString + " zł";

        receiptPanel.SetActive(false);

        leftArrow.SetActive(false);
        if(transationAmount == 1){
            rightArrow.SetActive(false);
        }
    }

    public void ShowReceipts(){
        if(onReceipts == false){
            textsPanel.SetActive(false);
            receiptPanel.SetActive(true);
            exitButt.SetActive(false);
            buttText.text = "Ukryj paragony";
            onReceipts = true;
            receiptNumberText.text = "Paragon " + p + " z " + transationAmount;

            path =  @"D:\KasaFiskalna\Paragony\Paragon" + p + ".txt";

            string[] lines = File.ReadAllLines(path);

            arrLength = lines.Length;
            x = arrLength / 4;

            dateText.text = lines[j];
            for(i = 0; i < x; i++){
                j++;
                products[i].text = lines[j];
                j++;
                amounts[i].text = lines[j];
                j++;
                prices[i].text = lines[j];
                j++;
            }
            j = 0;
        }else{
            textsPanel.SetActive(true);
            receiptPanel.SetActive(false);
            buttText.text = "Pokaż paragony";
            exitButt.SetActive(true);
            onReceipts = false;
        }
    }

    public void RightArrow(){
        for(i = 0; i < x; i++){
            products[i].text = "";
            amounts[i].text = "";
            prices[i].text = "";
        }

        p++;
        leftArrow.SetActive(true);
    
        receiptNumberText.text = "Paragon " + p + " z " + transationAmount;
        path = @"D:\KasaFiskalna\Paragony\Paragon" + p + ".txt";

        string[] lines = File.ReadAllLines(path);

        arrLength = lines.Length;
        x = arrLength / 4;

        dateText.text = lines[j];
        for(i = 0; i < x; i++){
            j++;
            products[i].text = lines[j];
            j++;
            amounts[i].text = lines[j];
            j++;
            prices[i].text = lines[j];
            j++;
        }
        j = 0;

        if(p == transationAmount){
            rightArrow.SetActive(false);
        }
    }

    public void LeftArrow(){
        for(i = 0; i < x; i++){
            products[i].text = "";
            amounts[i].text = "";
            prices[i].text = "";
        }

        p--;
        rightArrow.SetActive(true);

        receiptNumberText.text = "Paragon " + p + " z " + transationAmount;
        path = @"D:\KasaFiskalna\Paragony\Paragon" + p + ".txt";

        string[] lines = File.ReadAllLines(path);

        arrLength = lines.Length;
        x = arrLength / 4;

        dateText.text = lines[j];
        for(i = 0; i < x; i++){
            j++;
            products[i].text = lines[j];
            j++;
            amounts[i].text = lines[j];
            j++;
            prices[i].text = lines[j];
            j++;
        }
        j = 0;

        if(p == 1){
            leftArrow.SetActive(false);
        }
    }

    public void Exit(){
        MenuManager.transationAmount += transationAmount;
        MenuManager.profit += profit;

        SceneManager.LoadScene(0);
    }
}
