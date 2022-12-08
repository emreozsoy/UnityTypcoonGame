/*using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour
{
    public RewardedAd reklamObjesi;

    void Start()
    {
        MobileAds.Initialize(reklamDurumu => { });
    }

    // Ekranda test amaçlý "Reklamý Göster" butonu göstermeye yarar, bu fonksiyonu silerseniz buton yok olur
    public void ShowAds()
    {   
            if (reklamObjesi != null)
                

            reklamObjesi = new RewardedAd("ca-app-pub-3940256099942544/1033173712");
            AdRequest reklamIstegi = new AdRequest.Builder().Build();
            reklamObjesi.LoadAd(reklamIstegi);

            StartCoroutine(ReklamiGoster());
        
    }

    IEnumerator ReklamiGoster()
    {
        while (!reklamObjesi.IsLoaded())
            yield return null;

        reklamObjesi.Show();
    }

 
}
*/