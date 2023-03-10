using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainManager : MonoBehaviour
{
////////////////////// VARIABLES //////////////////////
 ///MAIN VAR///
    public TextMeshProUGUI[] products = new TextMeshProUGUI[15];
    public TextMeshProUGUI[] prices = new TextMeshProUGUI[15];
    public TextMeshProUGUI[] amounts = new TextMeshProUGUI[15];
    public TextMeshProUGUI[] textTypes = new TextMeshProUGUI[8];
    public TextMeshProUGUI pageNumber;
    public TextMeshProUGUI sumText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI nonstandardText;
    public GameObject pageI;
    public GameObject pageII;
    public GameObject pageIII;
    public GameObject[] checkmarks = new GameObject[6];
    
 ///BUTTONS VAR///
    public GameObject[] typesButton = new GameObject[8];
    public GameObject[] posButton = new GameObject[15];
    public GameObject upButton;
    public GameObject downButton;
    public GameObject applyButton;

 ///PANELS VAR///
    public GameObject errorPanel;
    public GameObject amountPanel;
    public GameObject amountErrorPanel;
    public GameObject priceErrorPanel;
    public GameObject endDayErrorPanel;
    public GameObject removePanel;
    public GameObject typesPanel;
    public GameObject nonstandardPanel;

 ///CALC VAR///
    private int cakePrice = 5; //ciasto
   ///COFFFE///
    private int coffee1Price = 4; //espresso
    private int coffee2Price = 8; //kawa latte
    private int coffee3Price = 4; //kawa czarna
    private int coffee4Price = 5; //kawa biała
    private int coffee5Price = 6; //cappyccino
    private int coffee6Price = 12; //kawa mrożona
   ///WAFFLE///
    private int wafflePrice = 2; //cena gofra bez dodatków
    private int sugarPrice = 1; //cukier puder
    private int creamPrice = 2; //bita śmietana
    private int fruitPrice = 2; //owoce
    private int saucePrice = 1; //polewa
   ///TEA///
    private int tea1Price = 3; //herbata zwykła
    private int tea2Price = 9; //rozkwitająca herbata
    private int tea3Price = 12; //herbata zimowa
   ///DRINK///
    private int drink1Price = 2; //woda
    private double drink2Price = 1.5; //sok
    private int drink3Price = 1; //lemoniada
    private int drink4Price = 8; //gorąca czekolada
    private int drink5Price = 6; //koktail owocowy
   ///ICE CREAM///
    private int iceCream1Price = 12; //deser lodowy mały
    private int iceCream2Price = 15; //deser lodowy duży
   ///NONSTANDARD///
    private int nonstandard1Price = 6; //kremowka
    private float nonstandard2Price; //niestandardowe
   ///PACG///
    private double pacg1Price = 0.5; //opakowanie
   ///OTHER///
    private int amount;
    private string product;
    private string price;
    private string sumString;
    private string amountString;
    private float sum;
    private float priceFloat;
    private bool cakeSel = false, coffeSel = false, waffleSel = false, teaSel = false, drinkSel = false, iceCreamSel = false, nonstandardSel = false;

 ///HELPING VAR///
    public static int x, c;
    private int i, y = 5, z = 5, pos, nonstandardAmount = 0, amountH, afterComma = 0, limitAmount = 0;
    private bool page2 = false, page3 = false, applyAmount = false, applyPrice = false, typeSelected = false, waffleSellected = false, stop = false, removeP = false, nonstandardP = false, inPanel = false;
    private bool sugar = false, cream = false, fruit = false, sauce = false, all = false, isComma = false;
    private bool[] waffleType = new bool[6] {true, false, false, false, false, false};

 ///SUMM VAR///
    public static string[] finProducts = new string[15];
    public static string[] finPrices = new string[15];
    public static string[] finAmounts = new string[15];
    public static float finSum;

 
///////////////////// FUNCTIONS /////////////////////

    private void Start() {  //hide useless buttons and panels when program start
        upButton.SetActive(false);
        downButton.SetActive(false);
        applyButton.SetActive(false);  
        pageII.SetActive(false);
        pageIII.SetActive(false);
        errorPanel.SetActive(false);
        amountPanel.SetActive(false);
        amountErrorPanel.SetActive(false);
        priceErrorPanel.SetActive(false);
        endDayErrorPanel.SetActive(false);
        removePanel.SetActive(false);
        typesPanel.SetActive(false);
        nonstandardPanel.SetActive(false);
        for(i = 0; i < 6; i++){
            checkmarks[i].SetActive(false);
        }

        if(c != 0 && PaymentManager.newReceipt == false && MenuManager.newDayv == false){
            sum = finSum;
            for(i = 0; i < c; i++){
                products[i].text = finProducts[i];
                amounts[i].text = finAmounts[i];
                prices[i].text = finPrices[i];
            }
            if(c > 5){
                page2 = true;
                downButton.SetActive(true);
                z += 5;
            }
            if(c > 10){
                page3 = true;
                z += 5;
            }

            sumString = sum.ToString();
            sumText.text = sumString + " zł";
        }else if(PaymentManager.newReceipt == true || MenuManager.newDayv == true){
            x = 0;
            PaymentManager.newReceipt = false;
            MenuManager.newDayv = false;
        }
    }

    private void removing(){  // remove product from the list
        prices[pos-1].text = prices[pos-1].text.Remove(prices[pos-1].text.Length - 3);
        priceFloat = float.Parse(prices[pos-1].text);
        sum -= priceFloat;
        sumString = sum.ToString();
        sumText.text = sumString + " zł";
        products[pos-1].text = "";
        prices[pos-1].text = "";
        amounts[pos-1].text = "";
        if(x == 15){
            products[x-1].text = "";
            prices[x-1].text = "";
            amounts[x-1].text = "";
            x--;
        }else{
            for(i = pos-1; i < x; i++){
                products[i].text = products[i+1].text;
                prices[i].text = prices[i+1].text;
                amounts[i].text = amounts[i+1].text;
            }
            x--;
            products[x].text = "";
            prices[x].text = "";
            amounts[x].text = "";
        }
        back();
    }

    private void actualPage(){  //shows actual page when adds products
        if(z != y){
            if(z == 10){
                pageIII.SetActive(false);
                pageII.SetActive(true);
                pageI.SetActive(false);

                upButton.SetActive(true);
                downButton.SetActive(false);
                pageNumber.text = "Strona 2 z 2";
                y=10;
            }else if(z == 15){
                pageIII.SetActive(true);
                pageII.SetActive(false);
                pageI.SetActive(false);

                upButton.SetActive(true);
                downButton.SetActive(false);
                pageNumber.text = "Strona 3 z 3";
                y=15;
            }
        }
    }

    private void addProduct(){  //adds product, prices and amount to panel 
        priceFloat = float.Parse(price);
        priceFloat *= amount;
        price = priceFloat.ToString();
        amountString = amount.ToString();
        if(x<z){
            products[x].text = product;
            prices[x].text = price + " zł";
            amounts[x].text = " x" + amountString;
            sum += priceFloat;
            x++; 
        }else{
            if(page2 == false){
                products[x].text = product;
                prices[x].text = price + " zł";
                amounts[x].text = " x" + amountString;
                x++;
                upButton.SetActive(true);
                pageI.SetActive(false);
                pageII.SetActive(true);
                page2 = true;
                pageNumber.text = "Strona 2 z 2";
                sum += priceFloat;

                y+=5;
                z+=5;
            }else if(page3 == false){
                products[x].text = product;
                prices[x].text = price + " zł";
                amounts[x].text = " x" + amountString;
                x++;
                downButton.SetActive(false);
                upButton.SetActive(true);
                pageII.SetActive(false);
                pageIII.SetActive(true);
                page3 = true;
                pageNumber.text = "Strona 3 z 3";
                sum += priceFloat;

                y+=5;
                z+=5;
            }
        }
        cakeSel = false; coffeSel = false; waffleSel = false; teaSel = false; drinkSel = false; iceCreamSel = false; nonstandardSel = false;
        if(waffleSellected == true){
            for(i = 0; i < 6; i++){
                checkmarks[i].SetActive(false);
                waffleType[i] = false;
            }
            waffleSellected = false;
            applyButton.SetActive(false);
            sugar = false; cream = false; sauce = false; fruit = false; all = false;
            wafflePrice = 2;
        }
        sumString = sum.ToString();
        sumText.text = sumString + " zł";
        amountText.text = "";
    }

    private void amountCalc(){  //shows amount panel and enables do enter value
        inPanel = true;
        amountPanel.SetActive(true);
        StartCoroutine(enterAmount());    
    }

    private void typesSelect(){ //change buttons text to correct
        inPanel = true;
        typesPanel.SetActive(true);
        if(cakeSel == true){
            textTypes[0].text = "Nr1";
            textTypes[1].text = "Nr2";
            textTypes[2].text = "Nr3";
            textTypes[3].text = "Nr4";
            textTypes[4].text = "Nr5";
            textTypes[5].text = "Nr6";
            textTypes[6].text = "Nr7";
            textTypes[7].text = "Nr8";
        }else if(coffeSel == true){
            textTypes[0].text = "Espresso";
            textTypes[1].text = "Kawa latte";
            textTypes[2].text = "Kawa czarna";
            textTypes[3].text = "Kawa biała";
            textTypes[4].text = "Cappuccino";
            textTypes[5].text = "Kawa mrożona";
            typesButton[6].SetActive(false);
            typesButton[7].SetActive(false);
        }else if(waffleSel == true){
            applyButton.SetActive(true);
            textTypes[0].text = "Gofr";
            textTypes[1].text = "Cukier puder";
            textTypes[2].text = "Bita śmietana";
            textTypes[3].text = "Owoce";
            textTypes[4].text = "Polewa";
            textTypes[5].text = "Wszystko";
            typesButton[6].SetActive(false);
            typesButton[7].SetActive(false);
        }else if(teaSel == true){
            textTypes[0].text = "Herbata";
            textTypes[1].text = "Rozkw. herbata";
            textTypes[2].text = "Herbata zimowa";
            typesButton[3].SetActive(false);
            typesButton[4].SetActive(false);
            typesButton[5].SetActive(false);
            typesButton[6].SetActive(false);
            typesButton[7].SetActive(false);
        }else if(drinkSel == true){
            textTypes[0].text = "Woda";
            textTypes[1].text = "Sok";
            textTypes[2].text = "Lemoniada";
            textTypes[3].text = "Gorąca czekolada";
            textTypes[4].text = "Koktail owocowy";
            typesButton[5].SetActive(false);
            typesButton[6].SetActive(false);
            typesButton[7].SetActive(false);
        }else if(iceCreamSel == true){
            textTypes[0].text = "Deser lodo. mały";
            textTypes[1].text = "Deser lodo. duży";
            typesButton[2].SetActive(false);
            typesButton[3].SetActive(false);
            typesButton[4].SetActive(false);
            typesButton[5].SetActive(false);
            typesButton[6].SetActive(false);
            typesButton[7].SetActive(false);
        }else if(nonstandardSel == true){
            textTypes[0].text = "Kremówka";
            textTypes[1].text = "Inne";
            typesButton[2].SetActive(false);
            typesButton[3].SetActive(false);
            typesButton[4].SetActive(false);
            typesButton[5].SetActive(false);
            typesButton[6].SetActive(false);
            typesButton[7].SetActive(false);
        }
        StartCoroutine(enterType());
    }

    private void waffleSellect(){ //set a type of waffle
        if(waffleType[0] == true && waffleType[1] == false && waffleType[2] == false && waffleType[3] == false && waffleType[4] == false){
            checkmarks[0].SetActive(true);
            price = wafflePrice.ToString();
        }
        if(waffleType[1] == true && sugar == false){
            checkmarks[1].SetActive(true);
            sugar = true;
            price = wafflePrice.ToString();
            for(i = 2; i < 6; i++){
                waffleType[i] = false;
                checkmarks[i].SetActive(false);
            }
            cream = false;
            fruit = false;
            sauce = false;
            all = false;
        }
        if(waffleType[2] == true && cream == false){
            checkmarks[2].SetActive(true);
            cream = true;
            if(sauce == true && fruit == true){
                waffleType[5] = true;
            }
            if(sugar == true){
                waffleType[1] = false;
                checkmarks[1].SetActive(false);
                sugar = false;
            }
        }
        if(waffleType[3] == true && fruit == false){
            checkmarks[3].SetActive(true);
            fruit = true;
            if(sauce == true && cream == true){
                waffleType[5] = true;
            }
            if(sugar == true){
                waffleType[1] = false;
                checkmarks[1].SetActive(false);
                sugar = false;
            }
        }
        if(waffleType[4] == true && sauce == false){
            checkmarks[4].SetActive(true);
            sauce = true;
            if(cream == true && fruit == true){
                waffleType[5] = true;
            }
            if(sugar == true){
                waffleType[1] = false;
                checkmarks[1].SetActive(false);
                sugar = false;
            }
        }
        if(waffleType[5] == true && all == false){
            checkmarks[5].SetActive(true);
            all = true;
        }
    }

    private void waffleCalc(){ //calculate a price of waffle and its name
        product = "Gofr";
        if(all == true){
            product = "Gofr wszystko";
            wafflePrice = wafflePrice + creamPrice + fruitPrice;
        }else if (sugar == true){
            product += " cukier puder";
            wafflePrice += sugarPrice;    
        }else{ 
            if(cream == true){
                product += " bita";
                wafflePrice += creamPrice;
            }
            if(fruit == true){
                product += " owoce";
                wafflePrice += fruitPrice;
            }
            if(sauce == true){
                product += " polewa";
                if(fruit == false && cream == false){
                    wafflePrice += saucePrice;
                }else{
                    wafflePrice += 0;
                }
            }
        }
        if(sugar == false && cream == false && fruit == false && sauce == false && all == false){
            product = "Gofr suchy";
        }
        price = wafflePrice.ToString();
        amountCalc();
    }

////////////////////////////////// IENUMERATORS ///////////////////////////////

    IEnumerator error(){ //shows when is more than 15 products
        inPanel = true;
        errorPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        errorPanel.SetActive(false);
        inPanel = false;
    }

    IEnumerator amountError(){ //shows when amount is 0
        inPanel = true;
        amountErrorPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        amountErrorPanel.SetActive(false);
        inPanel = false;
    }
    
    IEnumerator priceError(){ //shows when price is 0
        inPanel = true;
        priceErrorPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        priceErrorPanel.SetActive(false);
        inPanel = false;
    }

    IEnumerator endError(){
        inPanel = true;
        endDayErrorPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        endDayErrorPanel.SetActive(false);
        inPanel = false;
    }

    IEnumerator enterAmount(){ //waitnig until user set amount
        yield return new WaitUntil(() => applyAmount == true);
        if(stop == true){
            stop = false;
            applyAmount = false;
        }else{
            amountPanel.SetActive(false);
            applyAmount = false;
            inPanel = false;
            addProduct();
        }
    }

    IEnumerator enterType(){ // waiting until user set type
        if(waffleSel == true){
            yield return new WaitUntil(() => waffleSellected == true);
        }else{
            yield return new WaitUntil(() => typeSelected == true);
        }
        typesPanel.SetActive(false);
        inPanel = false;
        typeSelected = false;
        for(i = 0; i < 6; i++){
            typesButton[i].SetActive(true);  //!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
        if(waffleSel == false){
            amountCalc();
        }else{
            waffleCalc();
        }
    }

    IEnumerator enterPrice(){ //wainting until user set price
        yield return new WaitUntil(() => applyPrice == true);
        nonstandardPanel.SetActive(false);
        applyPrice = false;
        nonstandardP = false;
        inPanel = false;
        amountCalc();
    }

///////////////////////////////// BUTTONS ///////////////////////////////

 ///FUNCTIONALS///
    public void Up(){
        if(inPanel == false){
            if(y == 10){
                downButton.SetActive(true);
                upButton.SetActive(false);
                pageI.SetActive(true);
                pageII.SetActive(false);
                pageIII.SetActive(false);
                if(page3 == true){
                    pageNumber.text = "Strona 1 z 3";
                }else{
                pageNumber.text = "Strona 1 z 2";
                }
                y-=5;
            }else if(y == 15){
                downButton.SetActive(true);
                upButton.SetActive(true);
                pageI.SetActive(false);
                pageII.SetActive(true);
                pageIII.SetActive(false);
                y-=5;
                pageNumber.text = "Strona 2 z 3";
            }
        }
    }

    public void Down(){
        if(inPanel == false){
            if(y == 5){
                pageI.SetActive(false);
                pageII.SetActive(true);
                pageIII.SetActive(false);

                upButton.SetActive(true);
                if(page3 == true){
                    downButton.SetActive(true);
                    pageNumber.text = "Strona 2 z 3";
                }else{
                    downButton.SetActive(false);
                    pageNumber.text = "Strona 2 z 2";
                }
                y+=5;
            }else if(y == 10){
                pageIII.SetActive(true);
                pageII.SetActive(false);
                pageI.SetActive(false);

                upButton.SetActive(true);
                downButton.SetActive(false);
                if(page3 == true){
                    pageNumber.text = "Strona 3 z 3";
                }else{
                    pageNumber.text = "Strona 1 z 2";
                }
                y+=5;
            }
        }
    }

    public void remove(){
        if(inPanel == false){
            removeP = true;
            inPanel = true;
            removePanel.SetActive(true);
            for(i = 0; i < 15; i++){
                posButton[i].SetActive(false);
            }
            for(i = 0; i < x; i++){
                posButton[i].SetActive(true);
            }
        }
    }

    public void summary(){
        if(inPanel == false && products[0].text != ""){
            SceneManager.LoadScene(2);
            c = x;
            for(i = 0; i < x; i++){
                finProducts[i] = products[i].text;
                finPrices[i] = prices[i].text;
                finAmounts[i] = amounts[i].text;
            }
            finSum = sum;
        }
    }

    public void EndDay(){
        if(inPanel == false){
            if(products[0].text != ""){
                StartCoroutine(endError());
            }else{
                SceneManager.LoadScene(4);
            }
        }
    }

 ///PRODUCTS///
    public void Cake(){
        if(inPanel == false){
            if(x >= 15){
                StartCoroutine(error());
            }else{
                actualPage();
                cakeSel = true;
                typesButton[6].SetActive(true);
                typesButton[7].SetActive(true);
                typesSelect();
            }
        }
    }

    public void Coffee(){
        if(inPanel == false){
            if(x >= 15){
                StartCoroutine(error());
            }else{
                actualPage();
                coffeSel = true;
                typesSelect();
            }
        }
    }

    public void Waffle(){
        if(inPanel == false){
            if(x >= 15){
                StartCoroutine(error());
            }else{
                actualPage();
                waffleSel = true;
                typesSelect();
                waffleType[0] = true;
                checkmarks[0].SetActive(true);
                waffleSellect();
            }
        }
    }

    public void Tea(){
        if(inPanel == false){
            if(x >= 15){
                StartCoroutine(error());
            }else{
                actualPage();
                teaSel = true;
                typesSelect();
            }
        }
    }

    public void Drink(){
         if(inPanel == false){
            if(x >= 15){
                StartCoroutine(error());
            }else{
                actualPage();
                drinkSel = true;
                typesSelect();
            }
        }
    }

    public void IceCream(){
         if(inPanel == false){
            if(x >= 15){
                StartCoroutine(error());
            }else{
                actualPage();
                iceCreamSel = true;
                typesSelect();
            }
        }
    }

    public void Nonstandard(){
         if(inPanel == false){
            actualPage();
            nonstandardSel = true;
            isComma = false;
            afterComma  = 0;
            limitAmount = 0;
            typesSelect();
        }
    }

    public void Pacg(){
        if(inPanel == false){
            if(x >= 15){
                StartCoroutine(error());
            }else{
                actualPage();
                product = "Opakowanie";
                price = pacg1Price.ToString();
                amountCalc();
            }       
        }
    }

    public void TypeOne(){
        if(cakeSel == true){
            product = "Ciasto Nr1";
            price = cakePrice.ToString();
        }else if(coffeSel == true){
            product = "Espresso";
            price = coffee1Price.ToString();
        }else if(teaSel == true){
            product = "Herbata";
            price = tea1Price.ToString();
        }else if(drinkSel == true){
            product = "Woda";
            price = drink1Price.ToString();
        }else if(iceCreamSel == true){
            product = "Deser lodo. mały";
            price = iceCream1Price.ToString();
        }else if(nonstandardSel == true){
            product = "Kremówka";
            price = nonstandard1Price.ToString();
        }
        typeSelected = true;
    }

    public void TypeTwo(){
        if(waffleSel == true){
            if(waffleType[1] == false){
                waffleType[1] = true;
                waffleSellect();
            }else{
                waffleType[1] = false;
                checkmarks[1].SetActive(false);
                sugar = false;
            }
        }else{
            if(cakeSel == true){
                product = "Ciasto Nr2";
                price = cakePrice.ToString();
            }else if(coffeSel == true){
                product = "Kawa latte";
                price = coffee2Price.ToString();
            }else if(teaSel == true){
                product = "Rozkw. herbata";
                price = tea2Price.ToString();
            }else if(drinkSel == true){
                product = "Sok owocowy";
                price = drink2Price.ToString();
            }else if(iceCreamSel == true){
                product = "Deser lodo. duży";
                price = iceCream2Price.ToString();
            }else if(nonstandardSel == true){
                product = "Niestandardowe";
                inPanel = true;
                typesPanel.SetActive(false);
                nonstandardPanel.SetActive(true);
                nonstandardP = true;
                StartCoroutine(enterPrice());
                return;
            }
            typeSelected = true;
        }
    }

    public void TypeThree(){
        if(waffleSel == true){
            if(waffleType[2] == false){
                waffleType[2] = true;
                waffleSellect();
            }else{
                waffleType[2] = false;
                checkmarks[2].SetActive(false);
                cream = false;
                if(all == true){
                    waffleType[5] = false;
                    checkmarks[5].SetActive(false);
                    all = false;
                }
            }
        }else{
            if(cakeSel == true){
                product = "Ciasto Nr3";
                price = cakePrice.ToString();
            }else if(coffeSel == true){
                product = "Kawa czarna";
                price = coffee3Price.ToString();
            }else if(teaSel == true){
                product = "Herbata zimowa";
                price = tea3Price.ToString();
            }else if(drinkSel == true){
                product = "Lemoniada";
                price = drink3Price.ToString();
            }
            typeSelected = true;
        }
    }

    public void TypeFour(){
        if(waffleSel == true){
           if(waffleType[3] == false){
                waffleType[3] = true;
                waffleSellect();
            }else{
                waffleType[3] = false;
                checkmarks[3].SetActive(false);
                fruit = false;
                if(all == true){
                    waffleType[5] = false;
                    checkmarks[5].SetActive(false);
                    all = false;
                }
            }
        }else{
            if(cakeSel == true){
                product = "Ciasto Nr4";
                price = cakePrice.ToString();
            }else if(coffeSel == true){
                product = "Kawa biała";
                price = coffee4Price.ToString();
            }else if(drinkSel == true){
                product = "Gorąca czekolada";
                price = drink4Price.ToString();
            }
            typeSelected = true;
        }
    }

    public void TypeFive(){
        if(waffleSel == true){
            if(waffleType[4] == false){
                waffleType[4] = true;
                waffleSellect();
            }else{
                waffleType[4] = false;
                checkmarks[4].SetActive(false);
                sauce = false;
                if(all == true){
                    waffleType[5] = false;
                    checkmarks[5].SetActive(false);
                    all = false;
                }
            }
        }else{
            if(cakeSel == true){
                product = "Ciasto Nr5";
                price = cakePrice.ToString();
            }else if(coffeSel == true){
                product = "Cappuccino";
                price = coffee5Price.ToString();
            }else if(drinkSel == true){
                product = "Koktail owocowy";
                price = drink5Price.ToString();
            }
            typeSelected = true;
        }
    }

    public void TypeSix(){
        if(waffleSel == true){
            if(waffleType[5] == false){
                waffleType[2] = true;
                waffleType[3] = true;
                waffleType[4] = true;
                waffleType[5] = true;
                waffleSellect();
            }else{
                for(i = 5; i > 1; i--){
                    waffleType[i] = false;
                    checkmarks[i].SetActive(false);
                }
                all = false;
                cream = false;
                fruit = false;
                sauce = false;
            }
        }else{
            if(cakeSel == true){
                product = "Ciasto Nr6";
                price = cakePrice.ToString();
            }else if(coffeSel == true){
                product = "Kawa mrożona";
                price = coffee6Price.ToString();
            }
            typeSelected = true;
        }
    }

    public void TypeSeven(){
        if(cakeSel == true){
            product = "Ciasto Nr7";
            price = cakePrice.ToString();
        }
        typeSelected = true;
    }

    public void TypeEight(){
        if(cakeSel == true){
            product = "Ciasto Nr8";
            price = cakePrice.ToString();
        }
        typeSelected = true;
    }

 ///KEYBOARD///
    public void applyNonstandard(){
        nonstandardAmount = 0;
        nonstandard2Price = float.Parse(nonstandardText.text);
        if(nonstandard2Price <= 0){
            StartCoroutine(priceError());
        }else{
            price = nonstandard2Price.ToString();
            applyPrice = true;
            nonstandardText.text = "";
        }
    }

    public void returnNonstandard(){
        if(nonstandardText.text == ""){
            nonstandardText.text = "";
            nonstandardPanel.SetActive(false);
            nonstandardP = false;
            inPanel = false;
            isComma = false;
        }else{
            nonstandardText.text = nonstandardText.text.Remove(nonstandardText.text.Length - 1);
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
                nonstandardAmount--;
            }
        }
    }

    public void applyWaffle(){
        waffleSellected = true;
    }

    public void back(){
        inPanel = false;
        typesPanel.SetActive(false);
        removePanel.SetActive(false);
        removeP = false;
        cakeSel = false; coffeSel = false; teaSel = false; drinkSel = false; iceCreamSel = false; nonstandardSel = false;
        for(i = 0; i < 6; i++){
            typesButton[i].SetActive(true);
        }
        if(waffleSel == true){
            for(i = 0; i < 6; i++){
                waffleType[i] = false;
                checkmarks[i].SetActive(false);
            }
            sugar = false;
            cream = false;
            fruit = false;
            sauce = false;
            all = false;
        }
        waffleSel = false;
    }

    public void zero(){
        if(nonstandardP == true){
            if(isComma == true){
                if(limitAmount != 2){
                    nonstandardText.text += "0";
                    limitAmount++;
                    afterComma++;
                }
            }else{
                nonstandardText.text += "0";
                nonstandardAmount++;
            }
        }else{
            if(amountText.text.Length <= 1){
                amountText.text += "0";
            }
        }
    }

    public void one(){
        if(removeP == true){
            pos = 1;
            removing();
        }else if(nonstandardP == true){
            if(isComma == true){
                if(limitAmount != 2){
                    nonstandardText.text += "1";
                    limitAmount++;
                    afterComma++;
                }
            }else if(nonstandardAmount < 2){
                nonstandardText.text += "1";
                nonstandardAmount++;
            }
        }else{
            if(amountText.text.Length <= 1){
                amountText.text += "1";
            }
        }
    }

    public void two(){
        if(removeP == true){
            pos = 2;
            removing();
        }else if(nonstandardP == true){
            if(isComma == true){
                if(limitAmount != 2){
                    nonstandardText.text += "2";
                    limitAmount++;
                    afterComma++;
                }
            }else if(nonstandardAmount < 2){
                nonstandardText.text += "2";
                nonstandardAmount++;
            }
        }else{
            if(amountText.text.Length <= 1){
                amountText.text += "2";
            }
        }
    }

    public void three(){
        if(removeP == true){
            pos = 3;
            removing();
        }else if(nonstandardP == true){
            if(isComma == true){
                if(limitAmount != 2){
                    nonstandardText.text += "3";
                    limitAmount++;
                    afterComma++;
                }
            }else if(nonstandardAmount < 2){
                nonstandardText.text += "3";
                nonstandardAmount++;
            }
        }else{
            if(amountText.text.Length <= 1){
                amountText.text += "3";
            }
        }
    }

    public void four(){
        if(removeP == true){
            pos = 4;
            removing();
        }else if(nonstandardP == true){
            if(isComma == true){
                if(limitAmount != 2){
                    nonstandardText.text += "4";
                    limitAmount++;
                    afterComma++;
                }
            }else if(nonstandardAmount < 2){
                nonstandardText.text += "4";
                nonstandardAmount++;
            }
        }else{
            if(amountText.text.Length <= 1){
                amountText.text += "4";
            }
        }
    }

    public void five(){
        if(removeP == true){
            pos = 5;
            removing();
        }else if(nonstandardP == true){
            if(isComma == true){
                if(limitAmount != 2){
                    nonstandardText.text += "5";
                    limitAmount++;
                    afterComma++;
                }
            }else if(nonstandardAmount < 2){
                nonstandardText.text += "5";
                nonstandardAmount++;
            }
        }else{
            if(amountText.text.Length <= 1){
                amountText.text += "5";
            }
        }
    }

    public void six(){
        if(removeP == true){
            pos = 6;
            removing();
        }else if(nonstandardP == true){
            if(isComma == true){
                if(limitAmount != 2){
                    nonstandardText.text += "6";
                    limitAmount++;
                    afterComma++;
                }
            }else if(nonstandardAmount < 2){
                nonstandardText.text += "6";
                nonstandardAmount++;
            }
        }else{
            if(amountText.text.Length <= 1){
                amountText.text += "6";
            }
        }
    }

    public void seven(){
        if(removeP == true){
            pos = 7;
            removing();
        }else if(nonstandardP == true){
            if(isComma == true){
                if(limitAmount != 2){
                    nonstandardText.text += "7";
                    limitAmount++;
                    afterComma++;
                }
            }else if(nonstandardAmount < 2){
                nonstandardText.text += "7";
                nonstandardAmount++;
            }
        }else{
            if(amountText.text.Length <= 1){
                amountText.text += "7";
            }
        }
    }

    public void eight(){
        if(removeP == true){
            pos = 8;
            removing();
        }else if(nonstandardP == true){
            if(isComma == true){
                if(limitAmount != 2){
                    nonstandardText.text += "8";
                    limitAmount++;
                    afterComma++;
                }
            }else if(nonstandardAmount < 2){
                nonstandardText.text += "8";
                nonstandardAmount++;
            }
        }else{
            if(amountText.text.Length <= 1){
                amountText.text += "8";
            }
        }
    }

    public void nine(){
        if(removeP == true){
            pos = 9;
            removing();
        }else if(nonstandardP == true){
            if(isComma == true){
                if(limitAmount != 2){
                    nonstandardText.text += "9";
                    limitAmount++;
                    afterComma++;
                }
            }else if(nonstandardAmount < 2){
                nonstandardText.text += "9";
                nonstandardAmount++;

            }
        }else{
            if(amountText.text.Length <= 1){
                amountText.text += "9";
            }
        }
    }

    public void ten(){
        pos = 10;
        removing();
    }

    public void eleven(){
        pos = 11;
        removing();
    }

    public void twelve(){
        pos = 12;
        removing();
    }

    public void thirteen(){
        pos = 13;
        removing();
    }

    public void fourteen(){
        pos = 14;
        removing();
    }

    public void fifteen(){
        pos = 15;
        removing();
    }

    public void comma(){
       if(isComma == false){
            nonstandardText.text += ",";
            isComma = true;
            afterComma = 0;
            limitAmount = 0;
        }
    }

    public void returnF(){
        if(amountText.text == "" ){
            inPanel = false;
            amountPanel.SetActive(false);
            stop = true;
            applyAmount = true;
            waffleSellected = false;
            if(waffleSel == true){
                for(i = 0; i < 6; i++){
                    waffleType[i] = false;
                    checkmarks[i].SetActive(false);
                }
                sugar = false;
                cream = false;
                fruit = false;
                sauce = false;
                all = false;
            }
        }else{
            amountText.text = amountText.text.Remove(amountText.text.Length - 1);
        }
    }

    public void apply(){
        if(amountText.text != ""){
            amountH = int.Parse(amountText.text);
            if(amountH == 0){
                StartCoroutine(amountError());
            }else{
                amount = int.Parse(amountText.text);
                applyAmount = true;
            }
        }else if(amountText.text == ""){
            amount = 1;
            applyAmount = true;
        }
    }
}