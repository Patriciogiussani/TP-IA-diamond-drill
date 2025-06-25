using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnRadius = 30f;
    public float itemHeightY = 1.6f;
    public float checkRadius = 0.5f;
    public LayerMask obstacleLayers;

    public ItemDirectionIndicator directionIndicator; // 🔁 Referencia a la flecha

    private GameObject currentItem;

    void Start()
    {
        SpawnNewItem();
    }

    public void SpawnNewItem()
    {
        if (currentItem != null)
            Destroy(currentItem);

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, spawnRadius, NavMesh.AllAreas))
            {
                Vector3 finalPosition = new Vector3(
                    hit.position.x,
                    itemHeightY,
                    hit.position.z
                );

                if (!Physics.CheckSphere(finalPosition, checkRadius, obstacleLayers))
                {
                    currentItem = Instantiate(itemPrefab, finalPosition, Quaternion.identity);
                    currentItem.GetComponent<Item>().spawner = this;

                    // 🔁 Actualiza la flecha para que apunte al nuevo ítem
                    if (directionIndicator != null)
                        directionIndicator.item = currentItem.transform;

                    return;
                }
            }
        }

        Debug.LogWarning("No se pudo encontrar una posición válida para el ítem.");
    }
}
