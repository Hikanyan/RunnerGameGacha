using UnityEngine;
public class ItemAddScorePoint:ItemBase
{
    [SerializeField] private int _scorePoint;
    protected override void Activate()
    {
        GameManager.Instance.AddScore(_scorePoint);
    }
}