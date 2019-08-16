﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EquipStatus : MonoBehaviour
{
    public GameObject LockBtn, EquipBtn, UnequipBtn, warningtxt;
    private bool Unlock = false;
    private string e_name; [SerializeField]public int e_str, e_hp, e_spd;
    public SpriteRenderer e_sprite;
    [SerializeField]private Text equipName, equipHP, equipSTR, equipSPD;
    private UIManager ui = new UIManager();
    public bool isEquiped, Equip = false;
    [SerializeField]private string type;

    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        ZPlayerPrefs.GetInt(e_name, 0);
        ZPlayerPrefs.GetInt(e_name + "_e", 0);

        if (ZPlayerPrefs.GetInt(e_name) == 1)
        { Equip = true; }

        if (Unlock || ZPlayerPrefs.GetInt(e_name + "_s") == 1)
        {
            LockBtn.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        SetToText();
        e_sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

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
            ui.equipHp += e_hp;
            ui.equipStr += e_str;
            ui.equipSpd += e_spd;
            ui.SUI.CurWea.sprite = e_sprite.sprite;
        }

    }

    public void CurrentEquipStatus(bool u, bool e, string equipname, int equipstr, int equipspd, int equiphp, Sprite equipSprite, string typeItem) {
        e_sprite.sprite = equipSprite;
        e_name = equipname;
        e_hp = equiphp;
        e_str = equipstr;
        e_spd = equipspd;
        Unlock = u;
        Equip = e;
        type = typeItem;
    }

    void SetToText() {
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


    }

    public void EquipItem() {
        if (!Equip)
        {
            if (type == "Weapon")
            {
                if (ui.SUI.e_w_c == 0)
                {
                    Equip = true;
                    ui.SUI.e_w_c = 1;
                    ui.equipHp += e_hp;
                    ui.equipStr += e_str;
                    ui.equipSpd += e_spd;
                    ui.SUI.CurWea.sprite = e_sprite.sprite;
                    ZPlayerPrefs.SetInt(e_name, 1);
                }
                else
                {
                    //warning pake weapon 1
                    StartCoroutine(WarningText());
                }
            } else if (type == "Armor") {
                if (ui.SUI.e_a_c == 0)
                {
                    Equip = true;
                    ui.SUI.e_a_c = 1;
                    ui.equipHp += e_hp;
                    ui.equipStr += e_str;
                    ui.equipSpd += e_spd;
                    ui.SUI.CurArm.sprite = e_sprite.sprite;
                    ZPlayerPrefs.SetInt(e_name, 1);
                }
                else
                {
                    //warning pake armor 1
                    StartCoroutine(WarningText());
                }
            }
            else if (type == "Accessory")
            {
                if (ui.SUI.e_acc_c == 0)
                {
                    Equip = true;
                    ui.SUI.e_acc_c = 1;
                    ui.equipHp += e_hp;
                    ui.equipStr += e_str;
                    ui.equipSpd += e_spd;
                    ui.SUI.CurAcc.sprite = e_sprite.sprite;
                    ZPlayerPrefs.SetInt(e_name, 1);
                }
                else
                {
                    //warning pake weapon 1
                    StartCoroutine(WarningText());
                }
            }
            
            
        }
        else
        {
            if (type == "Weapon")
            {
                ui.SUI.e_w_c = 0;
                Equip = false;
                ui.equipHp -= e_hp;
                ui.equipStr -= e_str;
                ui.equipSpd -= e_spd;
                ZPlayerPrefs.SetInt(e_name, 0);
            }

            if (type == "Armor")
            {
                ui.SUI.e_a_c = 0;
                Equip = false;
                ui.equipHp -= e_hp;
                ui.equipStr -= e_str;
                ui.equipSpd -= e_spd;
                ZPlayerPrefs.SetInt(e_name, 0);
            }

            if (type == "Accessory")
            {
                ui.SUI.e_acc_c = 0;
                Equip = false;
                ui.equipHp -= e_hp;
                ui.equipStr -= e_str;
                ui.equipSpd -= e_spd;
                ZPlayerPrefs.SetInt(e_name, 0);
            }
        }
    }

    IEnumerator WarningText() {
        warningtxt.SetActive(true);
        yield return new WaitForSeconds(1f);
        warningtxt.SetActive(false);
    }

}