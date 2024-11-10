using UnityEngine;
using System.Collections.Generic;

public class CloudTrapSpawner : MonoBehaviour
{
    public GameObject trapPrefab; // The trap to spawn
    public int totalPositions = 100; // Total positions in the cloud
    public Vector3 spawnAreaMin; // The minimum position in the spawn area
    public Vector3 spawnAreaMax; // The maximum position in the spawn area
    public float refreshInterval = 1f; // How often to refresh traps (in seconds)
    private int luckValue;
    private List<Vector3> spawnPositions; // List to store 100 positions

    void Start()
    {
        // Get the luck value from the previous scene (default to 100 if not found)
        luckValue = PlayerPrefs.GetInt("LuckValue", 100);

        // Initialize the 100 positions in the cloud area
        spawnPositions = GenerateRandomPositions(totalPositions);

        // Start spawning traps continuously, refreshing every second
        InvokeRepeating("RefreshTraps", 0f, refreshInterval);
    }

    // Generates 100 random positions within the spawn area
    List<Vector3> GenerateRandomPositions(int count)
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );
            positions.Add(randomPosition);
        }
        return positions;
    }

    // Refreshes the traps by destroying the current ones and dropping new ones
    void RefreshTraps()
    {
        // Clear existing traps (if any)
        ClearExistingTraps();

        // Calculate how many traps to drop based on luck value
        int trapsToDrop = Mathf.CeilToInt(totalPositions * (1 - luckValue / 100f));

        // Drop the traps
        DropTraps(trapsToDrop);
    }

    // Destroys all currently existing traps
    void ClearExistingTraps()
    {
        GameObject[] existingTraps = GameObject.FindGameObjectsWithTag("Trap");
        foreach (GameObject trap in existingTraps)
        {
            Destroy(trap); // Remove the trap from the scene
        }
    }

    // Drops the number of traps at random positions
    void DropTraps(int trapsToDrop)
    {
        HashSet<int> usedIndices = new HashSet<int>();

        for (int i = 0; i < trapsToDrop; i++)
        {
            int randomIndex;

            // Ensure we are picking unique positions
            do
            {
                randomIndex = Random.Range(0, spawnPositions.Count);
            }
            while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);

            // Instantiate the trap at the random position
            Vector3 spawnPosition = spawnPositions[randomIndex];
            Instantiate(trapPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
