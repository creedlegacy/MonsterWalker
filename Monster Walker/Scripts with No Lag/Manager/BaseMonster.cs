using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    public Sprite image;
    public Sprite Element;
    public int m_num;//monster apa yg di pilih
    public string m_name;//nama monster
    public MonsterType m_type;//element monster
    public int m_hp;
    public int m_atk;
    //public int m_def;
    public int m_spd;
    public RuntimeAnimatorController spriteAnimation;

    
}


public enum MonsterType {
    Fire,
    Grass,
    Water
}

