using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance { get; private set; }

    [SerializeField] GameObject squarePrefab;
    [SerializeField] float timeBetweenSpawns = 10f;

    private float timeSinceLastSpawn = Mathf.Infinity;
    private float startNumberOfTile = 1;
    private List<Vector3> tilePositions;
    private List<Vector3> availableTiles;
    private float currentMaxNumber = 1;

    private void Awake()
    {
        Instance = this;

        tilePositions = new List<Vector3>();
        availableTiles = new List<Vector3>();

        foreach (Transform child in transform)
        {
            tilePositions.Add(child.localPosition);
        }

        availableTiles = tilePositions;
    }

    private void Update()
    {
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            Spawn();
            timeSinceLastSpawn = 0;
        }
        timeSinceLastSpawn += Time.deltaTime;
    }

    private void Spawn()
    {
        if (availableTiles.Count == 0)
            return;

        int tileIndex = Random.Range(0, availableTiles.Count);

        CheckFieldOccupancy();

        GameObject tile = Instantiate(squarePrefab, transform);
        tile.transform.localPosition = tilePositions[tileIndex];
        tile.GetComponent<TileDragging>().CurrentNumber = startNumberOfTile;

        availableTiles.RemoveAt(tileIndex);       
    }

    public void SpawnBoughtTile(float number)
    {
        Spawn();
    }

    private void CheckFieldOccupancy()
    {
        if (availableTiles.Count < tilePositions.Count / 2)
        {
            startNumberOfTile = Random.Range(1, currentMaxNumber);
        }
    }


    public void AddAvailableTilePosition(Vector3 tilePos)
    {
        if (!availableTiles.Contains(tilePos))
        {
            availableTiles.Add(tilePos);
        }
    }

    public void RemoveAvailableTilePosition(Vector3 tilePos)
    {
        if (availableTiles.Contains(tilePos))
        {
            availableTiles.Remove(tilePos);
        }

    }

    public void SetMaxTileNumber(float number)
    {
        if(number>currentMaxNumber)
        {
            currentMaxNumber = number;
        }
    }




    public bool IsTileAvailable(Vector3 tilePos)
    {
        if(availableTiles.Contains(tilePos))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
