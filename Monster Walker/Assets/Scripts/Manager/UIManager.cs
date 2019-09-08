using PedometerU.Tests;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //opening
    public GameObject Credit, GPSWarning;

    //Start of the Game
    public GameObject PreGame, chooseMon, inputName, mainCanvas;
    public InputField chooseName;
    private MonsterManager MM;
    private Monster M;

    //textUI
    public Text nickname, monLevel, monExp, playGold;

    [SerializeField]private AudioScript AS;
    [SerializeField]private AudioScript SM;

    //setting
    public Slider MusicSlider, SFXSlider;
    public GameObject SettingMenu;
    public Text MusicPercentage, SoundPercentage;

    //UIChooseMOnster
    public Transform ChoseMonSpriteSize;
    public SpriteRenderer ChoseMonSprite;
    public TextMeshProUGUI MonQuotes;
    public Text ChoseMonName, ChoseMonBio, ChoseMonHP, ChoseMonATK, ChoseMonSPD;
    private int tempchose;
    public int equipHp, equipStr, equipSpd;
    public GameObject ChooseWarningText;
    //UIFAQ
    public GameObject w1;

    //UITrainingResult
    public GameObject AruSureTraining;
    private ParallaxBackground pb;
    public GameObject TR;
    public Text CurStep, BesStep, TotStep,AdStepText, multiplier;
    
    
    private int totSteptemp,AdStep,AdstepAddition;


    private StepCounter sc;

    //Tutorial Menu
    private TutorialManager TM;
    private int T_Step;

    //ItemIndication
    [SerializeField]private Text LowPot, MedPot, HighPot, BattleTicket;

    [System.Serializable]
    public class StatusUI {
        public GameObject StatsUI, baseUI, LevelUI,
            PlusMinusBtn, explackwarning, WeaponUI, 
            ArmorUI, AccUI, SkillUI, StatisticUI, ShopUI, 
            ShopWeapon, ShopArmor, ShopAcc, ShopPotion, PointNotif,
            BattleBtnWarning;
        public Scrollbar WeaponScroll, ArmorScroll, AccScroll, PotionScroll;
        public bool StatusShown, LvUpClick, ShopShown;
        public Text StatHP, StatSTR, StatSPD,
            LevelHP, LevelSTR, LevelSPD, 
            totStep, besStep, StatPoint, 
            expCost,
            LvlHP, LvlSTR, LvlSPD,
            battleEncounter, deathInExplore,
            battleWin, battleLose;
        public int tempHP, tempSTR, tempSPD, 
            pointUsedTotal, pointUsedHP, pointUsedSTR, pointUsedSPD;
        public SpriteRenderer CurWea, CurArm, CurAcc, CurSkill, CurrElement;
        public Scrollbar WS, AS, ACCS, SkillScroll;
        public int e_w_c, e_a_c, e_acc_c, s_c, NOWHP, NOWSPD, NOWSTR;
    }
    public StatusUI SUI = new StatusUI();

    private int tempPoint;
    //Application.levelCount == 1 -> Game Screen

    //OpeningMenu
    private int IntroAlreadyHappen;

    private void Awake()
    {
        MM = FindObjectOfType<MonsterManager>();
        M = FindObjectOfType<Monster>();
        SUI.StatusShown = false;
        SUI.tempHP = SUI.tempSTR = SUI.tempSPD = 
            SUI.pointUsedTotal = SUI.pointUsedHP = SUI.pointUsedSTR = SUI.pointUsedSPD = 0;

        AS = FindObjectOfType<AudioScript>();
        TM = FindObjectOfType<TutorialManager>();
        

        if (Application.loadedLevelName == "OpeningMenu")
        {
            IntroAlreadyHappen = PlayerPrefs.GetInt("Intro", 0);
            PlayOP();
        }
        else if (Application.loadedLevelName == "GameScreen")
        {
            PlayGS();
        }

        if (Application.loadedLevelName == "GameScreen")
        {
            tempPoint = ExpManager.instance.POINT;

            if (!PlayerPrefs.HasKey("isChosen"))
            {
                mainCanvas.SetActive(false);
                PreGame.SetActive(true);
            }
            else
            {
                PreGame.SetActive(false);
            }
            
        }

        if(Application.loadedLevelName == "StepCounter")
        {
            pb = FindObjectOfType<ParallaxBackground>();
            sc = FindObjectOfType<StepCounter>();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        AS.MusicSource.volume = PlayerPrefs.GetFloat("MusicSetting", 1f);
        AS.SFXSource.volume = PlayerPrefs.GetFloat("SoundSetting", 1f);
        if (Application.loadedLevelName == "GameScreen")
        {
            MusicPercentage.text = ((PlayerPrefs.GetFloat("MusicSetting") * 100)).ToString("#") + "%";
            SoundPercentage.text = ((PlayerPrefs.GetFloat("SoundSetting") * 100)).ToString("#") + "%";
            MusicSlider.value = PlayerPrefs.GetFloat("MusicSetting", 1f);
            SFXSlider.value = PlayerPrefs.GetFloat("SoundSetting", 1f);
        }

    }

    // Update is called once per frame
    void Update()
    {

        //if (Application.loadedLevelName == "OpeningMenu")
        //{
        //    if (Input.location.isEnabledByUser == false)
        //    {
        //        GPSWarning.SetActive(true);
        //    }
        //    else
        //    {
        //        GPSWarning.SetActive(false);
        //    }
        //}

        if (Application.loadedLevelName == "GameScreen")
        {
            #region Monster + Status Update
            monExp.text = ExpManager.instance.EXP.ToString();
            playGold.text = GoldManager.instance.GOLD.ToString();
            nickname.text = M.OM.NickName;
            monLevel.text = M.OM.Level.ToString();
            SUI.NOWHP = PlayerPrefs.GetInt("monHP") + equipHp;
            SUI.NOWSPD = PlayerPrefs.GetInt("monSpd") + equipSpd;
            SUI.NOWSTR = PlayerPrefs.GetInt("monAtk") + equipStr;

            SUI.CurrElement.sprite = M.OM.Element;

            LowPot.text = "x" + PotionManager.instance.LOWPOTION;
            MedPot.text = "x" + PotionManager.instance.MEDIUMPOTION;
            HighPot.text = "x" + PotionManager.instance.HIGHPOTION;
            BattleTicket.text = "x" + TicketManager.instance.TICKET;

            SUI.totStep.text = StatisticManager.instance.STEP.ToString();
            SUI.besStep.text = StatisticManager.instance.TOPSTEP.ToString();
            SUI.battleEncounter.text = StatisticManager.instance.BATTLECOUNTER.ToString();
            SUI.deathInExplore.text = StatisticManager.instance.DEATHEXPLORE.ToString();

            SUI.battleWin.text = BattleRecord.instance.WIN.ToString();
            SUI.battleLose.text = BattleRecord.instance.LOSE.ToString();
            
            if (ExpManager.instance.POINT <= 0)
            {
                SUI.PointNotif.SetActive(false);
            }
            else if (ExpManager.instance.POINT > 0)
            {
                SUI.PointNotif.SetActive(true);
            }

            if (SUI.StatsUI.activeInHierarchy == true)
            {
                StatusMenu();
                if (SUI.LvUpClick == true)
                {
                    SUI.LvUpClick = false;
                    ResetStatUpdate();
                }

            }
            #endregion

            #region AudioSetting
            MusicPercentage.text = (MusicSlider.value * 100).ToString("#") + "%";

            if (MusicSlider.value == 0)
            {
                MusicPercentage.text = "0%";
            }

            SoundPercentage.text = (SFXSlider.value * 100).ToString("#") + "%";

            if (SFXSlider.value == 0)
            {
                SoundPercentage.text = "0%";
            }

            AS.MusicSource.volume = (float)MusicSlider.value;
            AS.SFXSource.volume = (float)SFXSlider.value;

            if (MusicSlider.value >= MusicSlider.value || MusicSlider.value <= MusicSlider.value)
            {
                PlayerPrefs.SetFloat("MusicSetting", MusicSlider.value);
            }

            if (SFXSlider.value >= SFXSlider.value || SFXSlider.value <= SFXSlider.value)
            {
                PlayerPrefs.SetFloat("SoundSetting", SFXSlider.value);
            }

            #endregion

        }
    }
    
    #region Choose Starter Button
    public void MonA() {
        M.OM.monster = MM.allMonster[0];
        PlayerPrefs.SetInt("PickMon", 0);
        tempchose = 0;
        ChooseName();
    }

    public void MonB()
    {
        M.OM.monster = MM.allMonster[1];
        PlayerPrefs.SetInt("PickMon", 1);
        tempchose = 1;
        ChooseName();
    }

    public void MonC()
    {
        M.OM.monster = MM.allMonster[2];
        PlayerPrefs.SetInt("PickMon", 2);
        tempchose = 2;
        ChooseName();
    }

    void ChooseName() {

        if (tempchose == 0)
        {
            ChoseMonSpriteSize.localPosition = new Vector3(0f, -1100f, 0);
            ChoseMonSpriteSize.localScale = new Vector3(30f, 30f, 0);
            //232.8336 251.4603
            ChoseMonSprite.sprite = MM.allMonster[0].image; ChoseMonSprite.flipX = false;
            ChoseMonName.text = MM.allMonster[0].m_name;
            ChoseMonBio.text = "Some says that slime is the weakest monster in the world. " +
                "Well, this one strongly disagrees! " +
                "The essences of the water are its source of power and keep its metabolism at the top. " +
                "It has the perfect balance that no monsters ever hope to achieve. "; //textbiomonste slime
            MonQuotes.color = Color.blue;
            MonQuotes.text = "“Thirsty? Worry not, I have a lot of water for you. Break those sweats for me…... please…. ”";
            ChoseMonHP.text = MM.allMonster[0].m_hp.ToString();
            ChoseMonATK.text = MM.allMonster[0].m_atk.ToString();
            ChoseMonSPD.text = MM.allMonster[0].m_spd.ToString();
        }else if (tempchose == 1)
        {
            ChoseMonSpriteSize.localPosition = new Vector3(0f, -1050f,0);
            ChoseMonSpriteSize.localScale = new Vector3(40f, 35f, 0);
            //122.544 132.3475
            ChoseMonSprite.sprite = MM.allMonster[1].image;
            ChoseMonSprite.flipX = false;
            ChoseMonName.text = MM.allMonster[1].m_name;
            ChoseMonBio.text = "It is no ordinary monster. " +
                "Flarezard belongs as one of the oldest monster families in the forest." +
                "Strength provided by the essences of fire, " +
                "Flarezard is ready to tag along and burn those calories with you with its mighty strength. "; //textbiomonste flarezard
            MonQuotes.color = Color.red;
            MonQuotes.text = "“Turn it up! Burn it Up! Burn those fat if you want to have a sexy body like me!”";
            ChoseMonHP.text = MM.allMonster[1].m_hp.ToString();
            ChoseMonATK.text = MM.allMonster[1].m_atk.ToString();
            ChoseMonSPD.text = MM.allMonster[1].m_spd.ToString();
        }else if (tempchose == 2)
        {
            ChoseMonSpriteSize.localPosition = new Vector3(0f, -1050f,0);
            ChoseMonSpriteSize.localScale = new Vector3(30f, 30f, 0);
            //256.117 276.6064
            ChoseMonSprite.flipX = false;
            ChoseMonSprite.sprite = MM.allMonster[2].image;
            ChoseMonName.text = MM.allMonster[2].m_name;
            ChoseMonBio.text = "Born in the forest, Leafy is the spawn of the forest guardian. " +
                "It likes to jump around and has fun with its kin. " +
                "Through the essences of the grass, it has the hardness of steel and the speed of thunder, " +
                "or so it thought. Leafy have prepared itself to grow alongside you."; //textbiomonste leafy
            MonQuotes.color = Color.green;
            MonQuotes.text = "“You think I’m chubby and smelly?! How about try looking at yourself and smell yourself! Yeah, I know, now run! ”";
            ChoseMonHP.text = MM.allMonster[2].m_hp.ToString();
            ChoseMonATK.text = MM.allMonster[2].m_atk.ToString();
            ChoseMonSPD.text = MM.allMonster[2].m_spd.ToString();
        }

        chooseMon.SetActive(false);
        inputName.SetActive(true);
    }

    public void CancelPick() {
        PlayerPrefs.DeleteKey("PickMon");
        chooseName.text = "";
        chooseMon.SetActive(true);
        inputName.SetActive(false);
    }

    public void ClosePreGame() {
        if (chooseName.text.Length >= 3 && chooseName.text.Length <= 8)
        {
            PlayerPrefs.SetString("monName", chooseName.text);
            PlayerPrefs.SetInt("Intro", 1);
            mainCanvas.SetActive(true);
            MM.PSG = true;
            TM.Tutorial.SetActive(true);
            PreGame.SetActive(false);
        }
        else
        {
            StartCoroutine(WarningText());
        }
    }

    IEnumerator WarningText() {
        ChooseWarningText.SetActive(true);
        yield return new WaitForSeconds(1f);
        ChooseWarningText.SetActive(false);
    }

    #endregion

    #region Training UI

    public void backToGame() {
        Application.LoadLevel("GameScreen");
    }

    public void TrainingVideoAd()
    {
        AdStep = Mathf.RoundToInt(T_Step * 1.5f);
        //AdstepAddition  = AdStep + T_Step;
        AdStepText.text = (AdStep).ToString();
        ExpManager.instance.AddEXP(AdStep);
    }


    public void FinishTraining()
    {
        sc.pedometer.Dispose();
        sc.pedometer = null;
        M.An.SetBool("IsWalking", false);
        pb.move = false;
        TR.SetActive(true);
        StatisticManager.instance.AddStep(sc.step1);
        StatisticManager.instance.BestStep(sc.step1);
        multiplier.text = "x " + sc.multiplier.ToString("#.#");
        CurStep.text = sc.step1.ToString();
        BesStep.text = StatisticManager.instance.TOPSTEP.ToString();
            totSteptemp = Mathf.RoundToInt(sc.step1 * sc.multiplier);
            TotStep.text = (totSteptemp).ToString();
        T_Step = totSteptemp;
    }

    public void ReturnToGameScreenWithXP() {
        ExpManager.instance.AddEXP(T_Step);
        Application.LoadLevel("GameScreen");
    }


    public void OpeningAreUSure() {
        AruSureTraining.SetActive(true);
    }

    public void YesAreSure() {
        FinishTraining();
    }

    public void NoAreSure()
    {
        AruSureTraining.SetActive(false);
    }

    #endregion

    #region Warning

    public void warning1() {
        w1.SetActive(true);
    }

    public void c_warning1() {
        w1.SetActive(false);
    }

    #endregion

    #region FooterButton

    public void gameToTrain() {
        PlayTraining();
        Application.LoadLevel("StepCounter");
    }

    public void StatusClick() {
        if (SUI.StatusShown == false)
        {
            SUI.StatusShown = true;
            SUI.ShopShown = false;
            ShowStatus();
        }
        else
        {
            SUI.StatusShown = false;
            CloseStatus();
        }
    }

    void ShowStatus() {
        resetEquipBtn();
        SUI.ShopShown = false;
        CloseShop();
        SUI.StatsUI.SetActive(true);
    }

    void CloseStatus() {
        SUI.StatsUI.SetActive(false);
    }

    public void ShopClick()
    {
        if (SUI.ShopShown == false)
        {
            PlayShop();
            SUI.ShopShown = true;
            SUI.StatusShown = false;

            ClickShop();
            SUI.WeaponScroll.value = 1; SUI.WeaponScroll.value = 1;

        }
        else
        {
            SUI.ShopShown = false;
            CloseShop();
        }
    }


    public void ClickShop()
    {
        SUI.StatisticUI.SetActive(false);
        SUI.StatsUI.SetActive(false);
        SUI.ShopUI.SetActive(true);
        SUI.ShopWeapon.SetActive(true);
        SUI.ShopArmor.SetActive(false);
        SUI.ShopAcc.SetActive(false);
        SUI.ShopPotion.SetActive(false);
    }

    public void CloseShop()
    {
        ShopClosed();
        SUI.StatsUI.SetActive(false);
        SUI.ShopUI.SetActive(false);
        SUI.ShopWeapon.SetActive(false);
        SUI.ShopArmor.SetActive(false);
        SUI.ShopAcc.SetActive(false);
        SUI.ShopPotion.SetActive(false);
    }

    public void ToExplore() {
        PlayExplore();
        PlayerPrefs.SetInt("m_current_hp", SUI.NOWHP);
        PlayerPrefs.SetInt("m_current_spd", SUI.NOWSPD);
        PlayerPrefs.SetInt("m_current_atk", SUI.NOWSTR);
        Application.LoadLevel("Explore");
    }

    public void ToBattle() {
        if (TicketManager.instance.TICKET <= 0)
        {
            StartCoroutine(BattleWarningBtnShow());
        }
        else
        {
            PlayBattle();
            TicketManager.instance.RemoveTicket(1);
            PlayerPrefs.SetInt("m_current_hp", SUI.NOWHP);
            PlayerPrefs.SetInt("m_current_spd", SUI.NOWSPD);
            PlayerPrefs.SetInt("m_current_atk", SUI.NOWSTR);
            Application.LoadLevel("Battle");
        }

        ////for testing purposes
        //PlayBattle();
        //PlayerPrefs.SetInt("m_current_hp", SUI.NOWHP);
        //PlayerPrefs.SetInt("m_current_spd", SUI.NOWSPD);
        //PlayerPrefs.SetInt("m_current_atk", SUI.NOWSTR);
        //Application.LoadLevel("Battle");

    }

    IEnumerator BattleWarningBtnShow() {
        SUI.BattleBtnWarning.SetActive(true);
        yield return new WaitForSeconds(1f);
        SUI.BattleBtnWarning.SetActive(false);

    }

    #endregion

    #region Status

    void StatusMenu() {
        SUI.StatHP.text = (PlayerPrefs.GetInt("monHP") + equipHp).ToString();
        SUI.LevelHP.text = PlayerPrefs.GetInt("monHP").ToString();
        SUI.StatSTR.text = (PlayerPrefs.GetInt("monAtk") + equipStr).ToString();
        SUI.LevelSTR.text = PlayerPrefs.GetInt("monAtk").ToString();
        SUI.StatSPD.text = (PlayerPrefs.GetInt("monSpd") + equipSpd).ToString();
        SUI.LevelSPD.text = PlayerPrefs.GetInt("monSpd").ToString();
        SUI.totStep.text = StatisticManager.instance.STEP.ToString();
        SUI.besStep.text = StatisticManager.instance.TOPSTEP.ToString();
        SUI.StatPoint.text = tempPoint.ToString();
        int tempcost = ExpManager.instance.EXPCOST;
        SUI.expCost.text = tempcost + " exp";

        if (ExpManager.instance.POINT > 0)
        {
            SUI.PlusMinusBtn.SetActive(true);
        }
        else
        {
            SUI.PlusMinusBtn.SetActive(false);
        }

    }

    public void LevelUP() {
        if (ExpManager.instance.EXP < ExpManager.instance.EXPCOST)
        {
            SUI.explackwarning.SetActive(true);
        }
        else
        {
            LevelUpSound();
            SUI.LvUpClick = true;
            SUI.explackwarning.SetActive(false);
            ExpManager.instance.RemoveEXP(ExpManager.instance.EXPCOST);
            ExpManager.instance.MonLevelUp();
            M.SG = true;
            int tempLvl = PlayerPrefs.GetInt("monLvl") + 1;
            PlayerPrefs.SetInt("monLvl", tempLvl);
            ExpManager.instance.AddPoint(1);
        }
    }

    public void LevelUIOpen() {
        SUI.StatisticUI.SetActive(false);
        SUI.LevelUI.SetActive(true);
        SUI.baseUI.SetActive(false);
        resetEquipBtn();
    }

    public void LevelUIClose() {
        SUI.LevelUI.SetActive(false);
        SUI.baseUI.SetActive(true);
        resetEquipBtn();
    }

    public void StatisticOpen()
    {
        resetEquipBtn();
        SUI.StatisticUI.SetActive(true);
    }


    #region PlusMinusStats

    public void PlusHP() {
        if (tempPoint != 0)
        {
            tempPoint -= 1;
            SUI.pointUsedTotal += 1;
            SUI.pointUsedHP += 1;
            SUI.tempHP += 5;
            SUI.LvlHP.text = "[+" + SUI.tempHP + "] ";
        }
    }

    public void MinusHP() {
         if (SUI.pointUsedHP != 0)
        {
            tempPoint += 1;
            SUI.pointUsedTotal -= 1;
            SUI.pointUsedHP -= 1;
            SUI.tempHP -= 5;
            SUI.LvlHP.text = "[+" + SUI.tempHP + "] ";
        }
    }

    public void PlusSTR() {
        if (tempPoint != 0)
        {
            tempPoint -= 1;
            SUI.pointUsedTotal += 1;
            SUI.pointUsedSTR += 1;
            SUI.tempSTR += 1;
            SUI.LvlSTR.text = "[+" + SUI.tempSTR + "] ";
        }
    }

    public void MinusSTR() {
        if (SUI.pointUsedSTR != 0)
        {
            tempPoint += 1; SUI.pointUsedTotal -= 1; SUI.pointUsedSTR -= 1;
            SUI.tempSTR -= 1;
            SUI.LvlSTR.text = "[+" + SUI.tempSTR + "] ";
        }
    }

    public void PlusSPD() {
        if (tempPoint != 0)
        {
            tempPoint -= 1;
            SUI.pointUsedTotal += 1;
            SUI.pointUsedSPD += 1;
            SUI.tempSPD += 1;
            SUI.LvlSPD.text = "[+" + SUI.tempSPD + "] ";
        }
    }

    public void MinusSPD() {
        if (SUI.pointUsedSPD != 0)
        {
            tempPoint += 1; SUI.pointUsedTotal -= 1; SUI.pointUsedSPD -= 1;
            SUI.tempSPD -= 1;
            SUI.LvlSPD.text = "[+" + SUI.tempSPD + "] ";
        }
    }

    void ResetStatUpdate() {
        tempPoint = 0;
        SUI.pointUsedTotal = 0;
        SUI.pointUsedSPD = 0;
        SUI.tempSPD = 0;
        SUI.tempSTR = 0;
        SUI.tempHP = 0;
        tempPoint = ExpManager.instance.POINT;
        SUI.LvlHP.text = "[+" + 0 + "] ";
        SUI.LvlSTR.text = "[+" + 0 + "] ";
        SUI.LvlSPD.text = "[+" + 0 + "] ";
    }

    public void ApplyPoint() {
        ExpManager.instance.RemovePoint(SUI.pointUsedTotal);
        tempPoint = 0;
        int MonUpHP = PlayerPrefs.GetInt("monHP") + SUI.tempHP;
        PlayerPrefs.SetInt("monHP", MonUpHP);
        int MonUPSTR = PlayerPrefs.GetInt("monAtk") + SUI.tempSTR;
        PlayerPrefs.SetInt("monAtk", MonUPSTR);
        int MonUPSPD = PlayerPrefs.GetInt("monSpd") + SUI.tempSPD;
        PlayerPrefs.SetInt("monSpd", MonUPSPD);
        ResetStatUpdate();        
    }

    public void CancelPoint() {
        ResetStatUpdate();
    }

    #endregion

    #region Weapon Armor Accessories Skill
    public void StatWeaponBtn() {
        SUI.StatisticUI.SetActive(false);
        SUI.AccUI.SetActive(false);
        SUI.ArmorUI.SetActive(false);
        SUI.WeaponUI.SetActive(true);
        SUI.SkillUI.SetActive(false);
        SUI.WS.value = 1;
    }

 
    public void StatArmorBtn()
    {
        SUI.StatisticUI.SetActive(false);
        SUI.AccUI.SetActive(false);
        SUI.ArmorUI.SetActive(true);
        SUI.WeaponUI.SetActive(false);
        SUI.SkillUI.SetActive(false);
        SUI.AS.value = 1;
    }

    public void StatAccBtn()
    {
        SUI.StatisticUI.SetActive(false);
        SUI.AccUI.SetActive(true);
        SUI.ArmorUI.SetActive(false);
        SUI.WeaponUI.SetActive(false);
        SUI.SkillUI.SetActive(false);
        SUI.ACCS.value = 1;
    }

    public void resetEquipBtn()
    {
        SUI.SkillUI.SetActive(false);
        SUI.AccUI.SetActive(false);
        SUI.ArmorUI.SetActive(false);
        SUI.WeaponUI.SetActive(false);
    }

    public void SkillUIOpen()
    {
        SUI.SkillUI.SetActive(true);
        SUI.StatisticUI.SetActive(false);
        SUI.AccUI.SetActive(false);
        SUI.ArmorUI.SetActive(false);
        SUI.WeaponUI.SetActive(false);
        SUI.SkillScroll.value = 1;
    }

    #endregion



    #endregion

    #region ShopUI

    public void OpenShopWeapon() {
        SUI.ShopWeapon.SetActive(true);
        SUI.ShopArmor.SetActive(false);
        SUI.ShopAcc.SetActive(false);
        SUI.ShopPotion.SetActive(false);
        SUI.WeaponScroll.value = 1;

    }

    public void OpenShopArmor() {
        SUI.ShopWeapon.SetActive(false);
        SUI.ShopArmor.SetActive(true);
        SUI.ShopAcc.SetActive(false);
        SUI.ShopPotion.SetActive(false);
        SUI.ArmorScroll.value = 1;

    }

    public void OpenShopAcc() {
        SUI.ShopWeapon.SetActive(false);
        SUI.ShopArmor.SetActive(false);
        SUI.ShopAcc.SetActive(true);
        SUI.ShopPotion.SetActive(false);
        SUI.AccScroll.value = 1;

    }


    public void OpenShopPotion() {
        SUI.ShopWeapon.SetActive(false);
        SUI.ShopArmor.SetActive(false);
        SUI.ShopAcc.SetActive(false);
        SUI.ShopPotion.SetActive(true);
        SUI.PotionScroll.value = 1;

    }

    #endregion

    #region OpeningMenu

    public void MoveOpeningToGameScreen() {
        if (IntroAlreadyHappen == 1)
        {
            Application.LoadLevel("GameScreen");
        }
        else if (IntroAlreadyHappen == 0)
        {
            Application.LoadLevel("OpeningStory");
        }
    }

    public void OpenCredit() {
        Credit.SetActive(true);
    }

    public void CloseCredit() {
        Credit.SetActive(false);
    }

    #endregion
    
    #region SettingButton

    public void ShowSetting()
    {
        SettingMenu.SetActive(true);
    }

    public void CloseSetting()
    {
        SettingMenu.SetActive(false);
    }

    #endregion

    #region Music/Sound Manager

    public void ButtonClickSound()
    {
        AS.SFXSource.clip = AS.ButtonSound;
        AS.SFXSource.Play();
    }

    public void LevelUpSound() {
        AS.SFXSource.clip = AS.LevelUpSound;
        AS.SFXSource.Play();
    }

    public void BuySound()
    {
        AS.SFXSource.clip = AS.BuySound;
        AS.SFXSource.Play();
    }

    public void EquipSound()
    {
        AS.SFXSource.clip = AS.EquipSound;
        AS.SFXSource.Play();
    }


    void PlayOP() {
        AS.MusicSource.clip = AS.OpeningMusic;
        AS.MusicSource.Play();
    }

    void PlayGS()
    {
        AS.MusicSource.clip = AS.GameScreenMusic;
        AS.MusicSource.Play();
    }

    void PlayShop() {
        AS.MusicSource.Pause();
        SM.MusicSource.clip = SM.ShopMusic;
        SM.MusicSource.Play();
    }

    void ShopClosed() {
        SM.MusicSource.Stop();
        AS.MusicSource.Play();
    }

    void PlayExplore() {
        AS.MusicSource.clip = AS.ExploreMusic;
        AS.MusicSource.Play();
    }

    void PlayTraining() {
        AS.MusicSource.clip = AS.TrainingMusic;
        AS.MusicSource.Play();
    }

    void PlayBattle() {
        AS.MusicSource.clip = AS.BattleMusic;
        AS.MusicSource.Play();
    }

    #endregion

}