using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] float timeBetweenSpawns = 10f;

    float timeSinceLastSpawn = Mathf.Infinity;
    List<Vector3> tilePositions;
    List<int> takenTiles;

    private void Awake()
    {
        tilePositions = new List<Vector3>();
        takenTiles = new List<int>();

        foreach (Transform child in transform)
        {
            tilePositions.Add(child.localPosition);
        }
    }

    private void Update()
    {
        if(timeSinceLastSpawn>timeBetweenSpawns)
        {
            int tileIndex = Random.Range(0, tilePositions.Count);
        
            if (takenTiles.Contains(tileIndex))
                return;

            takenTiles.Add(tileIndex);
            GameObject obj = Instantiate(objectPrefab, transform);
            obj.transform.localPosition = tilePositions[tileIndex];
            timeSinceLastSpawn = 0;
        }
        timeSinceLastSpawn += Time.deltaTime;
    }
}
