using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] GameObject squarePrefab;
    [SerializeField] float timeBetweenSpawns = 10f;

    float timeSinceLastSpawn = Mathf.Infinity;
    List<Vector3> tilePositions;
    List<Vector3> takenTiles;

    private void Awake()
    {
        tilePositions = new List<Vector3>();
        takenTiles = new List<Vector3>();

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
        
            if (takenTiles.Contains(tilePositions[tileIndex]))
                return;

            takenTiles.Add(tilePositions[tileIndex]);
            GameObject obj = Instantiate(squarePrefab, transform);
            obj.transform.localPosition = tilePositions[tileIndex];
            timeSinceLastSpawn = 0;
        }
        timeSinceLastSpawn += Time.deltaTime;
    }

    public void ReleaseTakenTile(Vector3 tilePos)
    {
        if (takenTiles.Contains(tilePos))
        {
            takenTiles.Remove(tilePos);
            Debug.Log(takenTiles.Count);
        }
    }
}
