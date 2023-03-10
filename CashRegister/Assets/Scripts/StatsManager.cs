using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;
using System.Diagnostics;

public class StatsManager : MonoBehaviour
{
    public TextMeshProUGUI monthText;
    public TextMeshProUGUI transationAmountText;
    public TextMeshProUGUI profitText;
    public DateTime dt = DateTime.Now;
    public string data = DateTime.Now.ToShortDateString();

    private string path;
    private string monthSting;
    private string transationAmountString;
    private string profitString;

    private void Start(){

        if(MenuManager.newMonth == 1){
            monthText.text = "styczeń";
        }else if(MenuManager.newMonth == 2){
            monthText.text = "luty";
        }else if(MenuManager.newMonth == 3){
            monthText.text = "marzec";
        }else if(MenuManager.newMonth == 4){
            monthText.text = "kwiecień";
        }else if(MenuManager.newMonth == 5){
            monthText.text = "maj";
        }else if(MenuManager.newMonth == 6){
            monthText.text = "czerwiec";
        }else if(MenuManager.newMonth == 7){
            monthText.text = "lipiec";
        }else if(MenuManager.newMonth == 8){
            monthText.text = "sierpień";
        }else if(MenuManager.newMonth == 9){
            monthText.text = "wrzesień";
        }else if(MenuManager.newMonth == 10){
            monthText.text = "październik";
        }else if(MenuManager.newMonth == 11){
            monthText.text = "listopad";
        }else if(MenuManager.newMonth == 12){
            monthText.text = "grudzień";
        }

        transationAmountString = MenuManager.transationAmount.ToString();
        profitString = MenuManager.profit.ToString();

        transationAmountText.text = transationAmountString;
        profitText.text = profitString + " zł";
    }

    public void OpenFolder(){
        path = @"D:\KasaFiskalna";
        Process.Start(path);
    }

    public void DownloadData(){
        MenuManager.transationAmount = 0;
        MenuManager.profit = 0;
        path = @"D:\KasaFiskalna\Podsumowania\Podsumowanie_" + data + ".txt";
        using(StreamWriter sw = File.CreateText(path)){
            sw.WriteLine("Wykonano: " + dt);
            sw.WriteLine("Liczba transakcji: " + transationAmountString);
            sw.WriteLine("Łączny zysk: " + profitString + " zł");
        }
        SceneManager.LoadScene(0);
    }

    public void Back(){
        SceneManager.LoadScene(0);
    }
}
