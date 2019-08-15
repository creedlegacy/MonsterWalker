using UnityEngine;

public class EnemyMonster : MonoBehaviour
{
    public EnemyMonsterStatus EMS;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[System.Serializable]
public class EnemyMonsterStatus
{
    public int Num;
    public string NickName;
    public Sprite EnemyElement;
    public BaseMonster monster;
    public int Level;
    public int hp;
    public int atk;
    public int spd;
}