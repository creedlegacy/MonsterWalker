namespace PedometerU.Tests {

    using UnityEngine;
    using UnityEngine.UI;

    public class ExploreStep : MonoBehaviour {

        [SerializeField]private GameObject GameText, GameText2, GameText3, DeadText, FinishGameObject, AreUSure;
        public Text GoldGather, ExpGather, BattleCounter, TicketGather;
        public Scrollbar ExploreUIScroll;
        public Transform TextHolder;
        [SerializeField]private int AccGold, AccExp, AccTicket, ExpGain, BattleTotal;
        public Slider HPBar;
        public Pedometer pedometer;
        public double feet;
        public float multiplier;
        public int step1, milestone, mileNum, HP;
        [SerializeField]private bool notwalking, inst_again, inst_again2;
        private Monster M;
        private ParallaxBackground pb;
        public Text stepCounterText;
        public bool isDead, ContinuePlay;
        [SerializeField]private MapManager mm;
        int counter = 0;
        public void Awake()
        {
            ContinuePlay = false;
            isDead = false;
            notwalking = inst_again = inst_again2 = false;
            mileNum = 1;
            milestone = 50;
            multiplier = 1.0f;
            M = FindObjectOfType<Monster>();
            pb = FindObjectOfType<ParallaxBackground>();
        }

        private void Start () {
            // Create a new pedometer
            // Reset UI
            HPBar.maxValue = HP = ZPlayerPrefs.GetInt("m_current_hp");
            pedometer = new Pedometer(OnStep);
            OnStep(0, 0);
        }

        private void OnStep (int steps, double distance) {
            // Display the values // Distance in feet
            if (steps > 0)
            {
                pb.move = true;
                M.An.SetBool("IsWalking", true);
                
            }
            stepCounterText.text = steps.ToString();

            if (steps >= milestone && !isDead && !ContinuePlay)
            {
                milestone += 50;
                Encounter();
            }


            if (HP <= 0)
            {
                YouAreDead();
                isDead = true;
                M.An.SetBool("IsDead", true);
                OnDisable();
            }

        

            M.idletime = 0;
        }

        public void OnDisable () {
            // Release the pedometer
            pedometer.Dispose();
            pedometer = null;
        }

        public void Update()
        {
            HPBar.value = HP;
            
        }

        void Encounter() {
            int randomNumber = Random.Range(0, 100);
            if (randomNumber <= 25)
            {
                GameText.GetComponent<Text>().text = "You Found Nothing!";
                
            }
            else if(randomNumber > 25)
            {
                int randomNumber2 = Random.Range(0, 100);
                if (randomNumber2 <= 30)
                {
                    int GoldRandomGenerate = Random.Range(5, 80);
                    GameText.GetComponent<Text>().text = "You Found " + GoldRandomGenerate + " Gold!";
                    AccGold += GoldRandomGenerate;
                }
                else if (randomNumber2 <= 80)
                {
                    int monChance = Random.Range(1, M.OM.Level + 3);
                    if (monChance <= M.OM.Level)
                    {
                        GameText.GetComponent<Text>().text = "You defeated a monster!";
                        ExpGain = monChance * 5;
                        inst_again = true;
                    }
                    else if(monChance >= M.OM.Level)
                    {
                        //hp monster
                        int randomEnemyHp = Random.Range(10, 7 * M.OM.Level);

                        int EnemyHpLeft = randomEnemyHp - ZPlayerPrefs.GetInt("m_current_atk");

                        if (EnemyHpLeft <= 0)
                        {
                            GameText.GetComponent<Text>().text = "With your great Strength you defeated a monster!";
                        }
                        else
                        {
                            float chanceOfDodge = Random.Range(0f, 100f);
                            float monsterSpd = ZPlayerPrefs.GetInt("m_current_spd") / 2;

                            if (monsterSpd > chanceOfDodge)
                            {
                                GameText.GetComponent<Text>().text = "You defeated a monster and dodged the attack!";

                            }
                            else
                            {
                                GameText.GetComponent<Text>().text = "You defeated a monster! But got injured!";
                                GameText3.GetComponent<Text>().text = "-" + monChance + "HP";
                                inst_again2 = true;
                                HP -= monChance;
                            }
                        }
                        
                        ExpGain = monChance * 5;
                        inst_again = true;
                        AccExp += ExpGain;
                        BattleTotal += 1;
                    }
                }
                else if(randomNumber2 <= 100)
                {
                    GameText.GetComponent<Text>().text = "You obtain Battle Ticket!";
                    AccTicket += 1;
                }
            }

            GameObject gt;
            gt = (GameObject)Instantiate(GameText) as GameObject;
            gt.transform.SetParent(TextHolder);
            gt.transform.localScale = new Vector3(1, 1, 1);


            if (inst_again2)
            {
                GameObject gt3;
                gt3 = (GameObject)Instantiate(GameText3) as GameObject;
                gt3.transform.SetParent(TextHolder);
                gt3.transform.localScale = new Vector3(1, 1, 1);
            }

            if (inst_again)
            {
                GameText2.GetComponent<Text>().text = "You gain " + ExpGain + " Exp!";
                GameObject gt2;
                gt2 = (GameObject)Instantiate(GameText2) as GameObject;
                gt2.transform.SetParent(TextHolder);
                gt2.transform.localScale = new Vector3(1, 1, 1);
            }
            
            inst_again = inst_again2 = false;
            ExploreUIScroll.value = 0;
            Debug.Log(randomNumber);
            
        }

        void YouAreDead() {
            StatisticManager.instance.AddDE(1);
            DeadText.GetComponent<Text>().text = "Your Monster have died! Your Gold and Exp has been decreased by 0.25x!";
            DeadText.GetComponent<Text>().fontSize = 100;
            GameObject gt;
            gt = (GameObject)Instantiate(DeadText) as GameObject;
            gt.transform.SetParent(TextHolder);
            gt.transform.localScale = new Vector3(1, 1, 1);

            float PenaltyGold = AccGold * 0.25f;
            float PenaltyExp = AccExp * 0.25f;

            AccGold -= (int)PenaltyGold;
            AccExp -= (int)PenaltyExp;

            if (AccGold <= 0)
            {
                AccGold = 0;
            }

            if (AccExp <= 0)
            {
                AccExp = 0;
            }


        }

        public void OpenAreUSure() {
            AreUSure.SetActive(true);
        }

        public void SureYes()
        {
            AreUSure.SetActive(false);
            ExploringFinish();
        }

        public void SureNo() {
            AreUSure.SetActive(false);
        }

        public void ExploringFinish() {

            FinishGameObject.SetActive(true);

            GoldManager.instance.AddGold(AccGold);
            ExpManager.instance.AddEXP(AccExp);
            TicketManager.instance.AddTicket(AccTicket);
            StatisticManager.instance.AddBC(BattleTotal);


            ContinuePlay = true;
            mm.ExploreFinish = true;

            GoldGather.text = AccGold.ToString();
            ExpGather.text = AccExp.ToString();
            BattleCounter.text = BattleTotal.ToString();
            TicketGather.text = AccTicket.ToString();
            pedometer.Dispose();
        }
    }
    
}