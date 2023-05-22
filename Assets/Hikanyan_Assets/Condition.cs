using UnityEngine;
using System;

public class Condition : MonoBehaviour
{
    public byte player = 0b0000_0000;
    public byte poison = 0b0000_0001;
    public byte deadlyPoison = 0b0000_0011;
    public byte paralysis = 0b0000_0010;
    public byte sleep = 0b0000_0100;
    public byte silence = 0b0000_1000;
    private void Start()
    {
        Debug.Log("poison => condition" +
        Convert.ToString(player, 2).PadLeft(4, '0'));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Poison();
            Debug.Log("poison => condition" +
            Convert.ToString(player, 2).PadLeft(4, '0'));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            player &= ((byte)~poison);
            player &= ((byte)~deadlyPoison);
            Debug.Log($"recover_poison => condition {Convert.ToString(player, 2).PadLeft(4, '0')}");
        }
    }
    void Poison()
    {
        if(player == 0b0000_0001)
        {
            player &= ((byte)~poison);
            player |= deadlyPoison;
        }
        player |= poison;
    }
}