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

        if (ZPlayerPrefs.HasKey("isChosen"))
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
            if (OM.monster.m_num == 4)
            {
                gameObject.transform.localPosition = new Vector3(0, -23.8f, 0);
            }
            else if (OM.monster.m_num == 5)
            {
                gameObject.transform.localPosition = new Vector3(0, -15f, 0);
                //gameObject.GetComponent<SpriteRenderer>().flipX = true;

            }


            if (SpriteInStatus == true)
            {
                if (OM.monster.m_num == 4)
                {
                    gameObject.transform.localPosition = new Vector3(-1.5f, 25.9f, 0);
                }
                else if (OM.monster.m_num == 5)
                {
                    gameObject.transform.localPosition = new Vector3(-3.5f, 32.2f, 0);
                    gameObject.transform.localScale = new Vector3(7, 7, 1);
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
            }

            OM.NickName = ZPlayerPrefs.GetString("monName");
            OM.Level = 1; ZPlayerPrefs.SetInt("monLvl", 1);
            OM.Element = MM.allMonster[OM.monster.m_num - 4].Element;
            OM.hp = OM.monster.m_hp; ZPlayerPrefs.SetInt("monHP", OM.monster.m_hp);
            OM.atk = OM.monster.m_atk; ZPlayerPrefs.SetInt("monAtk", OM.monster.m_atk);
            OM.spd = OM.monster.m_spd; ZPlayerPrefs.SetInt("monSpd", OM.monster.m_spd);
            ZPlayerPrefs.SetInt("isChosen", 0);
        }

        //abis pilih monster
        if (SG)
        {
            SG = false;
            OM.monster = MM.allMonster[ZPlayerPrefs.GetInt("PickMon")];
            sprite.GetComponent<SpriteRenderer>().sprite = OM.monster.image;
            An.runtimeAnimatorController = OM.monster.spriteAnimation;
            //in game screen
            if (OM.monster.m_num == 4)
            {
                gameObject.transform.localPosition = new Vector3(0, -23.8f, 0);

            }
            else if (OM.monster.m_num == 5)
            {
                gameObject.transform.localPosition = new Vector3(0, -15f, 0);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (OM.monster.m_num == 6)
            {
                gameObject.transform.localPosition = new Vector3(0, -20f, 0);
            }

            //status

            if (SpriteInStatus == true)
            {
                if (OM.monster.m_num == 4)
                {
                    gameObject.transform.localPosition = new Vector3(-1.5f, 25.9f, 0);
                }
                else if (OM.monster.m_num == 5)
                {
                    gameObject.transform.localPosition = new Vector3(-3.5f, 32.2f, 0);
                    gameObject.transform.localScale = new Vector3(7, 7, 1);
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
            }



            if (Application.loadedLevelName == "StepCounter")
            {
                
                if (OM.monster.m_num == 4)
                {
                    gameObject.transform.localPosition = new Vector3(0, -37.8f, 0);

                }
                else if (OM.monster.m_num == 5)
                {
                    gameObject.transform.localPosition = new Vector3(0, -17.4f, 0);
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (OM.monster.m_num == 6)
                {
                    gameObject.transform.localPosition = new Vector3(0, 0f, 0);
                }
                
            }

            if (Application.loadedLevelName == "Explore")
            {

                if (OM.monster.m_num == 4)
                {
                    gameObject.transform.localPosition = new Vector3(0, -37.8f, 0);

                }
                else if (OM.monster.m_num == 5)
                {
                    gameObject.transform.localPosition = new Vector3(0, -30f, 0);
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (OM.monster.m_num == 6)
                {
                    gameObject.transform.localPosition = new Vector3(0, -35f, 0);
                }

            }

            OM.Element = MM.allMonster[OM.monster.m_num - 4].Element;
            OM.NickName = ZPlayerPrefs.GetString("monName");
            OM.Level = ZPlayerPrefs.GetInt("monLvl");
            OM.hp = ZPlayerPrefs.GetInt("monHP");
            OM.atk = ZPlayerPrefs.GetInt("monAtk");
            OM.spd = ZPlayerPrefs.GetInt("monSpd");
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
                if (OM.monster.m_num == 4)
                {
                    gameObject.transform.localPosition = new Vector3(-13.7f, 0, 0);

                }
                else if (OM.monster.m_num == 5)
                {
                    gameObject.transform.localPosition = new Vector3(-10.9f, 10.2f, 0);
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (OM.monster.m_num == 6)
                {
                    // gameObject.transform.localPosition = new Vector3(0, -35f, 0);
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