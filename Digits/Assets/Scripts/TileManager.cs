using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance { get; private set; }

    [SerializeField] GameObject squarePrefab;
    [SerializeField] float timeBetweenSpawns = 10f;

    private List<Vector3> availableTiles;
    private float timeSinceLastSpawn = Mathf.Infinity;

    private int startNumberOfTile = 1;
    private int currentMaxNumber = 1;
    private int numberOfTiles;

    private void Awake()
    {
        Instance = this;

        availableTiles = new List<Vector3>();

        foreach (Transform child in transform)
        {
            availableTiles.Add(child.localPosition);
        }
        numberOfTiles = availableTiles.Count;
    }

    private void Update()
    {
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            SpawnBehaviour();
            timeSinceLastSpawn = 0;
        }
        timeSinceLastSpawn += Time.deltaTime;
    }

    private void SpawnBehaviour()
    {
        if (availableTiles.Count == 0)
            return;

        CheckFieldCapacity();
        SpawnATile(startNumberOfTile);
    }

    public void SpawnATile(int number)
    {
        int tileIndex = Random.Range(0, availableTiles.Count);

        GameObject tile = Instantiate(squarePrefab, transform);
        tile.transform.localPosition = availableTiles[tileIndex];
        tile.GetComponent<TileDragging>().CurrentNumber = number;

        availableTiles.RemoveAt(tileIndex);
    }

    private void CheckFieldCapacity()
    {
        if (availableTiles.Count < numberOfTiles/2)
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

    public void SetMaxTileNumber(int number)
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

    public bool IsThereAFreeTile()
    {
        return availableTiles.Count != 0;
    }
}
