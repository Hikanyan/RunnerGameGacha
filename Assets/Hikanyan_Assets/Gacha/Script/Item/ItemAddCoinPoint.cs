using UnityEngine;
public class ItemAddCoinPoint:ItemBase
{
    [SerializeField] private int _coinPoint;
    protected override void Activate()
    {
        GameManager.Instance.AddCoin(_coinPoint);
    }
}