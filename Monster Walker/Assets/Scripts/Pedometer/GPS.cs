using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GPS : MonoBehaviour
{
    public static GPS instance { get; private set; }
    public double longitude;
    public double latitude;
    private bool goNext;

    private void Awake()
    {
        
        instance = this;
        DontDestroyOnLoad(this);
       //PlayerPrefs.DeleteAll();
        StartCoroutine(WaitLogoAppear());
    }

    //IEnumerator Start() {
    //    Input.location.Start();


    //    if (Input.location.status == LocationServiceStatus.Failed)
    //    {
    //        Debug.Log("Unable to determine device location");
    //        yield break;
    //    }

    //}

    //public void Update()
    //{
            
    //    longitude = Input.location.lastData.longitude;
    //    latitude = Input.location.lastData.latitude;
        
    //}

    IEnumerator WaitLogoAppear() {
        yield return new WaitForSeconds(1f);
       Application.LoadLevel("OpeningMenu");
        
    }


}
