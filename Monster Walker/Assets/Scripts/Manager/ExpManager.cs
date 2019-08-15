using System;
using UnityEngine;

public class ExpManager : MonoBehaviour
{

    public static ExpManager instance;

    public static event Action<int> ExpUpdated = delegate { };

    public int EXP
    {
        get { return _exp; }
        private set { _exp = value; }
    }

    int _exp;


    public static event Action<int> PointUpdated = delegate { };

    public int POINT
    {
        get { return _point; }
        private set { _point = value; }
    }

    int _point;


    public static event Action<int> ExpCostUpdated = delegate { };


    public int EXPCOST
    {
        get { return _expcost; }
        private set { _expcost = value; }
    }

    int _expcost;

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
        EXPCOST = ZPlayerPrefs.GetInt("expCost", 200);
        EXP = ZPlayerPrefs.GetInt("m_exp", 0);
        POINT = ZPlayerPrefs.GetInt("m_lvl_point", 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddEXP(int amount) {
        EXP += amount;
        ZPlayerPrefs.SetInt("m_exp", EXP);
        ExpUpdated(EXP);
    }

    public void RemoveEXP(int amount)
    {
        EXP -= amount;
        ZPlayerPrefs.SetInt("m_exp", EXP);
        ExpUpdated(EXP);
    }


    public void AddPoint(int amount)
    {
        POINT += amount;
        ZPlayerPrefs.SetInt("m_lvl_point", POINT);
        PointUpdated(POINT);
    }

    public void RemovePoint(int amount)
    {
        POINT -= amount;
        ZPlayerPrefs.SetInt("m_lvl_point", POINT);
        PointUpdated(POINT);
    }

    public void MonLevelUp() {
        EXPCOST += EXPCOST;
        ZPlayerPrefs.SetInt("expCost", EXPCOST);
        ExpCostUpdated(EXPCOST);
    }


}
