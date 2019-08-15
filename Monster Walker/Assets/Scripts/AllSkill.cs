using System.Collections.Generic;
using UnityEngine;

public class AllSkill : MonoBehaviour
{
    public static AllSkill instance;
    public List<Skill> All_Skill = new List<Skill>();

    private void Awake()
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
    }


}
