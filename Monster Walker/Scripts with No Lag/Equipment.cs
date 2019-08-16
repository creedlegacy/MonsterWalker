using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Sprite i_Sprite;
    public string i_name;
    public ItemType i_type;
    public ItemEffect Effect1;
    public ItemEffect Effect2;
    public int price;
    public int i_hp;
    public int i_atk;
    public int i_spd;
    public string i_bio;
    public bool IsFree = false;
    public bool IsUnlocked = false;
    public bool IsEquiped = false;
}

 public enum ItemType
    {
        Weapon,
        Armor,
        Accessory,
        Potion
    }

public enum ItemEffect {
       NoEffect,
       FireResistance,
       WaterResistance,
       GrassResistance
}
