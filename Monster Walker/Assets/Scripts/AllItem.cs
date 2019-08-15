using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllItem : MonoBehaviour
{
    public static AllItem instance { get; private set; }
    public List<Equipment> All_Weapon = new List<Equipment>();
    public List<Equipment> All_Armor = new List<Equipment>();
    public List<Equipment> All_Acc = new List<Equipment>();
    public List<Equipment> All_Potion = new List<Equipment>();
    private AllSkill askill;

    public GameObject prefabEquip, prefabShop, prefabPotion, prefabSkill;
    private UIManager ui;

    [System.Serializable]
    public class ItemHolder {
        public Transform Holder;
        public Transform ShopHolder;
    }
    public ItemHolder wh = new ItemHolder();
    
    public ItemHolder ah = new ItemHolder();

    public ItemHolder acch = new ItemHolder();

    public ItemHolder ph = new ItemHolder();

    public ItemHolder sh = new ItemHolder();
     



    public bool updStat = true;
    // public List<Equipment> All_Potion = new List<Equipment>();

    // Start is called before the first frame update

    private void Awake()
    {
        askill = FindObjectOfType<AllSkill>();
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        ui = FindObjectOfType<UIManager>();
        SetItemsOnStatus();
        SetItemsOnShop();
        SetShopPotion();
        SetSkillHolder();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName != "GameScreen")
        {
            DestroyImmediate(this.gameObject);
        }
    }

    #region Equipment Status

    public void SetWeaponHolder()
    {
       
        for (int i = 0; i < All_Weapon.Count; i++)
        {
             GameObject pe;
            pe = (GameObject)Instantiate(prefabEquip) as GameObject;
            pe.transform.SetParent(wh.Holder);
            pe.transform.localScale = new Vector3(1, 1, 1);
            pe.GetComponent<EquipStatus>().CurrentEquipStatus(All_Weapon[i].IsUnlocked, All_Weapon[i].IsEquiped, 
                All_Weapon[i].i_name, All_Weapon[i].i_atk, All_Weapon[i].i_spd, All_Weapon[i].i_hp, All_Weapon[i].i_Sprite, 
                All_Weapon[i].i_type.ToString());

            if (ZPlayerPrefs.GetInt(All_Weapon[i].i_name) == 1)
            {
                if (!updStat)
                {
                    ui.equipHp -= pe.GetComponent<EquipStatus>().e_hp;
                    ui.equipStr -= pe.GetComponent<EquipStatus>().e_str;
                    ui.equipSpd -= pe.GetComponent<EquipStatus>().e_spd;
                }
                pe.GetComponent<EquipStatus>().Equip = true;
                ui.equipHp += pe.GetComponent<EquipStatus>().e_hp;
                ui.equipStr += pe.GetComponent<EquipStatus>().e_str;
                ui.equipSpd += pe.GetComponent<EquipStatus>().e_spd;
                ui.SUI.CurWea.sprite = pe.GetComponent<EquipStatus>().e_sprite.sprite;
                ui.SUI.e_w_c = 1;
            }
            
        }
    }

    void SetArmorHolder()
    {
        for (int i = 0; i < All_Armor.Count; i++)
        {
            //EquipStatus es = new EquipStatus();
            GameObject pe;
            pe = (GameObject)Instantiate(prefabEquip) as GameObject;
            pe.transform.SetParent(ah.Holder);
            pe.transform.localScale = new Vector3(1, 1, 1);
            pe.GetComponent<EquipStatus>().CurrentEquipStatus(All_Armor[i].IsUnlocked,
                All_Armor[i].IsEquiped, All_Armor[i].i_name, All_Armor[i].i_atk, All_Armor[i].i_spd,
                All_Armor[i].i_hp, All_Armor[i].i_Sprite, All_Armor[i].i_type.ToString());

            if (ZPlayerPrefs.GetInt(All_Armor[i].i_name) == 1)
            {
                if (!updStat)
                {
                    ui.equipHp -= pe.GetComponent<EquipStatus>().e_hp;
                    ui.equipStr -= pe.GetComponent<EquipStatus>().e_str;
                    ui.equipSpd -= pe.GetComponent<EquipStatus>().e_spd;
                }
                pe.GetComponent<EquipStatus>().Equip = true;
                ui.equipHp += pe.GetComponent<EquipStatus>().e_hp;
                ui.equipStr += pe.GetComponent<EquipStatus>().e_str;
                ui.equipSpd += pe.GetComponent<EquipStatus>().e_spd;
                ui.SUI.CurArm.sprite = pe.GetComponent<EquipStatus>().e_sprite.sprite;
                ui.SUI.e_a_c = 1;
            }


        }
    }

    void SetAccessoryHolder()
    {
        for (int i = 0; i < All_Acc.Count; i++)
        {
            //EquipStatus es = new EquipStatus();
            GameObject pe;
            pe = (GameObject)Instantiate(prefabEquip) as GameObject;
            pe.transform.SetParent(acch.Holder);
            pe.transform.localScale = new Vector3(1, 1, 1);
            pe.GetComponent<EquipStatus>().CurrentEquipStatus(All_Acc[i].IsUnlocked, All_Acc[i].IsEquiped, All_Acc[i].i_name, All_Acc[i].i_atk, All_Acc[i].i_spd, All_Acc[i].i_hp, All_Acc[i].i_Sprite, All_Acc[i].i_type.ToString());

            if (ZPlayerPrefs.GetInt(All_Acc[i].i_name) == 1)
            {
                if (!updStat)
                {
                    ui.equipHp -= pe.GetComponent<EquipStatus>().e_hp;
                    ui.equipStr -= pe.GetComponent<EquipStatus>().e_str;
                    ui.equipSpd -= pe.GetComponent<EquipStatus>().e_spd;
                }
                pe.GetComponent<EquipStatus>().Equip = true;
                ui.equipHp += pe.GetComponent<EquipStatus>().e_hp;
                ui.equipStr += pe.GetComponent<EquipStatus>().e_str;
                ui.equipSpd += pe.GetComponent<EquipStatus>().e_spd;
                ui.SUI.CurAcc.sprite = pe.GetComponent<EquipStatus>().e_sprite.sprite;
                ui.SUI.e_acc_c = 1;
            }


        }

    }

    void SetSkillHolder() {
        for (int i = 0; i < askill.All_Skill.Count; i++)
        {
            GameObject pe;
            pe = (GameObject)Instantiate(prefabSkill) as GameObject;
            pe.transform.SetParent(sh.Holder);
            pe.transform.localScale = new Vector3(1, 1, 1);
            pe.GetComponent<SkillOnStatus>().CurrentSkillStatus(askill.All_Skill[i].IsUnlocked, askill.All_Skill[i].IsEquipped,
                askill.All_Skill[i].SkillName, askill.All_Skill[i].SkillSprite, askill.All_Skill[i].SkillBio, askill.All_Skill[i].SkillCost, i);

            if (ZPlayerPrefs.GetInt(askill.All_Skill[i].SkillName) == 1)
            {
                pe.GetComponent<SkillOnStatus>().Equip = true;
                ui.SUI.CurSkill.sprite = pe.GetComponent<SkillOnStatus>().s_sprite.sprite;
                ui.SUI.s_c = 1;
            }


        }
    }


    #endregion

    #region Shop Status

    void SetShopWeapon()
    {
        for (int i = 0; i < All_Weapon.Count; i++)
        {
            GameObject pe;
            pe = (GameObject)Instantiate(prefabShop) as GameObject;
//            pe.transform.parent = wh.ShopHolder;
            pe.transform.SetParent(wh.ShopHolder);
            pe.transform.localScale = new Vector3(1, 1, 1);
            pe.GetComponent<ShopStatus>().CurrentShopStatus(All_Weapon[i].IsUnlocked, All_Weapon[i].i_name, All_Weapon[i].i_atk, All_Weapon[i].i_spd, All_Weapon[i].i_hp, All_Weapon[i].i_Sprite, All_Weapon[i].price, All_Weapon[i].i_type.ToString());

            if (ZPlayerPrefs.GetInt(All_Weapon[i].i_name + "_s") == 1)
            {
                pe.GetComponent<ShopStatus>().Unlock = true;
            }

        }

    }

    void SetShopArmor()
    {
        for (int i = 0; i < All_Armor.Count; i++)
        {
            GameObject pe;
            pe = (GameObject)Instantiate(prefabShop) as GameObject;
            //  pe.transform.parent = ah.ShopHolder;
            pe.transform.SetParent(ah.ShopHolder);
            pe.transform.localScale = new Vector3(1, 1, 1);
            pe.GetComponent<ShopStatus>().CurrentShopStatus(All_Armor[i].IsUnlocked, All_Armor[i].i_name, All_Armor[i].i_atk, All_Armor[i].i_spd, All_Armor[i].i_hp, All_Armor[i].i_Sprite, All_Armor[i].price, All_Armor[i].i_type.ToString());

            if (ZPlayerPrefs.GetInt(All_Armor[i].i_name + "_s") == 1)
            {
                pe.GetComponent<ShopStatus>().Unlock = true;
            }

        }

    }

    void SetShopAccessory()
    {
        for (int i = 0; i < All_Acc.Count; i++)
        {
            GameObject pe;
            pe = (GameObject)Instantiate(prefabShop) as GameObject;
            // pe.transform.parent = acch.ShopHolder;
            pe.transform.SetParent(acch.ShopHolder);
            pe.transform.localScale = new Vector3(1, 1, 1);
            pe.GetComponent<ShopStatus>().CurrentShopStatus(All_Acc[i].IsUnlocked, All_Acc[i].i_name, All_Acc[i].i_atk, All_Acc[i].i_spd, All_Acc[i].i_hp, All_Acc[i].i_Sprite, All_Acc[i].price, All_Acc[i].i_type.ToString());

            if (ZPlayerPrefs.GetInt(All_Acc[i].i_name + "_s") == 1)
            {
                pe.GetComponent<ShopStatus>().Unlock = true;
            }

        }

    }

    void SetShopPotion()
    {
        for (int i = 0; i < All_Potion.Count; i++)
        {
            GameObject pe;
            pe = (GameObject)Instantiate(prefabPotion) as GameObject;
            pe.transform.SetParent(ph.ShopHolder);
            pe.transform.localScale = new Vector3(1, 1, 1);
            pe.GetComponent<PotionShopStatus>().CurrentShopStatus(All_Potion[i].i_name, All_Potion[i].i_hp, All_Potion[i].i_Sprite, All_Potion[i].price);
            
        }

    }
    
    #endregion

    public void RemoveCloneStatus() {
        foreach (Transform child in wh.Holder)
        {
            DestroyImmediate(child.gameObject);
        }
        foreach (Transform child in ah.Holder)
        {
            DestroyImmediate(child.gameObject);
        }
        foreach (Transform child in acch.Holder)
        {
            DestroyImmediate(child.gameObject);
        }

    }

    public void SetItemsOnStatus() {
        SetWeaponHolder();
        SetArmorHolder();
        SetAccessoryHolder();
    }

    void SetItemsOnShop() {
        SetShopWeapon();
        SetShopArmor();
        SetShopAccessory();
        //potion blum

        if (!updStat)
        {
            updStat = true;
        }

    }

}
