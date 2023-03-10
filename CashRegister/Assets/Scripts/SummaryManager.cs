using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

public class SummaryManager : MonoBehaviour
{
    public TextMeshProUGUI[] newProducts = new TextMeshProUGUI[15];
    public TextMeshProUGUI[] newAmounts = new TextMeshProUGUI[15];
    public TextMeshProUGUI[] newPrices = new TextMeshProUGUI[15];
    public TextMeshProUGUI forPayText;
    private float newSum;
    private string forPay;
    private DateTime localDate = DateTime.Now;

    private int i = 0, x = 0;
    public static int p = 1;
    private string path, date, pathP;
    public static string FONT = @"D:\Czcionki/FreeSans.ttf";

    public static float staticSum;
    public static int transationAmount;
    public static float allSum;
    public static bool isBacked;

    private void Awake(){
        x = MainManager.c;
        for(i = 0; i < x; i++){
            newProducts[i].text = MainManager.finProducts[i];
            newAmounts[i].text = MainManager.finAmounts[i];
            newPrices[i].text = MainManager.finPrices[i];
        }
        newSum = MainManager.finSum;
    }

    private void Start(){
        isBacked = false;
        forPay = newSum.ToString();
        forPayText.text = forPay + " zł";
        date = localDate.ToString();
    }

    public void back(){
        SceneManager.LoadScene(1);
        isBacked = true;
    }

    public void next(){
        GenerateFile();

        path = @"D:\KasaFiskalna\Paragony\Paragon" + p + ".txt";
        using(StreamWriter sw = File.CreateText(path)){
            for(i = 0; i < x; i++){
                sw.WriteLine(date);
                sw.WriteLine(newProducts[i].text);
                sw.WriteLine(newAmounts[i].text);
                sw.WriteLine(newPrices[i].text);
            }
            p++;
        }

        staticSum = newSum;
        allSum += newSum;
        transationAmount++;
        SceneManager.LoadScene(3);
    }

    public void GenerateFile(){
        pathP = @"D:\KasaFiskalna\Paragon.pdf";
        if (File.Exists(pathP))
            File.Delete(pathP);
        using (var fileStream = new FileStream(pathP, FileMode.OpenOrCreate, FileAccess.Write))
        {
            Document document = new Document(); //lewa, prawa, gora, dol
            var writer = PdfWriter.GetInstance(document, fileStream);
            Rectangle one = new Rectangle(200, 500);
            document.SetPageSize(one);
            document.SetMargins(0, 0, 5, 0);

            document.Open();

            document.NewPage();

            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
            iTextSharp.text.Font helvetica16 = new iTextSharp.text.Font(baseFont, 10);
            for(i = 0; i < x; i++){
                Paragraph pp = new Paragraph(string.Format(newProducts[i].text + newAmounts[i].text), helvetica16);
                pp.Alignment = Element.ALIGN_CENTER;
                document.Add(pp);
            }

            Paragraph ppp = new Paragraph(string.Format("Numer stolika: ....."), helvetica16);
            ppp.Alignment = Element.ALIGN_CENTER;
            document.Add(ppp);

            document.Close();
            writer.Close();
        }
        //PrintFiles();
    }

    void PrintFiles()
    {
        UnityEngine.Debug.Log(pathP);
        if (pathP == null)
            return;

        if (File.Exists(pathP)){
            UnityEngine.Debug.Log("file found");
        }else{
            UnityEngine.Debug.Log("file not found");
            return;
        }

        ProcessStartInfo info = new ProcessStartInfo(pathP);
        info.Verb = "print";
        info.CreateNoWindow = true;
        info.WindowStyle = ProcessWindowStyle.Hidden;

        Process p = new Process();
        p.StartInfo = info;
        p.Start();
    }
}