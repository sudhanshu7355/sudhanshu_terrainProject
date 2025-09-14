using UnityEngine;

public class ForestBiomeSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject forestBiomePrefab;

    [Header("Spawn Settings")]
    public int numberOfSpawns = 5;
    public float yOffset = 0f;
    public Vector3 prefabScale = new Vector3(1f, 1f, 1f);

    [Header("Spawn Area (World Bounds)")]
    public float minX = -103.1f;
    public float maxX = -15.7f;
    public float minZ = -182f;
    public float maxZ = -21.8f;

    void Start()
    {
        SpawnBiomes();
    }

    void SpawnBiomes()
    {
        if (forestBiomePrefab == null)
        {
            Debug.LogError("Forest Biome Prefab not assigned.");
            return;
        }

        for (int i = 0; i < numberOfSpawns; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(minX, maxX),
                yOffset,
                Random.Range(minZ, maxZ)
            );

            GameObject instance = Instantiate(forestBiomePrefab, randomPos, Quaternion.identity);
            instance.transform.localScale = prefabScale;
        }
    }
}
