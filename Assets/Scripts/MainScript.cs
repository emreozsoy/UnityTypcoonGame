using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Globalization;


/*
Hi,these codes are the beginning of a software developer adventure, so it's not even "clean code" or something have fun!!!

Merhaba, bu kodlar bir yazılım geliştirici macerasını başlangıcıdır, bu yüzden temiz koda yakın bir şey göremeyeceksiniz, iyi eğlenceler!!

*/
public class MainScript : MonoBehaviour
{
    
    [Header("Gold Settings")]
    private float goldTimer=1;
    public static float Gold=10000;
    public float goldMining=0.01f;
    public int goldMiningLv=1;
    public GameObject GoldClickEffect;
    public Transform ClickPosition;
    public GameObject ClickEffectText;
    public Text GoldText2;
    
    [Header("Army Settings")] //Her a ifadesi asker seviyesine kar��l�k geliyor. Not: Atli S�valye = Valk�r
    public int kilicliasker; //a1
    public int okcuasker;   //a2
    public int atliasker; //a3
    public int pantheon; //a4
    public int sovalye;// a5
    public int atlisovalye;//a6
    public int mancinik;//a7
    public int kocbasi;//a8
    public int sifaci;//a9
    public int atliokcu;//a10

    public int a1, a2, a3, a4, a5, a6, a7, a8, a9, a10;
    
    public int randomint;
    public float randomTimer;
    public float SavasGucu;

    [Header("Random Create")]
    public GameObject VillagerWalkingRight;
    public GameObject VillagerWalkingRight2;

    public GameObject VillagerWalkingLeft;

    public GameObject VillagerWaiting;
    public GameObject ForcePosition;
    public GameObject ForcePosition2;
    public GameObject ForcePosition3;
    public GameObject ForcePosition4;




    [Header("GoldText")]
    public Text GoldText;
    public Text GoldLevelText;
    public Text GoldPrice;

    [Header("ArmyCountText")]// a1_text --> a1 askerinin say�s�n� belirtir.
    public Text a1_text;
    public Text a2_text;
    public Text a3_text;
    public Text a4_text;
    public Text a5_text;
    public Text a6_text;
    public Text a7_text;
    public Text a8_text;
    public Text a9_text;
    public Text a10_text;

    [Header("Army Item Upgrade")]// E�er ekleme yapmak isterseniz buray� a�!!!
    public GameObject a1_item;
    public GameObject a1_item2;

   /* public GameObject a2_item;
    public GameObject a2_item2;

    public GameObject a3_item;
    public GameObject a3_item2;

    public GameObject a4_item;
    public GameObject a4_item2;

    public GameObject a5_item;
    public GameObject a5_item2;

    public GameObject a6_item;
    public GameObject a6_item2;

    public GameObject a7_item;
    public GameObject a7_item2;

    public GameObject a8_item;
    public GameObject a8_item2;

    public GameObject a9_item;
    public GameObject a9_item2;

    public GameObject a10_item;
    public GameObject a10_item2;*/


    [Header("ArmyLevelText")]
    public Text a1_lvtext;
    public Text a2_lvtext;
    public Text a3_lvtext;
    public Text a4_lvtext;
    public Text a5_lvtext;
    public Text a6_lvtext;
    public Text a7_lvtext;
    public Text a8_lvtext;
    public Text a9_lvtext;
    public Text a10_lvtext;

    [Header("ArmyUpgradePriceText")]
    public Text a1_uptext;
    public Text a2_uptext;
    public Text a3_uptext;
    public Text a4_uptext;
    public Text a5_uptext;
    public Text a6_uptext;
    public Text a7_uptext;
    public Text a8_uptext;
    public Text a9_uptext;
    public Text a10_uptext;
    
    [Header("ArmyBuyPriceText")]
    public Text a1_buytext;
    public Text a2_buytext;
    public Text a3_buytext;
    public Text a4_buytext;
    public Text a5_buytext;
    public Text a6_buytext;
    public Text a7_buytext;
    public Text a8_buytext;
    public Text a9_buytext;
    public Text a10_buytext;

    [Header("Enemy Army Count Text")]// a1_text --> a1 askerinin say�s�n� belirtir.
    public Text a1_enemytext;
    public Text a2_enemytext;
    public Text a3_enemytext;
    public Text a4_enemytext;
    public Text a5_enemytext;
    public Text a6_enemytext;
    public Text a7_enemytext;
    public Text a8_enemytext;
    public Text a9_enemytext;
    public Text a10_enemytext;

    [Header("Enemey Army Level Text")] // d��man ordusu kod k�sm�
    public Text a1_enlvtext;
    public Text a2_enlvtext;
    public Text a3_enlvtext;
    public Text a4_enlvtext;
    public Text a5_enlvtext;
    public Text a6_enlvtext;
    public Text a7_enlvtext;
    public Text a8_enlvtext;
    public Text a9_enlvtext;
    public Text a10_enlvtext;
    public GameObject EnemyCastle;
    public Text EnemyCastleText;
    public float EnemeySavasGucu;
    public float WarOver;
    public GameObject LowerWarPanel;
    public int[] t;    //enemy tower �s attacked before?
    public bool IsWon=false;

    [Header("War Panel Objects")]
    public GameObject[] Clouds;
    public int CloudsInt=6;


    [Header("Buttons And Panels")]
    public GameObject ArmyBuyPanel;
    public GameObject MainPanel;
    public GameObject ResearchPanel;
    public GameObject WarPanel;
    private bool WarPanelBussy=true;
    private float EndWar;

    [Header("Languages")]
    private bool TurkishLan0,EnglishLan1;
    private int SelectLan;

    [Header("Scripts")]
    FollowScript followScript;




    // Veriler kaydedilicek

    private void Awake()
    {

        TranslateLanguages();
        Application.targetFrameRate = 60; // fps'i 60 sabitlemek i�in yazd�m*
        if (PlayerPrefs.GetInt("FirstTime")==0){
            t =new int[15]; //d��man kalesine sald�r�ld� m� kontrol dizisi olu�tur
            PlayerPrefs.SetInt("FirstTime", 1);
            SaveData();
        }
        else
        {
            EqualData();
        }
        //Gold = PlayerPrefs.GetFloat("Gold");//Bu kodu silebilirsin e�er goldtexti h�zl� geliyorsa.
       
    }
    private void Start()
    {
        CheckItems();
    }
    void Update()
    {
        GoldMining();
        CreateVillager();
    }
    public void OpenLowWarPanel()
    {
        LowerWarPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.InBack);
        LowerWarPanel.SetActive(false);
    }
    public void OpenWarPanel()
    {
        if(LowerWarPanel.GetComponent<CanvasGroup>().alpha == 0)
        {
            LowerWarPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutBack);
            LowerWarPanel.SetActive(true);
        }
        else
        {
            LowerWarPanel.GetComponent<CanvasGroup>().DOFade(0, 0.3f).SetEase(Ease.InOutExpo);
            LowerWarPanel.SetActive(false);
        }
    }
    private void TranslateLanguages()
    {
        if(SelectLan == 0) {TurkishLan0 = true;}else if(SelectLan ==1) {EnglishLan1 = true;}
    }
    public void EnemyArmy()
    {
       EnemyCastle.name= EventSystem.current.currentSelectedGameObject.name;
        IsNotWonBefore();
        if (!IsWon)
        {
            OpenWarPanel();//daha �nceden sald�r�lan kalenin intertable�n� false yapma
        }





        switch (EnemyCastle.name)
        {
            case "t1"://Tower1 anlam�na gelen t1 ,t2,t3,t4 seviye belirtir.
                a1_enemytext.text = "3";    a1_enlvtext.text = "Seviye 1";
                a2_enemytext.text = "2";    a1_enlvtext.text = "Seviye 1";
                a3_enemytext.text = "0";    a1_enlvtext.text = "Seviye 1";
                a4_enemytext.text = "0";    a1_enlvtext.text = "Seviye 1";
                a5_enemytext.text = "0";    a1_enlvtext.text = "Seviye 1";
                a6_enemytext.text = "0";    a1_enlvtext.text = "Seviye 1";
                a7_enemytext.text = "0";    a1_enlvtext.text = "Seviye 1";
                a8_enemytext.text = "0";    a1_enlvtext.text = "Seviye 1";
                a9_enemytext.text = "0";    a1_enlvtext.text = "Seviye 1";
                a10_enemytext.text = "0";   a1_enlvtext.text = "Seviye 1";

                if (TurkishLan0)
                {
                    EnemyCastleText.text = "K�y Sv1";
                }
                else if (EnglishLan1)
                {
                    EnemyCastleText.text = "Village Lv1";
                }
                break;
            case "t2"://Tower1 anlam�na gelen t1 ,t2,t3,t4 seviye belirtir.
                a1_enemytext.text = "0";
                a2_enemytext.text = "0";
                a3_enemytext.text = "0";
                a4_enemytext.text = "0";
                a5_enemytext.text = "0";
                a6_enemytext.text = "0";
                a7_enemytext.text = "0";
                a8_enemytext.text = "0";
                a9_enemytext.text = "0";
                a10_enemytext.text = "0";

                if (TurkishLan0)
                {
                    EnemyCastleText.text = "K�y Sv2";
                }
                else if (EnglishLan1)
                {
                    EnemyCastleText.text = "Village Lv2";
                }
                break;
            case "t3"://Tower1 anlam�na gelen t1 ,t2,t3,t4 seviye belirtir.
                a1_enemytext.text = "0";
                a2_enemytext.text = "0";
                a3_enemytext.text = "0";
                a4_enemytext.text = "0";
                a5_enemytext.text = "0";
                a6_enemytext.text = "0";
                a7_enemytext.text = "0";
                a8_enemytext.text = "0";
                a9_enemytext.text = "0";
                a10_enemytext.text = "0";

                if (TurkishLan0)
                {
                    EnemyCastleText.text = "K�y Sv3";
                }
                else if (EnglishLan1)
                {
                    EnemyCastleText.text = "Village Lv3";
                }
                break;
            case "t4":
                a1_enemytext.text = "0";
                a2_enemytext.text = "0";
                a3_enemytext.text = "0";
                a4_enemytext.text = "0";
                a5_enemytext.text = "0";
                a6_enemytext.text = "0";
                a7_enemytext.text = "0";
                a8_enemytext.text = "0";
                a9_enemytext.text = "0";
                a10_enemytext.text = "0";

                if (TurkishLan0)
                {
                    EnemyCastleText.text = "Derebeylik Sv4";
                }
                else if (EnglishLan1)
                {
                    EnemyCastleText.text = "Feodal Lv4";
                }
                break;
            case "t5":
                a1_enemytext.text = "0";
                a2_enemytext.text = "0";
                a3_enemytext.text = "0";
                a4_enemytext.text = "0";
                a5_enemytext.text = "0";
                a6_enemytext.text = "0";
                a7_enemytext.text = "0";
                a8_enemytext.text = "0";
                a9_enemytext.text = "0";
                a10_enemytext.text = "0";

                if (TurkishLan0)
                {
                    EnemyCastleText.text = "Derebeylik Sv5";
                }
                else if (EnglishLan1)
                {
                    EnemyCastleText.text = "Feodal Lv5";
                }
                break;
            case "t6":
                a1_enemytext.text = "0";
                a2_enemytext.text = "0";
                a3_enemytext.text = "0";
                a4_enemytext.text = "0";
                a5_enemytext.text = "0";
                a6_enemytext.text = "0";
                a7_enemytext.text = "0";
                a8_enemytext.text = "0";
                a9_enemytext.text = "0";
                a10_enemytext.text = "0";

                if (TurkishLan0)
                {
                    EnemyCastleText.text = "Derebeylik Sv6";
                }
                else if (EnglishLan1)
                {
                    EnemyCastleText.text = "Feodal Lv6";
                }
                break;
            case "t7":
                a1_enemytext.text = "0";
                a2_enemytext.text = "0";
                a3_enemytext.text = "0";
                a4_enemytext.text = "0";
                a5_enemytext.text = "0";
                a6_enemytext.text = "0";
                a7_enemytext.text = "0";
                a8_enemytext.text = "0";
                a9_enemytext.text = "0";
                a10_enemytext.text = "0";

                if (TurkishLan0)
                {
                    EnemyCastleText.text = "Derebeylik Sv7";
                }
                else if (EnglishLan1)
                {
                    EnemyCastleText.text = "Feodal Lv7";
                }
                break;
            case "t8":
                a1_enemytext.text = "0";
                a2_enemytext.text = "0";
                a3_enemytext.text = "0";
                a4_enemytext.text = "0";
                a5_enemytext.text = "0";
                a6_enemytext.text = "0";
                a7_enemytext.text = "0";
                a8_enemytext.text = "0";
                a9_enemytext.text = "0";
                a10_enemytext.text = "0";

                if (TurkishLan0)
                {
                    EnemyCastleText.text = "Derebeylik Sv8";
                }
                else if (EnglishLan1)
                {
                    EnemyCastleText.text = "Feodal Lv8";
                }
                break;
            case "t9":
                a1_enemytext.text = "0";
                a2_enemytext.text = "0";
                a3_enemytext.text = "0";
                a4_enemytext.text = "0";
                a5_enemytext.text = "0";
                a6_enemytext.text = "0";
                a7_enemytext.text = "0";
                a8_enemytext.text = "0";
                a9_enemytext.text = "0";
                a10_enemytext.text = "0";

                if (TurkishLan0)
                {
                    EnemyCastleText.text = "Derebeylik Sv9";
                }
                else if (EnglishLan1)
                {
                    EnemyCastleText.text = "Feodal Lv9";
                }
                break;
            case "t10":
                a1_enemytext.text = "0";
                a2_enemytext.text = "0";
                a3_enemytext.text = "0";
                a4_enemytext.text = "0";
                a5_enemytext.text = "0";
                a6_enemytext.text = "0";
                a7_enemytext.text = "0";
                a8_enemytext.text = "0";
                a9_enemytext.text = "0";
                a10_enemytext.text = "0";

                if (TurkishLan0)
                {
                    EnemyCastleText.text = "Derebeylik Sv10";
                }
                else if (EnglishLan1)
                {
                    EnemyCastleText.text = "Feodal Lv10";
                }
                break;
            default: 
                Debug.LogError("Hata var switchde");
                break;
        }
    }
    public void WarButton()//EnemeySavasGucu+=float.Parse(a1_enemytext.text) * float.Parse(a1_enlvtext.text.Substring(a1_enlvtext.text.Length - 1)); 
    {
        IsNotWonBefore();
        OpenLowWarPanel();// LowerWarPanel sava� butonunua t�kland�ktan sonra kapan�r ve sava� kazan�l�rsa tekrar a��lamz (Won war panelde ekran� a�mal�) 
        if (WarPanelBussy && !IsWon)
        {WarPanelBussy = false;
            IsWon = false;
            switch (EnemyCastle.name)
            {
                case "t1"://Tower1 anlam�na gelen t1 ,t2,t3,t4 seviye belirtir.
                    EnemeySavasGucu = 50; // D��MAN SAVA� G�C� DENGELEN�CEK
                    break;
                case "t2":
                    EnemeySavasGucu = 100;
                    break;
                case "t3":
                    EnemeySavasGucu = 150;
                    break;
                case "t4":
                    EnemeySavasGucu = 200;
                    break;
                case "t5":
                    EnemeySavasGucu = 250;
                    break;
            }
            WarBuffs();
            EndWar = SavasGucu - EnemeySavasGucu;

            if ( (EndWar <=100) && (-100 <=EndWar) && (randomint>=90) )
            {
                WonWar();
            }
            else if(EndWar > 0)
            {
                WonWar();
            }
            else
            {
                LoseWar();
            }

        }

    }
    void WonWar()
    {
        //KAZANMA EKRANI GEL�CEK
        Debug.Log("Sava�� Kazand�n�z!!!");
        switch (EnemyCastle.name) 
        {
            case "t1":
                {
                    a1_enemytext.text = "0";//player prefs ile yang�n animasyonu buraya
                                            //1. Towerda ordunun hareket etmesine gerek yok   
                    t[1] = 1;
                    break;
                }
            case "t2":
                {
                    if (t[1] != 1)
                    {
                        a2_enemytext.text = "0";
                        t[2] = 1;
                        //followScript.MoveArmy(); hareket eden asker kalkmal� m�?
                    }
                    break;
                }
            case "t3":
                {
                    if(t[2] != 1)
                    {
                        a3_enemytext.text = "0";
                        t[3] = 1;
                        //followScript.MoveArmy();
                    }
                    break;
                }
            case "t4":
                {
                    if (t[3] != 1)
                    {
                        a4_enemytext.text = "0";
                        t[4] = 1;
                        followScript.MoveArmy();
                    }
                    break;
                }
            case "t5":
                {
                    if (t[4] != 1)
                    {
                        a5_enemytext.text = "0";
                        t[5] = 1;
                        followScript.MoveArmy();
                    }
                    break;
                }
            case "t6":
                {
                    if (t[5] != 1)
                    {
                        a6_enemytext.text = "0";
                        t[6] = 1;
                        followScript.MoveArmy();
                    }
                    break;
                }
            case "t7":
                {
                    if (t[6] != 1)
                    {
                        a7_enemytext.text = "0";
                        t[7] = 1;//bulutlar� bu sava��n kazan�lmas�yla beraber kald�rmal�y�z 
                        CloudsInt = 0;
                        followScript.MoveArmy();
                        StartCoroutine(MoveClouds());
                    }
                    break;
                }
            case "t8":
                {
                    if (t[7] != 1)
                    {
                        a8_enemytext.text = "0";
                        t[8] = 1;
                        followScript.MoveArmy();
                    }
                    break;
                }
            case "t9":
                {
                    if (t[8] != 1)
                    {
                        a9_enemytext.text = "0";
                        t[9] = 1;
                        followScript.MoveArmy();
                    }
                    break;
                }
            case "t10":
                {
                    if (t[9] != 1)
                    {
                        a10_enemytext.text = "0";
                        t[10] = 1;
                        followScript.MoveArmy();
                    }
                    break;
                }
            default:
                Debug.LogError("Sald�r�lan D��man Kalesi bulunamad�");
                break;
        }
        SaveData();
        WarPanelBussy = true;
    }
    void LoseWar()
    {
        //Kaybetme Ekran�
        Debug.Log("Sava�� Kaybettiniz!!!");
        WarPanelBussy = true;
    }
    void IsNotWonBefore()
    {
        int x = CharUnicodeInfo.GetDecimalDigitValue(EnemyCastle.name[1]); //enemy castle "t1" 2.harfini al�yor.
        int y = 0;
        if (EnemyCastle.name.Length >= 3)
        {
            Debug.Log("Nasil yani");
            y = CharUnicodeInfo.GetDecimalDigitValue(EnemyCastle.name[2]);
            x = (x * 10) + y;
        }

     
        if (x > 9)
        {
            switch ((EnemyCastle.name, t[x]))
            {
                case ("t10", 1): { IsWon = true; break; }
                case ("t11", 1): { IsWon = true; break; }
                case ("t12", 1): { IsWon = true; break; }
                case ("t13", 1): { IsWon = true; break; }
                case ("t14", 1): { IsWon = true; break; }
                case ("t15", 1): { IsWon = true; break; }
                case ("t16", 1): { IsWon = true; break; }
                case ("t17", 1): { IsWon = true; break; }
                case ("t18", 1): { IsWon = true; break; }
                case ("t19", 1): { IsWon = true; break; }
                default: IsWon = false; break;

            }
        }
        else
        {
            switch ((EnemyCastle.name, t[x]))
            {
                case ("t1", 1): { IsWon = true; break; }
                case ("t2", 1): { IsWon = true; break; }
                case ("t3", 1): { IsWon = true; break; }
                case ("t4", 1): { IsWon = true; break; }
                case ("t5", 1): { IsWon = true; break; }
                case ("t6", 1): { IsWon = true; break; }
                case ("t7", 1): { IsWon = true; break; }
                case ("t8", 1): { IsWon = true; break; }
                case ("t9", 1): { IsWon = true; break; }
                default: IsWon = false; break;

            }
        }

    }

    void WarBuffs()
    {
        randomint = Random.Range(0, 100);// 0 ve 100 yerine verilen rakam lucky upgrade ile de�i�icek. Ve Sava� g�c� dengelenmesine bak�l�cak.

        if (kilicliasker >= 5 && okcuasker >= 5 || atliasker >= 5 && okcuasker >= 10) // tanr�lar�n �fkesi gibi yetenekler birle�meler olabilir
        {
            SavasGucu += 150;
        }
        if (pantheon >= 5 && atliokcu >= 10 || pantheon >= 5 && sifaci >= 5)
        {
            SavasGucu += 500;

        }
        if (sovalye >= 10 && atlisovalye >= 5 || atlisovalye >= 5 && atliokcu >= 5)
        {
            SavasGucu += 1000;

        }
        if (mancinik >= 1 && atlisovalye >= 10 || kocbasi >= 1 && sovalye >= 10)
        {
            SavasGucu += 5000;
        }
        if (mancinik >= 1 && okcuasker >= 10 || mancinik >= 1 && atliokcu >= 10)
        {
            SavasGucu += 3000;
        }
    }
    public void CheckItems()//yeni itemler buraya eklenip, upgrade yerlerinden �a��r�lacak
    {
        if (a1 >=2)
        {
            a1_item.SetActive(true);
            if (a1 > 2)
            {
                a1_item.SetActive(false);
                a1_item2.SetActive(true);
            }
        }
    }
    public void CreateVillager()
    {
        randomTimer -= Time.deltaTime;
        if (randomTimer < 0)
        {
            randomint = Random.Range(0, 100);
        }
       
        if (randomTimer < 0 && randomint >= 99)
        {
            Destroy(Instantiate(ForcePosition, ForcePosition.transform.position, Quaternion.identity),2f);

            Destroy(Instantiate(VillagerWalkingRight, VillagerWalkingRight.transform.position, Quaternion.identity),10f);
            randomTimer = 15;
        }
        else if (randomTimer < 0 && randomint <= 1)
        {
            Destroy(Instantiate(ForcePosition2, ForcePosition2.transform.position, Quaternion.identity),2f);

            Destroy(Instantiate(VillagerWalkingLeft, VillagerWalkingLeft.transform.position, Quaternion.identity),10f);
            randomTimer = 20;

        }
        else if (randomTimer < 0 && randomint == 50)
        {
            Destroy(Instantiate(ForcePosition3, ForcePosition3.transform.position, Quaternion.identity), 2f);
            Destroy(Instantiate(VillagerWaiting, VillagerWaiting.transform.position, Quaternion.identity),25f);
            randomTimer = 30;

        }
        else if (randomTimer < 0 && randomint ==25)
        {
            Destroy(Instantiate(ForcePosition4, ForcePosition4.transform.position, Quaternion.identity), 2f);

            Destroy(Instantiate(VillagerWalkingRight2, VillagerWalkingRight2.transform.position, Quaternion.identity), 10f);
            randomTimer = 30;

        }
    }
    IEnumerator MoveClouds()
    {
        yield return new WaitForSeconds(2f);

        for(int i=CloudsInt; i <Clouds.Length;i++)
        {
            if (i % 2 == 0)
            {
                Clouds[i].transform.DOMoveX(-1024f, 5f);
            }
            else
            {
                Clouds[i].transform.DOMoveX(1500f, 5f);
            }
        }

        yield break;
    }
    public void ClickEarn()// BURAYA BAKMAN LAZZIMMM
    {
        if(Input.touchCount > 0 || true) //Input.GetMouseButtonDown(0))
        {
            //Touch touch = Input.GetTouch(0); bunu a�
            // Vector3 clickPosition = -Vector3.one;
            //clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));
            Gold++;
            Debug.Log("Working");// Alt�n t�klayarak kazanma oran� artt�rma yap�l�cak.
            StartCoroutine(GoldEffectt());
        }
    }
    public void ClickYOket()
    {
        Debug.Log("Okey;");
    }
    IEnumerator GoldEffectt()
    {
       yield return new WaitForSeconds(0.1f);

        Destroy(Instantiate(GoldClickEffect, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f)),Quaternion.identity),3f);
        Destroy(Instantiate(ClickEffectText,ClickPosition.transform), 3f);

        yield break;
    }
    private void GoldMining()
    {
        Gold += goldMining;
        goldTimer -= Time.deltaTime;
        if (goldTimer < 0)
        {
            if (Gold < 9999)
            {
                GoldText.text = Gold.ToString();
            }
            else if (Gold > 9999)
            {
                //10000.22
                if (Gold.ToString().Length > 7)
                    GoldText.text = Gold.ToString().Substring(0, 2) + '.' + Gold.ToString().Substring(6, 2) + "B"; //Substring 6. elemandan sonraki 2 eleman� al�r(6,2)


            }
            else
            {//1.000.000,00
                if (Gold.ToString().Length > 9)
                    GoldText.text = Gold.ToString().Substring(0,2) + '.' + Gold.ToString().Substring(8, 1) + "Mn"; 
            }


            GoldText2.text = GoldText.text;

            goldTimer = 1;
        }
    }
    public void GoldMiningUpgrade()
    {
        
        if (Gold >= 500 & goldMiningLv==1)
        {

            Gold -= 500;
            Gold =0.02f;
            goldMiningLv = 2;
            GoldLevelText.text ="SEV�YE 2";

        }
        else if (Gold >= 1500 & goldMiningLv ==2)
        {
            Gold -= 1500;

            goldMining = 0.03f;
            goldMiningLv = 3;
            GoldLevelText.text = "SEV�YE 3";

        }
        else if (Gold >= 5000 & goldMiningLv==3)
        {
            Gold -= 5000;

            Gold += 0.04f;
            goldMiningLv = 4;
            GoldLevelText.text = "SEV�YE 4";


        }
        else if (Gold >= 7500 & goldMiningLv == 4)
        {
            Gold -= 7500;

            goldMining = 0.05f;
            goldMiningLv = 5;
            GoldLevelText.text = "SEV�YE 5";

        }
        else if (Gold >= 15000 & goldMiningLv == 5)
        {
            goldMiningLv = 6;
            Gold -= 15000;

            Gold += 0.06f;
            GoldLevelText.text = "SEV�YE 2";

        }
        else if (Gold >= 25000 & goldMiningLv == 6)
        {
            Gold -= 25000;

            goldMining = 0.07f;
            goldMiningLv = 7;
            GoldLevelText.text = "SEV�YE 7";

        }
        else if (Gold >= 50000 & goldMiningLv == 7)
        {
            goldMiningLv = 8;
            Gold -= 50000;

            Gold += 0.08f;
            GoldLevelText.text = "SEV�YE 8";

        }
        else if (Gold >= 75000 & goldMiningLv == 8)
        {
            Gold -= 75000;

            goldMining = 0.09f;
            goldMiningLv = 9;
            GoldLevelText.text = "SEV�YE 9";

        }
        else if (Gold >= 100000 & goldMiningLv == 9)
        {
            goldMiningLv = 10;
            Gold -= 100000;

            Gold += 0.1f;
            GoldLevelText.text = "SEV�YE 10";

        }
        SaveData();
    }
    public void KilicliAskerSatinAl()
    {
        
        if(Gold >= 50 & a1 ==1)
        {
            kilicliasker++;
            SavasGucu++;
            Gold -= 50;         
        }
        else if(Gold >= 75 & a1 == 2)
        {
            kilicliasker++;
            SavasGucu+=3;
            Gold -= 75;
        }
        else if(Gold >=100 & a1 == 3)
        {
            kilicliasker++;
            SavasGucu += 15;
            Gold -= 100;
        }
        a1_text.text = kilicliasker.ToString();
        SaveData();
    }
    public void KilicliAskerGelistir()
    {
        if(Gold >= 500 && a1 == 1)
        {
            a1 = 2;
            Gold -= 500;
            a1_uptext.text ="1.000 Alt�n";
            a1_buytext.text="75 Alt�n";
            a1_lvtext.text = "Seviye 2";
            CheckItems();
        }
        else if (Gold >= 1000 && a1 == 2)
        {
            a1 = 3;
            Gold -= 1000;
            a1_uptext.text = "Son Seviye";
            a1_buytext.text = "100 Alt�n";
            a1_lvtext.text = "Seviye 3";
            CheckItems();
        }

        SaveData();
    }
    public void OkcuAskerGelistir()
    {
        if (Gold >= 750 && a2 == 1)
        {
            a2 = 2;
            Gold -= 750;
            a2_uptext.text = "1.500 Alt�n";
            a2_buytext.text = "75 Alt�n";
            a2_lvtext.text = "Seviye 2";
        }
        else if (Gold >= 1500 && a2 == 2)
        {
            a2 = 3;
            Gold -= 1500;
            a2_uptext.text = "Son Seviye";
            a2_buytext.text = "100 Alt�n";
            a2_lvtext.text = "Seviye 3";
        }
        SaveData();
    }
    public void OkcuAskerSatinAl()
    {

        if (Gold >= 50 & a2 == 1)
        {
            okcuasker++;
            SavasGucu++;
            Gold -= 50;
        }
        else if (Gold >= 75 & a2== 2)
        {
            okcuasker++;
            SavasGucu += 3;
            Gold -= 75;
        }
        else if (Gold >= 100 & a1 == 3)
        {
            okcuasker++;
            SavasGucu += 16;
            Gold -= 100;
        }
        a2_text.text = okcuasker.ToString();
        SaveData();
    }
    public void AtliAskerGelistir()
    {
        if (Gold >= 1000 && a3 == 0)
        {
            a3 = 1;
            Gold -= 1000;
            a3_uptext.text = "2.000 Alt�n";
            a3_buytext.text = "100 Alt�n";
            a3_lvtext.text = "Seviye 1";
        }
        else if (Gold >= 2000 && a3 == 1)
        {
            a3 = 2;
            Gold -= 2000;
            a3_uptext.text = "3.000 Alt�n";
            a3_buytext.text = "150 Alt�n";
            a3_lvtext.text = "Seviye 2";

        }
        else if (Gold >= 3000 && a3 == 2)
        {
            a3 = 3;
            Gold -= 3000;
            a3_uptext.text = "Son Seviye";
            a3_buytext.text = "200 Alt�n";
            a3_lvtext.text = "Seviye 3";

        }
        SaveData();
    }
    public void AtliAskerSatinAl()
    {

        if (Gold >= 100 & a3 == 1)
        {
            atliasker++;
            SavasGucu+= 15;
            Gold -= 100;

        }
        else if (Gold >= 150 & a3 == 2)
        {
            atliasker++;
            SavasGucu +=25;
            Gold -= 150;
        }
        else if (Gold >= 200 & a3 == 3)
        {
            atliasker++;
            SavasGucu += 30;
            Gold -= 200;
        }
        a3_text.text = atliasker.ToString();
        SaveData();
    }
    public void PantheonGelistir()
    {
        if (Gold >= 15000 && a4 == 0)
        {
            a4 = 1;
            Gold -= 15000;
            a4_uptext.text = "7.500 Alt�n";
            a4_buytext.text = "300 Alt�n";
            a4_lvtext.text = "Seviye 1";

        }
        else if (Gold >= 7500 && a4 == 1)
        {
            a4 = 2;
            Gold -= 7500;
            a4_uptext.text = "10.000 Alt�n";
            a4_buytext.text = "750 Alt�n";
            a4_lvtext.text = "Seviye 2";

        }
        else if (Gold >= 10000 && a4 == 2)
        {
            a4 = 3;
            Gold -= 10000;
            a4_uptext.text = "Son Seviye";
            a4_buytext.text = "1.000 Alt�n";
            a4_lvtext.text = "Seviye 3";

        }
        SaveData();
    }
    public void PantheonSatinAl()
    {

        if (Gold >= 300 & a4 == 1)
        {
            pantheon++;
            SavasGucu += 200;
            Gold -= 300;

        }
        else if (Gold >= 750 & a4 == 2)
        {
            pantheon++;
            SavasGucu += 250;
            Gold -= 750;
        }
        else if (Gold >= 1000 & a4 == 3)
        {
            pantheon++;
            SavasGucu += 500;
            Gold -= 1000;
        }
        a4_text.text = pantheon.ToString();
        SaveData();
    }
    public void SovalyeGelistir()
    {
        if (Gold >= 20000 && a5 == 0)
        {
            a5 = 1;
            Gold -= 20000;
            a5_uptext.text = "1.000 Alt�n";
            a5_buytext.text = "500 Alt�n";
            a5_lvtext.text = "Seviye 1";

        }
        else if (Gold >= 10000 && a5 == 1)
        {
            a5 = 2;
            Gold -= 10000;
            a5_uptext.text = "1.500 Alt�n";
            a5_buytext.text = "1.000 Alt�n";
            a5_lvtext.text = "Seviye 2";

        }
        else if (Gold >= 15000 && a5 == 2)
        {
            a5 = 3;
            Gold -= 15000;
            a5_uptext.text = "Son Seviye";
            a5_buytext.text = "2.000 Alt�n";
            a5_lvtext.text = "Seviye 3";

        }
        SaveData();
    }
    public void SovalyeSatinAl()
    {

        if (Gold >= 500 & a5 == 1)
        {
            sovalye++;
            SavasGucu += 300;
            Gold -= 500;

        }
        else if (Gold >= 1000 & a5 == 2)
        {
            sovalye++;
            SavasGucu += 500;
            Gold -= 1000;
        }
        else if (Gold >= 2000 & a5 == 3)
        {
            sovalye++;
            SavasGucu += 1000;
            Gold -= 2000;
        }
        a5_text.text = sovalye.ToString();

        SaveData();
    }
    public void AtliSovalyeGelistir()
    {
        if (Gold >= 30000 && a6 == 0)
        {
            a6 = 1;
            Gold -= 30000;
            a6_uptext.text = "12.000 Alt�n";
            a6_buytext.text = "750 Alt�n";
            a6_lvtext.text = "Seviye 1";

        }
        else if (Gold >= 12000 && a6 == 1)
        {
            a6 = 2;
            Gold -= 12000;
            a6_uptext.text = "18.000 Alt�n";
            a6_buytext.text = "1.500 Alt�n";
            a6_lvtext.text = "Seviye 2";

        }
        else if (Gold >= 18000 && a6 == 2)
        {
            a6 = 3;
            Gold -= 18000;
            a6_uptext.text = "Son Seviye";
            a6_buytext.text = "3.000 Alt�n";
            a6_lvtext.text = "Seviye 3";

        }
        SaveData();
    }
    public void AtliSovalyeSatinAl()
    {

        if (Gold >= 750 & a6 == 1)
        {
            atlisovalye++;
            SavasGucu += 500;
            Gold -= 750;

        }
        else if (Gold >= 1500 & a6 == 2)
        {
            atlisovalye++;
            SavasGucu += 750;
            Gold -= 1500;
        }
        else if (Gold >= 3000 & a6 == 3)
        {
            atlisovalye++;
            SavasGucu += 1500;
            Gold -= 3000;
        }
        a6_text.text =atlisovalye.ToString();

        SaveData();
    }
    public void MancinikGelistir()
    {
        if (Gold >= 100000 && a7 == 0)
        {
            a7 = 1;
            Gold -= 100000;
            a7_uptext.text = "100.000 Alt�n";
            a7_buytext.text = "5.000 Alt�n";
            a7_lvtext.text = "Seviye 1";

        }
        else if (Gold >= 100000 && a7 == 1)
        {
            a7 = 2;
            Gold -= 100000;
            a7_uptext.text = "250.000 Alt�n";
            a7_buytext.text = "10.000 Alt�n";
            a7_lvtext.text = "Seviye 2";

        }
        else if (Gold >= 250000 && a7 == 2)
        {
            a7 = 3;
            Gold -= 250000;
            a7_uptext.text = "Son Seviye";
            a7_buytext.text = "100.000 Alt�n";
            a7_lvtext.text = "Seviye 3";

        }
        SaveData();
    }
    public void MancinikSatinAl()
    {

        if (Gold >= 5000 & a7 == 1)
        {
            mancinik++;
            SavasGucu += 100000;
            Gold -= 5000;

        }
        else if (Gold >= 10000 & a7 == 2)
        {
            mancinik++;
            SavasGucu += 200000;
            Gold -= 10000;
        }
        else if (Gold >= 100000 & a7 == 3)
        {
            mancinik++;
            SavasGucu += 50000;
            Gold -= 100000;
        }
        a7_text.text = mancinik.ToString();

        SaveData();
    }
    public void KocbasiGelistir()
    {
        if (Gold >= 50000 && a8 == 0)
        {
            a8 = 1;
            Gold -= 50000;
            a8_uptext.text = "20.000 Alt�n";
            a8_buytext.text = "1.000 Alt�n";
            a8_lvtext.text = "Seviye 1";


        }
        else if (Gold >= 20000 && a8 == 1)
        {
            a8 = 2;
            Gold -= 20000;
            a8_uptext.text = "50.000 Alt�n";
            a8_buytext.text = "2.000 Alt�n";
            a8_lvtext.text = "Seviye 2";

        }
        else if (Gold >= 50000 && a8 == 2)
        {
            a8 = 3;
            Gold -= 50000;
            a8_uptext.text = "Son Seviye";
            a8_buytext.text = "5.000 Alt�n";
            a8_lvtext.text = "Seviye 3";

        }
        SaveData();
    }
    public void KocbasiSatinAl()
    {

        if (Gold >= 1000 & a8 == 1)
        {
            kocbasi++;
            SavasGucu += 1000;
            Gold -= 1000;

        }
        else if (Gold >= 2000 & a8 == 2)
        {
            kocbasi++;
            SavasGucu += 1500;
            Gold -= 2000;
        }
        else if (Gold >= 5000 & a8 == 3)
        {
            kocbasi++;
            SavasGucu += 3000;
            Gold -= 5000;
        }
        a8_text.text = kocbasi.ToString();

        SaveData();
    }
    public void SifaciGelistir()
    {
        if (Gold >= 10000 && a9 == 0)
        {
            a9 = 1;
            Gold -= 10000;
            a9_uptext.text = "5.000 Alt�n";
            a9_buytext.text = "300 Alt�n";
            a9_lvtext.text = "Seviye 1";

        }
        else if (Gold >= 5000 && a9 == 1)
        {
            a9 = 2;
            Gold -= 5000;
            a9_uptext.text = "7.500 Alt�n";
            a9_buytext.text = "500 Alt�n";
            a9_lvtext.text = "Seviye 2";

        }
        else if (Gold >= 7500 && a9 == 2)
        {
            a9 = 3;
            Gold -= 7500;
            a9_uptext.text = "Son Seviye";
            a9_buytext.text = "1.000 Alt�n";
            a9_lvtext.text = "Seviye 3";

        }
        SaveData();
    }
    public void SifaciatinAl()
    {

        if (Gold >= 300 & a9 == 1)
        {
            sifaci++;
            SavasGucu += 100;
            Gold -= 300;

        }
        else if (Gold >= 500 & a9 == 2)
        {
            sifaci++;
            SavasGucu += 120;
            Gold -= 500;
        }
        else if (Gold >= 1000 & a9 == 3)
        {
            sifaci++;
            SavasGucu += 500;
            Gold -= 1000;
        }
        a9_text.text = sifaci.ToString();

        SaveData();
    }
    public void AtliOkcuGelistir()
    {
        if (Gold >= 3000 && a10 == 0)
        {
            a10 = 1;
            Gold -= 3000;
            a10_uptext.text = "1.500 Alt�n";
            a10_buytext.text = "150 Alt�n";
            a10_lvtext.text = "Seviye 1";

        }
        else if (Gold >= 1500 && a10 == 1)
        {
            a10 = 2;
            Gold -= 1500;
            a10_uptext.text = "3.000 Alt�n";
            a10_buytext.text = "300 Alt�n";
            a10_lvtext.text = "Seviye 2";

        }
        else if (Gold >= 3000 && a10 == 2)
        {
            a10 = 3;
            Gold -= 3000;
            a10_uptext.text = "Son Seviye";
            a10_buytext.text = "500 Alt�n";
            a10_lvtext.text = "Seviye 3";

        }
        SaveData();
    }
    public void AtliOkcuatinAl()
    {

        if (Gold >= 150 & a10 == 1)
        {
            atliokcu++;
            SavasGucu += 30;
            Gold -= 150;

        }
        else if (Gold >= 300 & a10 == 2)
        {
            atliokcu++;
            SavasGucu += 60;
            Gold -= 300;
        }
        else if (Gold >= 500 & a10 == 3)
        {
            atliokcu++;
            SavasGucu += 65;
            Gold -= 500;
        }
        a10_text.text = atliokcu.ToString();

        SaveData();
    }

    //GeneralButtonPart
    public void ArmyBuyBtn()
    {
        MainPanel.SetActive(false);ArmyBuyPanel.SetActive(true);
    }
    public void ReturnMainPanel()
    {
        MainPanel.SetActive(true); ArmyBuyPanel.SetActive(false);ResearchPanel.SetActive(false);WarPanel.SetActive(false);   
    }
    public void ResearchPanell()
    {
        MainPanel.SetActive(false); ResearchPanel.SetActive(true);
    }
    public void WarPanell()
    {
        WarPanel.SetActive(true); MainPanel.SetActive(false);
    }
    public void SelectLanguageBtn()
    {
        //Se�ilen butonu alarak tan�mla if(x gameobject){TurkishLan0=true;} 
    }
    /// Data Part

    public void SaveData()
    {//Asker Say�s� Kaydetme(t�r�ne g�re)
        PlayerPrefs.SetInt("KilicliAsker", kilicliasker);PlayerPrefs.SetInt("OkcuAsker", okcuasker);PlayerPrefs.SetInt("atl�asker", atliasker);
        PlayerPrefs.SetInt("pantheon", pantheon);PlayerPrefs.SetInt("sovalye", sovalye);PlayerPrefs.SetInt("atl�sovalye", atlisovalye);
        PlayerPrefs.SetInt("manc�n�k", mancinik);PlayerPrefs.SetInt("ko�ba��", kocbasi);
        PlayerPrefs.SetInt("�ifac�", sifaci);PlayerPrefs.SetInt("atl�okcu",atliokcu);

        PlayerPrefs.SetFloat("SG", SavasGucu);
        //Gold Kaydetme
        PlayerPrefs.SetFloat("Gold", Gold);
        PlayerPrefs.SetFloat("GoldMining", goldMining);
        PlayerPrefs.SetInt("GoldMiningLV", goldMiningLv);

        PlayerPrefs.SetInt("a1", a1);PlayerPrefs.SetInt("a2", a2);PlayerPrefs.SetInt("a3", a3);
        PlayerPrefs.SetInt("a4", a4);PlayerPrefs.SetInt("a5", a5);PlayerPrefs.SetInt("a6", a6);
        PlayerPrefs.SetInt("a7", a7);PlayerPrefs.SetInt("a8", a8);PlayerPrefs.SetInt("a9", a9);PlayerPrefs.SetInt("a10", a10);

        //tower�n yok edilip edilmedi�i durumu  
        PlayerPrefs.SetInt("t1", t[1]); PlayerPrefs.SetInt("t2", t[2]); PlayerPrefs.SetInt("t3", t[3]); PlayerPrefs.SetInt("t4", t[4]); PlayerPrefs.SetInt("t5", t[5]);
        PlayerPrefs.SetInt("t6", t[6]); PlayerPrefs.SetInt("t7", t[7]); PlayerPrefs.SetInt("t8", t[8]); PlayerPrefs.SetInt("t9", t[9]); PlayerPrefs.SetInt("t10", t[10]);

        // PlayerPrefs.SetString("GoldLeveltext", GoldLevelText.ToString()); PlayerPrefs.SetString("GoldPrice", GoldPrice.ToString());

        PlayerPrefs.SetString("a1_text", a1_text.text); PlayerPrefs.SetString("a2_text", a2_text.text); PlayerPrefs.SetString("a3_text", a3_text.text); PlayerPrefs.SetString("a4_text", a4_text.text);
        PlayerPrefs.SetString("a5_text", a5_text.text); PlayerPrefs.SetString("a6_text", a6_text.text); PlayerPrefs.SetString("a7_text", a7_text.text);
        PlayerPrefs.SetString("a8_text", a8_text.text); PlayerPrefs.SetString("a9_text", a9_text.text); PlayerPrefs.SetString("a10_text", a10_text.text);

        //UpgradePrice Kaydetme
        PlayerPrefs.SetString("a1_uptext", a1_uptext.text); PlayerPrefs.SetString("a2_uptext", a2_uptext.text); PlayerPrefs.SetString("a3_uptext", a3_uptext.text); PlayerPrefs.SetString("a4_uptext", a4_uptext.text);
        PlayerPrefs.SetString("a5_uptext", a5_uptext.text); PlayerPrefs.SetString("a6_uptext", a6_uptext.text); PlayerPrefs.SetString("a7_uptext", a7_uptext.text);
        PlayerPrefs.SetString("a8_uptext", a8_uptext.text); PlayerPrefs.SetString("a9_uptext", a9_uptext.text); PlayerPrefs.SetString("a10_uptext", a10_uptext.text);
        //BuyPrice Kaydetme
        PlayerPrefs.SetString("a1_buytext", a1_buytext.text); PlayerPrefs.SetString("a2_buytext", a2_buytext.text); PlayerPrefs.SetString("a3_buytext", a3_buytext.text); PlayerPrefs.SetString("a4_buytext", a4_buytext.text);
        PlayerPrefs.SetString("a5_buytext", a5_buytext.text); PlayerPrefs.SetString("a6_buytext", a6_buytext.text); PlayerPrefs.SetString("a7_buytext", a7_buytext.text);
        PlayerPrefs.SetString("a8_buytext", a8_buytext.text); PlayerPrefs.SetString("a9_buytext", a9_buytext.text); PlayerPrefs.SetString("a10_buytext", a10_buytext.text);
        //SeviyeText Kaydetme
        PlayerPrefs.SetString("a1_lvtext", a1_lvtext.text); PlayerPrefs.SetString("a2_lvtext", a2_lvtext.text); PlayerPrefs.SetString("a3_lvtext", a3_lvtext.text); PlayerPrefs.SetString("a4_lvtext", a4_lvtext.text);
        PlayerPrefs.SetString("a5_lvtext", a5_lvtext.text); PlayerPrefs.SetString("a6_lvtext", a6_lvtext.text); PlayerPrefs.SetString("a7_lvtext", a7_lvtext.text);
        PlayerPrefs.SetString("a8_lvtext", a8_lvtext.text); PlayerPrefs.SetString("a9_lvtext", a9_lvtext.text); PlayerPrefs.SetString("a10_lvtext", a10_lvtext.text);

        PlayerPrefs.Save();
        
        PlayerPrefs.DeleteAll();// burdan delete all diyerek kontrol etmelisin!!!!!
    }
    public void EqualData()
    {//Asker Say�s� �ekme
       kilicliasker = PlayerPrefs.GetInt("KilicliAsker");okcuasker= PlayerPrefs.GetInt("OkcuAsker");atliasker=PlayerPrefs.GetInt("atl�asker");
        pantheon=PlayerPrefs.GetInt("pantheon");sovalye=PlayerPrefs.GetInt("sovalye");atlisovalye=PlayerPrefs.GetInt("atl�sovalye");
        mancinik =PlayerPrefs.GetInt("manc�n�k");kocbasi =PlayerPrefs.GetInt("ko�ba��");
        sifaci =PlayerPrefs.GetInt("�ifac�");atliokcu= PlayerPrefs.GetInt("atl�okcu");

        SavasGucu= PlayerPrefs.GetFloat("SG");

        Gold =PlayerPrefs.GetFloat("Gold");
        goldMining= PlayerPrefs.GetFloat("GoldMining");
        goldMiningLv=PlayerPrefs.GetInt("GoldMiningLV");
        //Asker seviyesi �ekme
        a1=PlayerPrefs.GetInt("a1");a2 = PlayerPrefs.GetInt("a2");a3 = PlayerPrefs.GetInt("a3");a4=PlayerPrefs.GetInt("a4");
        a5=PlayerPrefs.GetInt("a5");a6=PlayerPrefs.GetInt("a6");a7=PlayerPrefs.GetInt("a7");
        a8=PlayerPrefs.GetInt("a8");a9=PlayerPrefs.GetInt("a9");a10=PlayerPrefs.GetInt("a10");
        //Asker Say�s� E�itleme
        a1_text.text = PlayerPrefs.GetString("a1_text"); a2_text.text = PlayerPrefs.GetString("a2_text"); a3_text.text = PlayerPrefs.GetString("a3_text");
        a4_text.text = PlayerPrefs.GetString("a4_text"); a5_text.text = PlayerPrefs.GetString("a5_text"); a6_text.text = PlayerPrefs.GetString("a6_text");
        a7_text.text = PlayerPrefs.GetString("a7_text"); a8_text.text = PlayerPrefs.GetString("a8_text"); a9_text.text = PlayerPrefs.GetString("a9_text"); a10_text.text = PlayerPrefs.GetString("a10_text");
        //UpgradePrice E�itleme
        a1_uptext.text = PlayerPrefs.GetString("a1_uptext"); a2_uptext.text = PlayerPrefs.GetString("a2_uptext"); a3_uptext.text = PlayerPrefs.GetString("a3_uptext");
        a4_uptext.text = PlayerPrefs.GetString("a4_uptext"); a5_uptext.text = PlayerPrefs.GetString("a5_uptext"); a6_uptext.text = PlayerPrefs.GetString("a6_uptext");
        a7_uptext.text = PlayerPrefs.GetString("a7_uptext"); a8_uptext.text = PlayerPrefs.GetString("a8_uptext"); a9_uptext.text = PlayerPrefs.GetString("a9_uptext"); a10_uptext.text = PlayerPrefs.GetString("a10_uptext");
        //BuyPrice E�itleme
        a1_buytext.text = PlayerPrefs.GetString("a1_buytext"); a2_buytext.text = PlayerPrefs.GetString("a2_buytext"); a3_buytext.text = PlayerPrefs.GetString("a3_buytext");
        a4_buytext.text = PlayerPrefs.GetString("a4_buytext"); a5_buytext.text = PlayerPrefs.GetString("a5_buytext"); a6_buytext.text = PlayerPrefs.GetString("a6_buytext");
        a7_buytext.text = PlayerPrefs.GetString("a7_buytext"); a8_buytext.text = PlayerPrefs.GetString("a8_buytext"); a9_buytext.text = PlayerPrefs.GetString("a9_buytext"); a10_buytext.text = PlayerPrefs.GetString("a10_buytext");
        //Level E�itleme
        a1_lvtext.text = PlayerPrefs.GetString("a1_lvtext"); a2_lvtext.text = PlayerPrefs.GetString("a2_lvtext"); a3_lvtext.text = PlayerPrefs.GetString("a3_lvtext");
        a4_lvtext.text = PlayerPrefs.GetString("a4_lvtext"); a5_lvtext.text = PlayerPrefs.GetString("a5_lvtext"); a6_lvtext.text = PlayerPrefs.GetString("a6_lvtext");
        a7_lvtext.text = PlayerPrefs.GetString("a7_lvtext"); a8_lvtext.text = PlayerPrefs.GetString("a8_lvtext"); a9_lvtext.text = PlayerPrefs.GetString("a9_lvtext"); a10_lvtext.text = PlayerPrefs.GetString("a10_lvtext");

        t[1] = PlayerPrefs.GetInt("t1");t[2] = PlayerPrefs.GetInt("t2"); t[3] = PlayerPrefs.GetInt("t3"); t[4] = PlayerPrefs.GetInt("t4"); t[5] = PlayerPrefs.GetInt("t5");
        t[6] = PlayerPrefs.GetInt("t6"); t[7] = PlayerPrefs.GetInt("t7"); t[8] = PlayerPrefs.GetInt("t8"); t[9] = PlayerPrefs.GetInt("t9"); t[10] = PlayerPrefs.GetInt("t10");

    }

    private void OnApplicationQuit()//applicationexit gibi bir method var m�?!
    {
        SaveData();
    }
}

