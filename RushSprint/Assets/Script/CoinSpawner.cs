using UnityEngine;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float zOffset = 0f; // Z-position on the tile where coins should appear
    public float[] trackPositionsX = new float[12]; // Set X positions for 12 tracks
    public int coinsPerTile = 4; // Spawn 4 coins per tile
    public Transform tileSpawnPoint; // Where to spawn coins per tile
    public float yPosition = 1f; // Height to place coins

    void Start()
    {
        // Example setup: evenly spaced tracks on X-axis
        for (int i = 0; i < trackPositionsX.Length; i++)
        {
            trackPositionsX[i] = -5.5f + i; // Example: from -5.5 to 5.5
        }

        // Call this when a new tile spawns (you can link it to your tile manager)
        SpawnCoinsOnTile(tileSpawnPoint.position);
    }

    public void SpawnCoinsOnTile(Vector3 tilePosition)
    {
        // Randomly choose 6 out of 12 track indices
        List<int> selectedTracks = GetRandomUniqueIndices(12, 6);

        // Randomly pick 4 out of the 6 selected tracks for coin placement
        List<int> coinTracks = GetRandomUniqueIndicesFromList(selectedTracks, coinsPerTile);

        foreach (int trackIndex in coinTracks)
        {
            Vector3 coinPos = new Vector3(
                trackPositionsX[trackIndex],
                yPosition,
                tilePosition.z + zOffset
            );

            Instantiate(coinPrefab, coinPos, Quaternion.identity);
        }
    }

    private List<int> GetRandomUniqueIndices(int max, int count)
    {
        List<int> indices = new List<int>();
        while (indices.Count < count)
        {
            int rand = Random.Range(0, max);
            if (!indices.Contains(rand))
                indices.Add(rand);
        }
        return indices;
    }

    private List<int> GetRandomUniqueIndicesFromList(List<int> source, int count)
    {
        List<int> result = new List<int>();
        List<int> copy = new List<int>(source);
        while (result.Count < count && copy.Count > 0)
        {
            int randIndex = Random.Range(0, copy.Count);
            result.Add(copy[randIndex]);
            copy.RemoveAt(randIndex);
        }
        return result;
    }
}

