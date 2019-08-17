using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopStatus : MonoBehaviour
{
    public GameObject BuyBtn, BoughtBtn, warningtxt;
    public bool Unlock = false;
    private string e_name, equipType;[SerializeField] public int e_str, e_hp, e_spd, e_price;
    public SpriteRenderer e_sprite;
    [SerializeField] private Text equipName, equipHP, equipSTR, equipSPD, equipPrice;
    private UIManager ui = new UIManager();
    public bool isUnlocked;
    private AudioScript AS;
    
    private void Awake()
    {
        AS = FindObjectOfType<AudioScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        PlayerPrefs.GetInt(e_name + "_s", 0);
    }

    // Update is called once per frame
    void Update()
    {
        SetToText();
        e_sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        if (PlayerPrefs.GetInt(e_name + "_s") == 1)
        {
            Unlock = true;
        }

        if (Unlock)
        {
            BuyBtn.SetActive(false);
        }
        
    }

    public void CurrentShopStatus(bool u, string equipname, int equipstr, int equipspd, int equiphp, Sprite equipSprite, int cost, string type)
    {
        e_sprite.sprite = equipSprite;
        e_name = equipname;
        e_hp = equiphp;
        e_str = equipstr;
        e_spd = equipspd;
        e_price = cost;
        equipType = type;
        Unlock = u;
    }
    
    void SetToText()
    {
        equipName.text = e_name;

        if (e_hp >= 0)
        {
            equipHP.text = "+" + e_hp;
        }
        else
        {
            equipHP.text = e_hp.ToString();
        }

        if (e_str >= 0)
        {
            equipSTR.text = "+" + e_str;
        }
        else
        {
            equipSTR.text = e_str.ToString();
        }

        if (e_spd >= 0)
        {
            equipSPD.text = "+" + e_spd;
        }
        else
        {
            equipSPD.text = e_spd.ToString();
        }

        equipPrice.text = e_price.ToString();

    }


    public void buyEquip() {
        if (GoldManager.instance.GOLD >= e_price)
        {
            AllItem.instance.RemoveCloneStatus();
            AllItem.instance.RemoveCloneStatus();
            AllItem.instance.RemoveCloneStatus();
            GoldManager.instance.RemoveGold(e_price);
            PlayerPrefs.SetInt(e_name + "_s", 1);

            if (equipType == "Weapon")
            {
                for (int i = 0; i < AllItem.instance.All_Weapon.Count; i++)
                {
                    if (AllItem.instance.All_Weapon[i].i_name == e_name)
                    {
                        AllItem.instance.All_Weapon[i].IsUnlocked = true;
                    }
                }
            }

            if (equipType == "Armor")
            {
                for (int i = 0; i < AllItem.instance.All_Armor.Count; i++)
                {
                    if (AllItem.instance.All_Armor[i].i_name == e_name)
                    {
                        AllItem.instance.All_Armor[i].IsUnlocked = true;
                    }
                }


            }

            if (equipType == "Accessory")
            {
                for (int i = 0; i < AllItem.instance.All_Acc.Count; i++)
                {
                    if (AllItem.instance.All_Acc[i].i_name == e_name)
                    {
                        AllItem.instance.All_Acc[i].IsUnlocked = true;
                    }
                }


            }

            AllItem.instance.updStat = false;
            AllItem.instance.SetItemsOnStatus();

        }
        else
        {
            StartCoroutine(WarningText());
        }
    }

    public void BuySound()
    {
        AS.SFXSource.clip = AS.BuySound;
        AS.SFXSource.Play();
    }

    IEnumerator WarningText()
    {
        warningtxt.SetActive(true);
        yield return new WaitForSeconds(1f);
        warningtxt.SetActive(false);
    }

}
