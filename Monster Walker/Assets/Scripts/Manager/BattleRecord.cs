using System;
using UnityEngine;

public class BattleRecord : MonoBehaviour
{
    public static BattleRecord instance { get; private set; }
    public static event Action<int> WinUpdated = delegate { };
    public static event Action<int> LoseUpdated = delegate { };

    public int WIN
    {
        get { return _win; }
        private set { _win = value; }
    }

    int _win;

    public int LOSE
    {
        get { return _lose; }
        private set { _lose = value; }
    }

    int _lose;

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
        WIN = ZPlayerPrefs.GetInt("BattleWin", 0);
        LOSE = ZPlayerPrefs.GetInt("BattleLose", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWin(int amount) {
        WIN += amount;
        ZPlayerPrefs.SetInt("BattleWin", WIN);
        WinUpdated(WIN);
    }

    public void AddLose(int amount) {
        LOSE += amount;
        ZPlayerPrefs.SetInt("BattleLose", LOSE);
        LoseUpdated(LOSE);
    }


}
