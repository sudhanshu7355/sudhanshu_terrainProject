using UnityEngine;
using System.Collections.Generic;

public class HouseSpawner : MonoBehaviour
{
    [SerializeField] private GameObject housePrefab;
    [SerializeField] private int numberOfHouses = 6;


    [SerializeField] private float houseWidth = 24.32828f;
    [SerializeField] private float houseDepth = 32.53511f;


    private Vector3 cornerA = new Vector3(-160.4f, 5f, 60f);     // Top Left
    private Vector3 cornerB = new Vector3(-73.3f, 5f, 59.7f);    // Top Right
    private Vector3 cornerC = new Vector3(-73.3f, 5f, -53.2f);   // Bottom Right
    private Vector3 cornerD = new Vector3(-161.7f, 5f, -60f);    // Bottom Left

    private List<Vector3> placedPositions = new List<Vector3>();

    void Start()
    {
        SpawnHouses();
    }

    private void SpawnHouses()
    {
        int spawned = 0;
        int maxAttemptsPerHouse = 50;

        while (spawned < numberOfHouses)
        {
            bool placed = false;

            for (int attempt = 0; attempt < maxAttemptsPerHouse; attempt++)
            {
                
                Vector3 candidatePos = (Random.value < 0.5f)
                    ? GetRandomPointInTriangle(cornerA, cornerB, cornerD)
                    : GetRandomPointInTriangle(cornerB, cornerC, cornerD);

                if (IsPositionValid(candidatePos))
                {
                    Instantiate(housePrefab, candidatePos, Quaternion.identity);
                    placedPositions.Add(candidatePos);
                    placed = true;
                    spawned++;
                    break;
                }
            }

            if (!placed)
            {
                Debug.LogWarning("Failed to place house without overlap after multiple attempts.");
                break;
            }
        }
    }

 
    private Vector3 GetRandomPointInTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        float r1 = Random.value;
        float r2 = Random.value;

        if (r1 + r2 > 1f)
        {
            r1 = 1f - r1;
            r2 = 1f - r2;
        }

        return a + r1 * (b - a) + r2 * (c - a);
    }

    private bool IsPositionValid(Vector3 newPos)
    {
        foreach (var existing in placedPositions)
        {
            float dx = Mathf.Abs(existing.x - newPos.x);
            float dz = Mathf.Abs(existing.z - newPos.z);

            if (dx < houseWidth && dz < houseDepth)
            {
                return false; 
            }
        }

        return true;
    }
}
