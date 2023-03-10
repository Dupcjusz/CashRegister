using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;

public class MenuManager : MonoBehaviour
{
    public static bool newDayv = false;
    private static MenuManager menuManager;

    public DateTime dt = DateTime.Now;
    public static int newMonth;
    public static int transationAmount;
    public static float profit;
    private static bool alrLoaded = false;

    private void Start() {
        if(alrLoaded == false){
            LoadInfo();
            newMonth = dt.Month;
            alrLoaded = true;
        }
    }

    public void NewDay(){
        SummaryManager.transationAmount = 0;
        SummaryManager.p = 1;
        newDayv = true;
        SceneManager.LoadScene(1);
    }

    public void Stats(){
        SceneManager.LoadScene(5);
    }

    public void Exit(){
        SaveInfo();
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    [System.Serializable]
    class SaveData{
        public int oldMonthD;
        public float profitD;
        public int transationAmountD;
    }

    public void SaveInfo(){
        SaveData data = new SaveData();
        data.profitD = profit;
        data.transationAmountD = transationAmount;
        data.oldMonthD = newMonth;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath +
        "/savefile.json", json);
    }

    public void LoadInfo(){
        string pathD = Application.persistentDataPath +
        "/savefile.json";
        if(File.Exists(pathD)){
           string json = File.ReadAllText(pathD);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            profit = data.profitD;
            transationAmount = data.transationAmountD;
            newMonth = data.oldMonthD;
        }
    }
}
