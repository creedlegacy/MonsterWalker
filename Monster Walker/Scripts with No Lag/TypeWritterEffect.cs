using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWritterEffect : MonoBehaviour
{

    public float delay = 0.1f;
    public List<string> ListOfText = new List<string>();
    public GameObject StoryImage,StoryImage1,StoryImage2;
    private int counter = 0;
    private string currentText = "";
    private bool TextStillMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText() {
        TextStillMoving = true;
        for (int i = 0; i <= ListOfText[counter].Length; i++){
            if (!TextStillMoving)
            {
                break;
            }
            currentText = ListOfText[counter].Substring(0,i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        TextStillMoving = false;
    }


    public void nextText() {

        if (TextStillMoving)
        {
            
            TextStillMoving = false;
            this.GetComponent<Text>().text = ListOfText[counter];
        }
        else
        {
            counter++;
            if (counter < 10)
            {
                if(counter == 1)
                {
                    StoryImage.SetActive(true);
                }
                
                if (counter == 4)
                {
                    StoryImage.SetActive(false);
                    StoryImage1.SetActive(true);
                }
                if (counter == 6)
                {
                    StoryImage1.SetActive(false);
                    StoryImage2.SetActive(true);
                }

                StartCoroutine(ShowText());
            }
            else
            {
                Application.LoadLevel("GameScreen");
            }
        }
    }

}
