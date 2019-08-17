using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject Tutorial, L_Click, R_Click;
    public List<string> All_Tutorial = new List<string>();
    public GameObject TrainingArrow, ExploreArrow, BattleArrow, ShopArrow, StatusArrow,
        XPArrow, GoldArrow, PotionArrow, BattleTicketArrow, LevelArrow;
    public int page;
    public Text pageText, pageContent;


    private void Awake()
    {
        page = 0;
    }

    // Update is called once per frame
    void Update()
    {

        bool showLeftClick = (true ? page > 0 : page == 0);
        L_Click.SetActive(showLeftClick);

        bool showRightClick = (true ? page < All_Tutorial.Count - 1 : page == All_Tutorial.Count);
        R_Click.SetActive(showRightClick);

        switch (page)
        {
            case 0:
                TrainingArrow.SetActive(true);
                ; break;

            case 1:
                ExploreArrow.SetActive(true);
                ; break;
            case 2:
                BattleArrow.SetActive(true);
                ; break;
            case 3:
                ShopArrow.SetActive(true);
                ; break;
            case 4:
                StatusArrow.SetActive(true);
                ; break;
        }

        pageContent.text = All_Tutorial[page].ToString();
        pageText.text = (page + 1).ToString() + " / " + All_Tutorial.Count.ToString();

    }

    public void CloseTutorial()
    {
        Tutorial.SetActive(false);
    }

    public void OpenTutorial()
    {
        Tutorial.SetActive(true);
    }

    public void LeftClick() {
        page--;
        ResetArrow();
    }

    public void RightClick() {
        page++;
        ResetArrow();
    }

    void ResetArrow() {
        TrainingArrow.SetActive(false);
        ExploreArrow.SetActive(false);
        BattleArrow.SetActive(false);
        ShopArrow.SetActive(false);
        StatusArrow.SetActive(false);
        XPArrow.SetActive(false);
        GoldArrow.SetActive(false);
        PotionArrow.SetActive(false);
        BattleTicketArrow.SetActive(false);
        LevelArrow.SetActive(false);
    }

}
