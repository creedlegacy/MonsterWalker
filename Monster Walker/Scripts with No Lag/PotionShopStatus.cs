using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PotionShopStatus : MonoBehaviour
{
    public GameObject BuyBtn, warningtxt;
    private string e_name;[SerializeField] public int e_hp, e_price;
    public SpriteRenderer e_sprite;
    [SerializeField] private Text equipName, equipHP, equipPrice;
    private UIManager ui = new UIManager();

    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SetToText();
        e_sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    public void CurrentShopStatus(string equipname, int equiphp, Sprite equipSprite, int cost)
    {
        e_sprite.sprite = equipSprite;
        e_name = equipname;
        e_hp = equiphp;
        e_price = cost;
    }

    void SetToText()
    {
        equipName.text = e_name;

        if (e_hp >= 0)
        {
            equipHP.text = "+" + e_hp;
        }
        else
        {
            equipHP.text = e_hp.ToString();
        }
        equipPrice.text = e_price.ToString();

    }


    public void buyPotion() {
        if (GoldManager.instance.GOLD >= e_price)
        {
            GoldManager.instance.RemoveGold(e_price);
            if (e_name == "Low Health Potion")
            {
                PotionManager.instance.AddLowPot(1);
            }
            else if (e_name == "Medium Health Potion")
            {
                PotionManager.instance.AddMedPot(1);
            }
            else if (e_name == "High Health Potion")
            {
                PotionManager.instance.AddHighPot(1);
            }
        }
        else
        {
            StartCoroutine(WarningText());
        }
    }

    IEnumerator WarningText()
    {
        warningtxt.SetActive(true);
        yield return new WaitForSeconds(1f);
        warningtxt.SetActive(false);
    }
}
