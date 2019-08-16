using System;
using UnityEngine;

public class TicketManager : MonoBehaviour
{
    public static TicketManager instance {get; private set;}

    public static event Action<int> TicketUpdated = delegate { };

    public int TICKET
    {
        get { return _ticket; }
        private set { _ticket = value; }
    }

    int _ticket;


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
        TICKET = PlayerPrefs.GetInt("BattleTicket", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTicket(int amount)
    {
        TICKET += amount;
        PlayerPrefs.SetInt("BattleTicket", TICKET);
        TicketUpdated(TICKET);
    }

    public void RemoveTicket(int amount)
    {
        TICKET -= amount;
        PlayerPrefs.SetInt("BattleTicket", TICKET);
        TicketUpdated(TICKET);
    }

}
