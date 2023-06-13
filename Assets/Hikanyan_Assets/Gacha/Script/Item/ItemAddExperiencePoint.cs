using UnityEngine;
using UnityEngine.Serialization;

public class ItemAddExperiencePoint:ItemBase
{
    [SerializeField] private int _experiencePoint;
    protected override void Activate()
    {
        GameManager.Instance.AddScore(_experiencePoint);
    }
}