using System;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;

    public static event Action<int> GoldUpdated = delegate { };

    public int GOLD
    {
        get { return _gold; }
        private set { _gold = value; }
    }

    int _gold;


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
        GOLD = PlayerPrefs.GetInt("m_gold", 2000);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddGold(int amount)
    {
        GOLD += amount;
        PlayerPrefs.SetInt("m_gold", GOLD);
        GoldUpdated(GOLD);
    }

    public void RemoveGold(int amount)
    {
        GOLD -= amount;
        PlayerPrefs.SetInt("m_gold", GOLD);
        GoldUpdated(GOLD);
    }
}
