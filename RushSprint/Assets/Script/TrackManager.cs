using UnityEngine;
using System.Collections.Generic;

public class TrackManager : MonoBehaviour
{
    public GameObject[] trackPrefabs;
    public Transform player;
    private List<GameObject> activeTracks = new List<GameObject>();
    private float spawnZ = 0f;
    private float trackLength = 800f;
    private int numTracksOnScreen = 1;

    void Start()
    {
        for (int i = 0; i < numTracksOnScreen; i++)
        {
            SpawnTrack();
        }
    }

    void Update()
    {
        if (player.position.z - trackLength > (spawnZ - numTracksOnScreen * trackLength))
        {
            SpawnTrack();
            DeleteOldTrack();
        }
    }

    void SpawnTrack()
    {
        GameObject track = Instantiate(trackPrefabs[Random.Range(0, trackPrefabs.Length)], new Vector3(0, 0, spawnZ), Quaternion.identity);
        activeTracks.Add(track);
        spawnZ += trackLength;
    }

    void DeleteOldTrack()
    {
        Destroy(activeTracks[0]);
        activeTracks.RemoveAt(0);
    }
}
