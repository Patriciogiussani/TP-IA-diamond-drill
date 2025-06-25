using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSpawner spawner; // El spawner que generó este objeto

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddPoint(); // Suma un punto
            spawner.SpawnNewItem();           // Genera otro item
            Destroy(gameObject);              // Destruye este item
        }
    }

}
