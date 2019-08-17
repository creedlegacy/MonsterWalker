using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenBackground : MonoBehaviour
{
    public List<ImageList> BG_Sprite = new List<ImageList>();
    public SpriteRenderer background, foreground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (System.DateTime.Now.Hour >= 4 && System.DateTime.Now.Hour <= 9)
        {
            background.sprite = BG_Sprite[0].BG[0];
            foreground.sprite = BG_Sprite[0].BG[1];
            //foreg.sprite = BG_Sprite[0].BG[2];
            //bgDec.sprite = BG_Sprite[0].BG[3];
            //g1.sprite = BG_Sprite[0].BG[4];
            //g2.sprite = BG_Sprite[0].BG[5];
        }
        else if (System.DateTime.Now.Hour >= 10 && System.DateTime.Now.Hour <= 17)
        {
            background.sprite = BG_Sprite[1].BG[0];
            foreground.sprite = BG_Sprite[1].BG[1];
        //    foreg.sprite = BG_Sprite[1].BG[2];
        //    bgDec.sprite = BG_Sprite[1].BG[3];
        //    g1.sprite = BG_Sprite[1].BG[4];
        //    g2.sprite = BG_Sprite[1].BG[5];
        }
        else if(System.DateTime.Now.Hour >= 18 || System.DateTime.Now.Hour >= 0 && System.DateTime.Now.Hour <= 3)
        {
            background.sprite = BG_Sprite[2].BG[0];
            foreground.sprite = BG_Sprite[2].BG[1];
            //foreg.sprite = BG_Sprite[2].BG[2];
            //bgDec.sprite = BG_Sprite[2].BG[3];
            //g1.sprite = BG_Sprite[2].BG[4];
            //g2.sprite = BG_Sprite[2].BG[5];
        }
    }
}

[System.Serializable]
public class ImageList {
    public string nameBG;
    public List<Sprite> BG = new List<Sprite>();
}
