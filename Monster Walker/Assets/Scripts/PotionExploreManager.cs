using PedometerU.Tests;
using UnityEngine;
using UnityEngine.UI;

public class PotionExploreManager : MonoBehaviour
{

    [SerializeField]private Button LowPot, MedPot, HighPot;
    [SerializeField] private Text LowPotQuantity, MedPotQuantity, HighPotQuantity;
    private ExploreStep es;

    private void Awake()
    {
        es = FindObjectOfType<ExploreStep>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LowPotQuantity.text = "x" + PotionManager.instance.LOWPOTION;
        MedPotQuantity.text = "x" + PotionManager.instance.MEDIUMPOTION;
        HighPotQuantity.text = "x" + PotionManager.instance.HIGHPOTION;

        if (PotionManager.instance.LOWPOTION == 0)
        {
            LowPot.interactable = false;
        }
        else
        {
            LowPot.interactable = true;
        }

        if (PotionManager.instance.MEDIUMPOTION == 0)
        {
            MedPot.interactable = false;
        }
        else
        {
            MedPot.interactable = true;
        }

        if (PotionManager.instance.HIGHPOTION == 0)
        {
            HighPot.interactable = false;
        }
        else
        {
            HighPot.interactable = true;
        }


    }

    public void LowPotClick() {
        PotionManager.instance.RemoveLowPot(1);
        es.HP += 5;
        RegulateHealthandDead();

    }

    public void MedPotClick()
    {
        PotionManager.instance.RemoveMedPot(1);
        es.HP += 20;
        RegulateHealthandDead();

    }

    public void HighPotClick()
    {
        PotionManager.instance.RemoveHighPot(1);
        es.HP += 50;
        RegulateHealthandDead();

    }

    void RegulateHealthandDead() {
        if (es.HP > PlayerPrefs.GetInt("m_current_hp"))
        {
            es.HP = PlayerPrefs.GetInt("m_current_hp");
        }

        if (es.isDead)
        {
            es.ContinuePlay = true;
        }

    }


}
