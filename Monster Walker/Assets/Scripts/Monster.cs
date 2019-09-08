using UnityEngine;

public class Monster : MonoBehaviour
{

    public OwnedMonster OM;
    private GameObject sprite;
    private MonsterManager MM;
    public Animator An;
    private BattleScript BS;
    public bool SG;
    // Start is called before the first frame update
    public float idletime;
    [SerializeField]private ParallaxBackground pb;
    [SerializeField]private bool SpriteInStatus;

    private void Awake()
    {
        MM = FindObjectOfType<MonsterManager>();
        An = GetComponent<Animator>();

        if (Application.loadedLevelName == "StepCounter")
        {
            pb = FindObjectOfType<ParallaxBackground>();
        }

        if (Application.loadedLevelName == "Battle")
        {
            BS = FindObjectOfType<BattleScript>();
        }

    }

    void Start()
    {

        if (PlayerPrefs.HasKey("isChosen"))
        {
            SG = true;
            
        }

        sprite = gameObject;

       
    }

    // Update is called once per frame
    void Update()
    {
        
        #region GameScreen 
        //buat sebelum pilih monster
        if (MM.PSG)
        {
            MM.PSG = false;
            sprite.GetComponent<SpriteRenderer>().sprite = OM.monster.image;
            An.runtimeAnimatorController = OM.monster.spriteAnimation;
            gameObject.transform.localPosition = new Vector3(0, -12.6f, 0);
            if (OM.monster.m_num == 7)
            {
                gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0);
            }
            else if (OM.monster.m_num == 8 || OM.monster.m_num == 9)
            {
                gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0);

            }


            if (SpriteInStatus == true)
            {
                if (OM.monster.m_num == 7 || OM.monster.m_num == 8 || OM.monster.m_num == 9)
                {
                    gameObject.transform.localScale = new Vector3(1.05f, 1.05f, 0);

                    gameObject.transform.localPosition = new Vector3(-1.6f, 30.9f, 0);
                }
            }

            OM.NickName = PlayerPrefs.GetString("monName");
            OM.Level = 1; PlayerPrefs.SetInt("monLvl", 1);
            OM.Element = MM.allMonster[OM.monster.m_num - 4].Element;
            OM.hp = OM.monster.m_hp; PlayerPrefs.SetInt("monHP", OM.monster.m_hp);
            OM.atk = OM.monster.m_atk; PlayerPrefs.SetInt("monAtk", OM.monster.m_atk);
            OM.spd = OM.monster.m_spd; PlayerPrefs.SetInt("monSpd", OM.monster.m_spd);
            PlayerPrefs.SetInt("isChosen", 0);
        }

        //abis pilih monster
        if (SG)
        {
            SG = false;
            OM.monster = MM.allMonster[PlayerPrefs.GetInt("PickMon")];
            sprite.GetComponent<SpriteRenderer>().sprite = OM.monster.image;
            An.runtimeAnimatorController = OM.monster.spriteAnimation;
            gameObject.transform.localPosition = new Vector3(0, -15f, 0);

            //in game screen
            if (OM.monster.m_num == 7)
            {
                gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0);
            }
            else if (OM.monster.m_num == 8 || OM.monster.m_num == 9)
            {
                gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0);

            }



            //status

            if (SpriteInStatus == true)
            {
                if (OM.monster.m_num == 7 || OM.monster.m_num == 8 || OM.monster.m_num == 9)
                {
                    gameObject.transform.localScale = new Vector3(1.05f, 1.05f, 0);
                    gameObject.transform.localPosition = new Vector3(-1.6f, 30.9f, 0);
                }
            }


            if (Application.loadedLevelName == "StepCounter")
            {
                
                if (OM.monster.m_num == 7)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    gameObject.transform.localPosition = new Vector3(0, -32f, 0);

                }
                else if (OM.monster.m_num == 8 || OM.monster.m_num == 9)
                {
                    gameObject.transform.localPosition = new Vector3(0, -28.3f, 0);
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                
            }

            if (Application.loadedLevelName == "Explore")
            {

                if (OM.monster.m_num == 7)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    gameObject.transform.localPosition = new Vector3(0, -30f, 0);

                }
                else if (OM.monster.m_num == 8)
                {
                    gameObject.transform.localPosition = new Vector3(0, -25.6f, 0);
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else if ( OM.monster.m_num == 9)
                {
                    gameObject.transform.localPosition = new Vector3(0, -26.5f, 0);
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }

            OM.Element = MM.allMonster[OM.monster.m_num - 4].Element;
            OM.NickName = PlayerPrefs.GetString("monName");
            OM.Level = PlayerPrefs.GetInt("monLvl");
            OM.hp = PlayerPrefs.GetInt("monHP");
            OM.atk = PlayerPrefs.GetInt("monAtk");
            OM.spd = PlayerPrefs.GetInt("monSpd");
        }
        #endregion

        #region Training + Explore
        if (Application.loadedLevelName == "StepCounter" || Application.loadedLevelName == "Explore")
        {
            idletime += Time.deltaTime;
        }


        if (Application.loadedLevelName == "StepCounter" || Application.loadedLevelName == "Explore")
        {
            if (idletime >= 3)
            {
                An.SetBool("IsWalking", false);
                pb.move = false;
            }
        }
        #endregion

        #region Battle
        
            if (Application.loadedLevelName == "Battle")
            {
            if (!BS.EnemyWin)
            {
                if (OM.monster.m_num == 7 || OM.monster.m_num == 8 || OM.monster.m_num == 9)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    gameObject.transform.localPosition = new Vector3(-12.4f, 12.9f, 0);

                }
            }   

            }
        

        #endregion

        //if (An.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        //{
        //    An.SetBool("IsAttacked", false);
        //}

        //if (An.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        //{
        //    An.SetBool("IsAttacking", false);
        //}



    }


}


[System.Serializable]
public class OwnedMonster {
    public string NickName;
    public Sprite Element;
    public BaseMonster monster;
    public int Level;
    public int hp;
    public int atk;
    public int spd;
}