using UnityEngine;

enum ItemEnum
{
    None,
    ExperiencePoint,
    CoinPoint,
    ScorePoint
    
}
public abstract class ItemBase:MonoBehaviour
{
    [SerializeField] string _itemName;
    [SerializeField] ItemEnum _item = ItemEnum.None;
    protected abstract void Activate();
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Activate();
        }
    }
}