using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private enum PickUpObjectType
    {
        HealthKit,
        BulletsKit,
        Battery
    }

    [SerializeField] private BoxCollider2D _itemsSpawnArea;
    [SerializeField] private PickUpObjectType _type;

    private const int HealthKitHPUp = 1;

    private void OnEnable()
    {
        RespawnObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (_type){
                case PickUpObjectType.HealthKit:
                    collision.GetComponent<ObjectHealth>().HealthPointUpObject(HealthKitHPUp);
                    break;
                case PickUpObjectType.BulletsKit:
                    collision.GetComponent<PlayerShooting>().ReloadGun();
                    break;
                case PickUpObjectType.Battery:
                    collision.GetComponent<PlayerLightZone>().ReloadBattery();
                    break;
            }
            RespawnObject();
        }
    }

    private void RespawnObject()
    {
        transform.position =
            new Vector3(Random.Range(_itemsSpawnArea.bounds.min.x, _itemsSpawnArea.bounds.max.x),
            Random.Range(_itemsSpawnArea.bounds.min.y, _itemsSpawnArea.bounds.max.y), 0);
    }
}
