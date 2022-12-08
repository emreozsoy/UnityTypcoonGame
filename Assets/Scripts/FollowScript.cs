using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowScript : MonoBehaviour
{
    [SerializeField] public GameObject SetBG;

    [SerializeField] public ScrollRect StopScroll;

    [SerializeField] public Scrollbar MapScrollBar;

    [SerializeField] public Image ArmySprite;
    [SerializeField] public Sprite RSprite;
    [SerializeField] public Sprite LSprite;

    [SerializeField]
    private Transform[] routes;
    [SerializeField]
    private Transform[] routesObj;

    private int routeToGo;

    private float tParam;

    private Vector2 objectPosition;

    private float speedModifier;

    private bool coroutineAllowed;

    private int count;

    private float trans_0Y,trans_1Y;

    private int count2;

    private bool WorkedOnce=true;

    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 1f;
        coroutineAllowed = false;
        routeToGo = PlayerPrefs.GetInt("GoTo");
        count = PlayerPrefs.GetInt("count");
        trans_0Y = PlayerPrefs.GetFloat("trans_0Y");
        trans_1Y = PlayerPrefs.GetFloat("trans_1Y");

        if (trans_0Y == 0)
        {
            trans_0Y = routesObj[0].position.y;
        }
        if (trans_1Y == 0)
        {
            trans_1Y = routesObj[1].position.y;
        }
    }

    void Update()
    {
        if (coroutineAllowed)
        {
            if (count2 >=33 && WorkedOnce)
            {
                MapScrollBar.value = 0.7f;

                StopScroll.enabled = false;
                StartCoroutine(WaitForEqual());
            }
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }
    IEnumerator WaitForEqual() // setbg Y AYARLA - Scroll enabled ayarla 
    {
    
        WorkedOnce = false;

        // Waiting just one frame is probably good enough, yield return null does that

        yield return new WaitForEndOfFrame();

        MapScrollBar.value = 0.7f;

        StopScroll.enabled = false;
        //SetBG.transform.position = new Vector3(SetBG.transform.position.x, 100 , SetBG.transform.position.z); BG ayarlama için denemeydi


        //Ekraný Kaydýrmayý engelleme

        //Yumuþak geçiþ için for için deðeri azaltarak yada arttýrarak eþitleme yapabiliriz

        Debug.Log("Buraya Geldim.");

        trans_0Y = 675f;
        trans_1Y = 250f;
        
        routesObj[0].position = new Vector3(routesObj[0].position.x, trans_0Y, routesObj[0].position.z);
        routesObj[1].position = new Vector3(routesObj[1].position.x, trans_1Y, routesObj[1].position.z);

        yield return new WaitForSeconds(4f);
        yield break;
    }
        private IEnumerator GoByTheRoute(int routeNum)
    {
        if (count2 >= 33 && WorkedOnce)
        {
            StartCoroutine(WaitForEqual());
        }
        else if(count2<33)
        {
            MapScrollBar.value = 0.1117567f;
            StopScroll.enabled = false;
        }
        count2++;
            
        //Debug.Log(count2);

        coroutineAllowed = false;

        routesObj[0].position = new Vector3(routesObj[0].position.x, trans_0Y, routesObj[0].position.z);
        routesObj[1].position = new Vector3(routesObj[1].position.x, trans_1Y, routesObj[1].position.z);

        
        if (routes[routeNum] == null) //dizi boþ veya dizinin 4. elemanýna geldiysek durdur
        {
            count++;
            PlayerPrefs.SetInt("GoTo", 0);
            routeToGo = PlayerPrefs.GetInt("GoTo");
            PlayerPrefs.SetInt("count", count);


            if (count >=2)
            {
                ArmySprite.sprite = LSprite;
                trans_0Y += 431;
                routesObj[0].position = new Vector3(routesObj[0].position.x, trans_0Y, routesObj[0].position.z);
                PlayerPrefs.SetFloat("trans_0Y", trans_0Y);
            }
            yield break;
        }
        if (routeNum == 4 && count %2 != 1)
        {
            count++;
            PlayerPrefs.SetInt("GoTo", 4);
            routeToGo = PlayerPrefs.GetInt("GoTo");
            PlayerPrefs.SetInt("count", count);

            ArmySprite.sprite = RSprite;

            if (count > 2)
            {
                trans_1Y += 431;
                routesObj[1].position = new Vector3(routesObj[1].position.x, trans_1Y, routesObj[1].position.z);
                PlayerPrefs.SetFloat("trans_1Y", trans_1Y);
            }
            yield break;
        }
        
        Vector2 p0 = routes[routeNum].GetChild(0).position;
        Vector2 p1 = routes[routeNum].GetChild(1).position;
        Vector2 p2 = routes[routeNum].GetChild(2).position;
        Vector2 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
            yield break; //break ile son kýsma geldiðimizde çýkýyoruz
        }

        StartCoroutine(CheckMoveDone());

        coroutineAllowed = true;
    }
    public void MoveArmy()
    {

        coroutineAllowed = true;
    }

    IEnumerator CheckMoveDone()//Ordu Ýlerleme Hareketinin bitip bitmedðini kontrol eder 
    {
        float LastYpos = this.transform.position.y;
        yield return new WaitForSeconds(0.5f);
        if (this.transform.position.y == LastYpos)
        {
            Debug.Log("lASTpos");
            StopScroll.enabled = true;
         
        }

        yield break;
    }
    
}
