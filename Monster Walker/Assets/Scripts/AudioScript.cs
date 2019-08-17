using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioScript instance { get; private set; }
    public AudioSource MusicSource, SFXSource;
    public AudioClip ButtonSound, LevelUpSound,BuySound,EquipSkillSound,UseSkillSound,EquipSound,HitSound,ShopMusic, OpeningMusic, GameScreenMusic, ExploreMusic, BattleMusic, TrainingMusic;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}
