using System;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public static PotionManager instance;

    public static event Action<int> LowPotionUpdated = delegate { };
    public static event Action<int> MedPotionUpdated = delegate { };
    public static event Action<int> HighPotionUpdated = delegate { };

    public int LOWPOTION
    {
        get { return _lowpotion; }
        private set { _lowpotion = value; }
    }

    int _lowpotion;

    public int MEDIUMPOTION
    {
        get { return _medpotion; }
        private set { _medpotion = value; }
    }

    int _medpotion;

    public int HIGHPOTION
    {
        get { return _highpotion; }
        private set { _highpotion = value; }
    }

    int _highpotion;


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
        LOWPOTION = PlayerPrefs.GetInt("LowPotion", 0);
        MEDIUMPOTION = PlayerPrefs.GetInt("MediumPotion", 0);
        HIGHPOTION = PlayerPrefs.GetInt("HighPotion", 0);

    }

    public void AddLowPot(int amount)
    {
        LOWPOTION += amount;
        PlayerPrefs.SetInt("LowPotion", LOWPOTION);
        LowPotionUpdated(LOWPOTION);
    }

    public void RemoveLowPot(int amount)
    {
        LOWPOTION -= amount;
        PlayerPrefs.SetInt("LowPotion", LOWPOTION);
        LowPotionUpdated(LOWPOTION);
    }

    public void AddMedPot(int amount)
    {
        MEDIUMPOTION += amount;
        PlayerPrefs.SetInt("MedPotion", MEDIUMPOTION);
        MedPotionUpdated(MEDIUMPOTION);
    }

    public void RemoveMedPot(int amount)
    {
        MEDIUMPOTION -= amount;
        PlayerPrefs.SetInt("MedPotion", MEDIUMPOTION);
        MedPotionUpdated(MEDIUMPOTION);
    }

    public void AddHighPot(int amount)
    {
        HIGHPOTION += amount;
        PlayerPrefs.SetInt("HighPotion", HIGHPOTION);
        HighPotionUpdated(HIGHPOTION);
    }

    public void RemoveHighPot(int amount)
    {
        HIGHPOTION -= amount;
        PlayerPrefs.SetInt("HighPotion", HIGHPOTION);
        HighPotionUpdated(HIGHPOTION);
    }

}
