using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillOnStatus : MonoBehaviour
{
    public GameObject LockBtn, EquipBtn, UnequipBtn, warning, CostText, Warning2;
    private bool Unlock = false;
    private string s_name, s_bio;
    private int s_cost, num;
    public SpriteRenderer s_sprite;
    [SerializeField] private Text skillName, skillBio, Cost;
    private UIManager ui = new UIManager();
    public bool isEquiped, Equip = false;

    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        PlayerPrefs.GetInt(s_name, 0);
        PlayerPrefs.GetInt(s_name + "_e", 0);

        if (PlayerPrefs.GetInt(s_name) == 1)
        {
            Equip = true;
        }

        if (Unlock || PlayerPrefs.GetInt(s_name + "_e") == 1)
        {
            LockBtn.SetActive(false);
            CostText.SetActive(false);
        }

        PlayerPrefs.GetInt("EquipSkillNumber", -1);
        
    }

    // Update is called once per frame
    void Update()
    {
        SetToText();
        s_sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        if (Equip)
        {
            EquipBtn.SetActive(false);
            UnequipBtn.SetActive(true);
        }
        else
        {
            EquipBtn.SetActive(true);
            UnequipBtn.SetActive(false);
        }


        if (isEquiped)
        {
            isEquiped = false;
        }

     
    }

    public void CurrentSkillStatus(bool u, bool e, string skillname, Sprite skillSprite, string bio, int cost, int SkillNum)
    {
        s_sprite.sprite = skillSprite;
        s_name = skillname;
        s_bio = bio;
        s_cost = cost;
        Unlock = u;
        Equip = e;
        num = SkillNum;
    }

    void SetToText()
    {
        skillName.text = s_name;
        skillBio.text = s_bio;
        Cost.text = (s_cost + " XP").ToString();
    }

    public void BuySkill() {
        if (ExpManager.instance.EXP >= s_cost)
        {
            ExpManager.instance.RemoveEXP(s_cost);
            PlayerPrefs.SetInt(s_name + "_e", 1);
            LockBtn.SetActive(false);
            CostText.SetActive(false);
            ui.SUI.CurSkill.sprite = s_sprite.sprite;
            Cost.GetComponent<GameObject>().SetActive(false);
        }
        else
        {
            StartCoroutine(WarningDisplay());
        }
    }

    public void EquipItem()
    {
        if (!Equip)
        {
            if (ui.SUI.s_c == 0)
            {
                Equip = true;
                ui.SUI.s_c = 1;
                ui.SUI.CurSkill.sprite = s_sprite.sprite;
                PlayerPrefs.SetInt("EquipSkillNumber", num);
                PlayerPrefs.SetInt(s_name, 1);
            }
            else
            {
                StartCoroutine(Warning2Display());
            }

        }
        else
        {
            ui.SUI.s_c = 0;
            Equip = false;
            PlayerPrefs.SetInt("EquipSkillNumber", -1);
            PlayerPrefs.SetInt(s_name, 0);
        }
    }

     IEnumerator WarningDisplay() {
        warning.SetActive(true);
        yield return new WaitForSeconds(1f);
        warning.SetActive(false);
    }

    IEnumerator Warning2Display()
    {
        Warning2.SetActive(true);
        yield return new WaitForSeconds(1f);
        Warning2.SetActive(false);
    }


}
