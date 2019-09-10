using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    //enemy
    [System.Serializable]
    public class EnemyClass {
        public GameObject EnemyObject;
        public List<string> ManyName = new List<string>();
        public EnemyMonster EM;
        public Animator EnemyAnimator;
        public Text EnemyLevel, EnemyName;
        public int MonNum, ELvl, EAtk, ESPD, EHP;
        public string Element;
        public Sprite EnemyElement;
        public float blockPer;
        public GameObject BlockAnimation;

        public AnimationClip[] EnemyAnimation;
        public float AttackingTime, AttackedTime, DieTime, IdleTime;

    }
    public EnemyClass EC;

    //Player
    [System.Serializable]
    public class PlayerClass {
        public Text MonsterName, MonsterLevel;
        public int MonHP, MonAtk, MonSpd;
        public AnimationClip[] PlayerAnimation;
        public float AttackingTime, AttackedTime, DieTime, IdleTime;
        public Animator PlayerAnimator;
        public int SpdMonster;
        public float blockPer;
        public GameObject BlockAnimation, DS, HL, WW;
    }
    public PlayerClass PC;


    private MonsterManager MM;
    [SerializeField] private Monster M;
    public Slider PlayerHPBar, PlayerSPDBar, EnemyHPBar, EnemySPDBar, SkillBar;
    [SerializeField]private bool PlayerTurn = false, EnemyTurn = false,
        StartBattle = false, FinishBattle = false, hasSkill = false;

    public SpriteRenderer PlayerEle, EnemyEle;

    public bool PlayerWin = false, EnemyWin = false;

    //skillUI
    public Image SkillConfirm;
    public Button SkillButton;
    public SpriteRenderer SkillSprite;
    public Text SkillName, SkillPanelName;
    private AllSkill askill;
    private int skillPoint, tempNum, thirdSkillTurn = 0, tempSpdBoost = 0, skillNumber;
    public GameObject SkillPanel;
    private bool WhistlingWindActive = false, skillActivate;


    //Opening and Ending UI
    public GameObject OpeningUI, PrepareUI, EndingUI, WinGold,AdWarning;
    public Text PrepareUIText, EndingUIText, GoldText,GoldTextAd;
    public Animator OpUI;
    private int WinGoldAd, WinGoldAdFinal;
    [SerializeField]private int WinGoldResult, counter = 0;

    //UiHolder
    public CanvasGroup UIHolder;

    //Sound
    private AudioScript AS;

    private void Awake()
    {
        PC.DS.GetComponent<ParticleSystem>().Stop(true);
        PC.HL.GetComponent<ParticleSystem>().Stop(true);
        PC.WW.GetComponent<ParticleSystem>().Stop(true);
        AS = FindObjectOfType<AudioScript>();
        askill = FindObjectOfType<AllSkill>();
        MM = FindObjectOfType<MonsterManager>();
        PrepareForEnemy();
        PC.PlayerAnimator = M.An;
        Debug.Log(PlayerPrefs.GetInt("EquipSkillNumber"));
        skillPoint = 0;
        skillActivate = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        PC.MonHP = PlayerPrefs.GetInt("m_current_hp");
        PC.MonAtk = PlayerPrefs.GetInt("m_current_atk");
        PC.MonSpd = PlayerPrefs.GetInt("m_current_spd");
        PlayerHPBar.maxValue = PC.MonHP;
        EC.ELvl = EC.EM.EMS.Level;
        EnemyHPBar.maxValue = EC.EHP;
        PC.SpdMonster = PlayerPrefs.GetInt("m_current_spd");
        skillNumber = PlayerPrefs.GetInt("EquipSkillNumber", -1);
        if (skillNumber >= 0)
        {
            hasSkill = true;
        }


        StartCoroutine(StartTheBattle());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerEle.sprite = M.OM.Element;


        #region UIManage
        if (UIHolder.alpha == 0)
                {
                    UIHolder.blocksRaycasts = false;
                }
                else if(UIHolder.alpha == 1)
                {
                    UIHolder.blocksRaycasts = true;
                }
                #endregion
                
        #region Skill Verification
        if (skillActivate)
        {
            SkillConfirm.color = Color.red;
        }
        else
        {
            SkillConfirm.color = Color.white;
        }

        if (skillPoint != 5)
        {
            SkillButton.interactable = false;
        }
        else
        {
            SkillButton.interactable = true;
        }


        if (hasSkill)
        {
            tempNum = PlayerPrefs.GetInt("EquipSkillNumber");
            SkillSprite.sprite = askill.All_Skill[tempNum].SkillSprite;
            SkillName.text = SkillPanelName.text = askill.All_Skill[tempNum].SkillName;
            SkillBar.value = skillPoint;
        }
        else
        {
            SkillButton.interactable = false;
        }

        #endregion

        #region Enemy Info
        EC.EnemyLevel.text = "Lvl: " + EC.ELvl;
        EC.EnemyName.text = EC.EM.EMS.NickName;
        EnemyHPBar.value = EC.EHP;
        EC.EnemyAnimation = EC.EnemyAnimator.runtimeAnimatorController.animationClips;
        EnemyAnimationTime();
        #endregion

        #region Player Info
        PC.MonsterName.text = M.OM.NickName;
        PC.MonsterLevel.text = "Lvl: " + M.OM.Level;
        PlayerHPBar.value = PC.MonHP;
        PC.PlayerAnimation = PC.PlayerAnimator.runtimeAnimatorController.animationClips;
        PlayerAnimationTime();
        #endregion

        #region EnemyAllocation
        if (!PlayerWin)
        {
            if (EC.EM.EMS.Num == 7 || EC.EM.EMS.Num == 8 || EC.EM.EMS.Num == 9) //LEAFY, FLAREZARD, SLIME
            {
                EC.EnemyObject.GetComponent<Transform>().localPosition = new Vector3(13.6f, 12.8f, 0);
                EC.EnemyObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (EC.EM.EMS.Num == 11 || EC.EM.EMS.Num == 12) //EARTH GOLEM + WATER GOLEM
            {
                EC.EnemyObject.GetComponent<Transform>().localPosition = new Vector3(13.6f, 7.5f, 0);
            }
            else if (EC.EM.EMS.Num == 10) //MAGMA GOLEM
            {
                EC.EnemyObject.GetComponent<Transform>().localPosition = new Vector3(13.6f, 7.5f, 0);
                EC.EnemyObject.GetComponent<SpriteRenderer>().flipX = true;

            }

        }

        #endregion

        if (StartBattle)
        {

            if (PlayerSPDBar.value < 301 && !EnemyTurn && !PlayerTurn && !FinishBattle)
            {
                PlayerSPDBar.value += PC.SpdMonster;
            }

            if (PlayerSPDBar.value == 300)
            {
                PlayerTurn = true;
                PlayerSPDBar.value = 0;
                Attack();
            }

            if (EnemySPDBar.value < 301 && !PlayerTurn && !EnemyTurn && !FinishBattle)
            {
                EnemySPDBar.value += EC.EM.EMS.spd;
            }

            if (EnemySPDBar.value == 300) {
                EnemyTurn = true;
                EnemySPDBar.value = 0;
                Attack();
            }

        }

        #region Battle End

        if (PC.MonHP <= 0)
        {
            int counter = 0;
            counter++;
            if (counter == 1)
            {
                M.An.SetBool("IsDead", true);
                FinishBattle = true;
                EnemyWin = true;
            }
        }

        if (EC.EHP <= 0)
        {

            int counter = 0;
            counter++;
            if (counter == 1)
            {
                EC.EnemyAnimator.SetBool("IsDead", true);
                FinishBattle = true;
                PlayerWin = true;
            }
        }

        if (FinishBattle)
        {
            StartBattle = false;
            if (PlayerWin)
            {
                EndingUIText.text = "You Win!";
                StartCoroutine(ShowAdWarning());
                float multiplier = ((float)M.OM.Level + (float)EC.ELvl) / 10;
                float multi2 = multiplier * 2000;
                WinGoldResult = (int)multi2;
                WinGold.SetActive(true);
                GoldText.text = WinGoldResult.ToString();
                counter++;
                if (counter <= 1)
                {
                    WinGoldAdFinal = WinGoldResult;
                    //GoldManager.instance.AddGold(WinGoldResult);
                    BattleRecord.instance.AddWin(1);
                }

            }

            IEnumerator ShowAdWarning() {
                yield return new WaitForSeconds(1.5f);
                AdWarning.SetActive(true);
            }

            if (EnemyWin)
            {
                counter++;
                if (counter <= 1)
                {
                    EndingUIText.text = "You Lose!";
                    BattleRecord.instance.AddLose(1);

                }
            }
            StartCoroutine(BattleEnded());
        }

        #endregion

    }

    public void BattleVideoAd()
    {
        
        WinGoldAd = Mathf.RoundToInt(WinGoldAdFinal * 1.5f);
        
       GoldTextAd.text = (WinGoldAd).ToString();

        GoldManager.instance.AddGold(WinGoldAd);
    }

    void PrepareForEnemy() {
        int RM = Random.Range(0, MM.allMonster.Count);
        EC.EM.EMS.monster = MM.allMonster[RM];
        EC.EM.EMS.Num = MM.allMonster[RM].m_num;
        EC.EnemyAnimator.runtimeAnimatorController = MM.allMonster[RM].spriteAnimation;
        EC.MonNum = MM.allMonster[RM].m_num;
        int RN = Random.Range(0, EC.ManyName.Count);
        EC.EM.EMS.NickName = EC.ManyName[RN];
        int RL = Random.Range(PlayerPrefs.GetInt("monLvl") - 2, PlayerPrefs.GetInt("monLvl") + 2);
        if (RL <= 0)
        {
            RL = 1;
        }
        EC.EM.EMS.Level = RL;
        EC.Element = MM.allMonster[RM].m_type.ToString();
        EnemyEle.sprite = MM.allMonster[RM].Element;

        #region EnemyStatusDistributor
        int EnemyPoint = RL - 1;

        int EPHP = MM.allMonster[RM].m_hp, EPATK = MM.allMonster[RM].m_atk, EPSPD = MM.allMonster[RM].m_spd, tempHPPoint = 0;

        for (int i = 0; i < EnemyPoint; i++)
        {
            int RandomDistribute = Random.Range(1, 3);

            switch (RandomDistribute) {
                case 1:
                    tempHPPoint += 1;
                    ; break;
                case 2:
                    EPATK += 1;
                    ; break;
                case 3:
                    EPSPD += 1;
                    ; break;
            }

        }

        int AdditionalHP = (tempHPPoint * 5) + EPHP;

        EC.EM.EMS.hp = AdditionalHP;
        EC.EM.EMS.atk = EPATK;
        EC.EM.EMS.spd = EPSPD;

        #endregion

        EC.EHP = EC.EM.EMS.hp;
        EC.EAtk = EPATK;
        EC.ESPD = EPSPD;
    }


    void Attack() {
        if (PlayerTurn)
        {
            if (skillPoint >= 5 && skillActivate)
            {
                SkillPanel.SetActive(true);
                ActivateSkill();
            }
            else
            {
                StartCoroutine(PlayerAttackingAnimation(PC.MonAtk));
                HitSound();
            }

            if (WhistlingWindActive)
            {
                thirdSkillTurn++;
                if (thirdSkillTurn >= 4)
                {
                    WhistlingWindActive = false;
                    PC.SpdMonster -= tempSpdBoost;
                    tempSpdBoost = 0;
                }
            }

        }

        if (EnemyTurn)
        {
            StartCoroutine(EnemyAttackingAnimation(EC.EAtk));
            HitSound();
        }

    }

    int DamageDeal(int Str) {

        int playerStr = Str;
        int enemyStr = Str;

        if (M.OM.monster.m_type.ToString() == "Fire" && EC.Element == "Water")
        {
            playerStr -= Mathf.RoundToInt(Str * 0.25f);
            enemyStr += Mathf.RoundToInt(Str * 0.25f);
        }
        else if (M.OM.monster.m_type.ToString() == "Fire" && EC.Element == "Grass")
        {
            playerStr += Mathf.RoundToInt(Str * 0.25f);
            enemyStr -= Mathf.RoundToInt(Str * 0.25f);
        }

        if (M.OM.monster.m_type.ToString() == "Water" && EC.Element == "Grass")
        {
            playerStr -= Mathf.RoundToInt(Str * 0.25f);
            enemyStr += Mathf.RoundToInt(Str * 0.25f);
        }
        else if (M.OM.monster.m_type.ToString() == "Water" && EC.Element == "Fire")
        {
            playerStr += Mathf.RoundToInt(Str * 0.25f);
            enemyStr -= Mathf.RoundToInt(Str * 0.25f);
        }

        if (M.OM.monster.m_type.ToString() == "Grass" && EC.Element == "Water")
        {
            playerStr -= Mathf.RoundToInt(Str * 0.25f);
            enemyStr += Mathf.RoundToInt(Str * 0.25f);
        }
        else if (M.OM.monster.m_type.ToString() == "Grass" && EC.Element == "Fire")
        {
            playerStr += Mathf.RoundToInt(Str * 0.25f);
            enemyStr -= Mathf.RoundToInt(Str * 0.25f);
        }

        int damage = 0;

        if (PlayerTurn)
        {
            Debug.Log("PlayerDMG: " + playerStr);
            damage = playerStr;
        }
        else if (EnemyTurn)
        {
            Debug.Log("EnemyDMG: " + enemyStr);
            damage = enemyStr;
        }

        return damage;
    }   
    
    #region animation
    IEnumerator PlayerAttackingAnimation(int damage) {
        M.An.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(PC.AttackingTime);
        M.An.SetBool("IsAttacking", false);
        SkillPanel.SetActive(false);
        StartCoroutine(EnemyGetAttacked(DamageDeal(damage)));
        }

    IEnumerator EnemyGetAttacked(int damage) {
        EC.EnemyAnimator.SetBool("IsAttacked", true);

        if (PC.MonAtk < EC.EAtk)
        {
            StartCoroutine(EnemyBlockHide());
            float atkCount = EC.EAtk - PC.MonAtk;
            EC.blockPer = (((atkCount)/PC.MonAtk) * 100);
            Debug.Log(EC.blockPer);

            if (EC.blockPer >= 50f)
            {
                EC.blockPer = 50f;
            }
            float totalDMG = damage - (damage * (EC.blockPer/100));
            EC.EHP -= Mathf.RoundToInt(totalDMG);
            Debug.Log(EC.blockPer);
            Debug.Log(Mathf.RoundToInt(totalDMG));
        }
        else
        {
            EC.EHP -= damage;
        }
        yield return new WaitForSeconds(EC.AttackedTime);
        EC.EnemyAnimator.SetBool("IsAttacked", false);
        
        if (skillPoint != 5)
        {
            skillPoint++;
        }
        PlayerTurn = false;
    }

    IEnumerator EnemyBlockHide() {
        EC.BlockAnimation.SetActive(true);
        yield return new WaitForSeconds(1f);
        EC.BlockAnimation.SetActive(false);
    }

    IEnumerator EnemyAttackingAnimation(int damage) {
        EC.EnemyAnimator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(EC.AttackingTime);
        EC.EnemyAnimator.SetBool("IsAttacking", false);
        StartCoroutine(PlayerGetAttacked(DamageDeal(damage)));
    }

    IEnumerator PlayerGetAttacked(int damage) {
        M.An.SetBool("IsAttacked", true);

        if (EC.EAtk < PC.MonAtk)
        {
            StartCoroutine(PlayerBlockHide());
            float atkCount = PC.MonAtk - EC.EAtk;
            PC.blockPer = (((atkCount) / EC.EAtk) * 100);
            Debug.Log(PC.blockPer);
            if (PC.blockPer >= 50f)
            {
                PC.blockPer = 50f;
            }
            float totalDMG = damage - (damage * (PC.blockPer / 100));
            PC.MonHP -= Mathf.RoundToInt(totalDMG);
            Debug.Log(PC.blockPer);
            Debug.Log(Mathf.RoundToInt(totalDMG));
        }
        else
        {
            PC.MonHP -= damage;
        }
        yield return new WaitForSeconds(PC.AttackedTime);
        M.An.SetBool("IsAttacked", false);
        EnemyTurn = false;
    }

    IEnumerator PlayerBlockHide()
    {
        PC.BlockAnimation.SetActive(true);
        yield return new WaitForSeconds(1f);
        PC.BlockAnimation.SetActive(false);
    }

    IEnumerator FinishBattleUI(float time) {
        yield return new WaitForSeconds(time);
        EndingUI.SetActive(true);
    }

    IEnumerator StartTheBattle() {
        yield return new WaitForSeconds(1f);
        PrepareUIText.text = "Fight!";
        OpUI.SetBool("BattleStart", true);
        StartBattle = true;
    }

    IEnumerator BattleEnded() {
        if (PlayerWin)
        {
            yield return new WaitForSeconds(EC.AttackedTime);
        }

        if (EnemyWin)
        {
            yield return new WaitForSeconds(PC.AttackedTime);
        }

        PrepareUI.SetActive(false);
        EndingUI.SetActive(true);
        OpUI.SetBool("BattleStart", false);
    }

    IEnumerator SkillActivate() {
        M.An.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(PC.AttackingTime);
        skillPoint = 0;
        PlayerTurn = false;
        M.An.SetBool("IsAttacking", false);
    }

    #endregion

    #region AnimationTime
    void PlayerAnimationTime() {
        foreach (AnimationClip clip in PC.PlayerAnimation)
        {
            switch (clip.name)
            {
                case "Attacking":
                    PC.AttackingTime = clip.length;
                    break;
                case "Attacked":
                    PC.AttackedTime = clip.length;
                    break;
                case "Die":
                    PC.DieTime = clip.length;
                    break;
                case "Idle":
                    PC.IdleTime = clip.length;
                    break;
            }
        }
    }

    void EnemyAnimationTime() {
        foreach (AnimationClip clip in EC.EnemyAnimation)
        {
            switch (clip.name)
            {
                case "Attacking":
                    EC.AttackingTime = clip.length;
                    break;
                case "Attacked":
                    EC.AttackedTime = clip.length;
                    break;
                case "Die":
                    EC.DieTime = clip.length;
                    break;
                case "Idle":
                    EC.IdleTime = clip.length;
                    break;
            }
        }
    }
    #endregion

    #region Skill

    void ActivateSkill() {
        switch (tempNum)
        {
            case 0:
                DeadlyAttack();
                break;
            case 1: HealingLight();
                break;
            case 2: WhistlingWind();
                break;
        }
    }

    void DeadlyAttack() {
        SkillSound();
        PC.DS.GetComponent<ParticleSystem>().Play(true);
        SkillName.text = "Deadly Strike";
        SkillName.color = Color.red;
        
        int damageMultiply = PC.MonAtk * 2;
        skillPoint = 0;
        StartCoroutine(PlayerAttackingAnimation(damageMultiply));
        skillActivate = false;
    }

    void HealingLight() {
        SkillSound();
        PC.HL.GetComponent<ParticleSystem>().Play(true);
        SkillName.text = "Healing LIght";
        SkillName.color = Color.yellow;
        StartCoroutine(SkillActivate());
        PC.MonHP += (int)(PlayerPrefs.GetInt("m_current_hp") * 0.25);
        if (PC.MonHP >= PlayerPrefs.GetInt("m_current_hp"))
        {
            PC.MonHP = PlayerPrefs.GetInt("m_current_hp");
        }
        skillActivate = false;

    }

    void WhistlingWind() {
        SkillSound();
        PC.WW.GetComponent<ParticleSystem>().Play(true);
        SkillName.text = "Whistling Wind";
        SkillName.color = Color.green;
        StartCoroutine(SkillActivate());
        WhistlingWindActive = true;
        tempSpdBoost = (int)(PC.SpdMonster * 0.3);
        PC.SpdMonster += tempSpdBoost;
        thirdSkillTurn = 0;
        skillActivate = false;

    }

  

    #endregion

    #region skillButon

    public void SkillButtonClick() {
        if (!skillActivate)
        {
            skillActivate = true;
        }
        else
        {
            skillActivate = false;
        }
        

    }

    #endregion


    public void HitSound()
    {
        AS.SFXSource.clip = AS.HitSound;
        AS.SFXSource.Play();
    }
    public void SkillSound()
    {
        AS.SFXSource.clip = AS.UseSkillSound;
        AS.SFXSource.Play();
    }


    public void ReturnToGameScreenWithXp() {
        GoldManager.instance.AddGold(WinGoldResult);
        Application.LoadLevel("GameScreen");
    }
    public void ReturnToGameScreen()
    {
       
        Application.LoadLevel("GameScreen");
    }

}
