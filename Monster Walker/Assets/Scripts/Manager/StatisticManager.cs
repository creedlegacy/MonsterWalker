using System;
using UnityEngine;

public class StatisticManager : MonoBehaviour
{
    public static StatisticManager instance;

    public static event Action<int> StepUpdated = delegate { };
    public static event Action<int> TopStepUpdated = delegate { };
    public static event Action<int> BattleEncounterUpdated = delegate { };
    public static event Action<int> DeathExploreUpdated = delegate { };

    public int STEP
    {
        get { return _step; }
        private set { _step = value; }
    }

    public int TOPSTEP {
        get { return _topstep; }
        private set { _topstep = value; }
    }

    public int BATTLECOUNTER
    {
        get { return _battlecounter; }
        private set { _battlecounter = value; }
    }

    public int DEATHEXPLORE
    {
        get { return _deathexplore; }
        private set { _deathexplore = value; }
    }


    int _deathexplore;
    int _battlecounter;
    int _step;
    int _topstep;

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
        STEP = ZPlayerPrefs.GetInt("m_step", 0);
        TOPSTEP = ZPlayerPrefs.GetInt("totalStep", 0);
        BATTLECOUNTER = ZPlayerPrefs.GetInt("battleCounter", 0);
        DEATHEXPLORE = ZPlayerPrefs.GetInt("deathExplore", 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddStep(int amount)
    {
        STEP += amount;
        ZPlayerPrefs.SetInt("m_step", STEP);
        StepUpdated(STEP);
    }

    public void BestStep(int amount) {

        if (amount > ZPlayerPrefs.GetInt("totalStep")){
            ZPlayerPrefs.SetInt("totalStep", amount);
            TOPSTEP = ZPlayerPrefs.GetInt("totalStep");
            TopStepUpdated(TOPSTEP);
        }
    }

    public void AddBC(int amount)
    {
        BATTLECOUNTER += amount;
        ZPlayerPrefs.SetInt("battleCounter", BATTLECOUNTER);
        BattleEncounterUpdated(BATTLECOUNTER);
    }

    public void AddDE(int amount)
    {
        DEATHEXPLORE += amount;
        ZPlayerPrefs.SetInt("deathExplore", DEATHEXPLORE);
        DeathExploreUpdated(DEATHEXPLORE);
    }


}
