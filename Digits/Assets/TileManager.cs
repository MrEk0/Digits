using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance { get; private set; }

    [SerializeField] GameObject squarePrefab;
    [SerializeField] float timeBetweenSpawns = 10f;

    private float timeSinceLastSpawn = Mathf.Infinity;
    private int startDigitOfTile = 1;
    private List<Vector3> tilePositions;
    private List<Vector3> takenTiles;
    private int currentMaxNumber = 1;

    public event Action OnTileWereSummed;

    private void Awake()
    {
        Instance = this;

        tilePositions = new List<Vector3>();
        takenTiles = new List<Vector3>();

        foreach (Transform child in transform)
        {
            tilePositions.Add(child.localPosition);
        }
    }

    private void Update()
    {
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            int tileIndex = UnityEngine.Random.Range(0, tilePositions.Count);

            if (takenTiles.Contains(tilePositions[tileIndex]))
                return;

            takenTiles.Add(tilePositions[tileIndex]);
            Debug.Log(takenTiles.Count);
            CheckFieldOccupancy();

            GameObject tile = Instantiate(squarePrefab, transform);
            tile.transform.localPosition = tilePositions[tileIndex];
            tile.GetComponent<SquareDragging>().CurrentNumber = startDigitOfTile;

            timeSinceLastSpawn = 0;
        }
        timeSinceLastSpawn += Time.deltaTime;
    }

    private void CheckFieldOccupancy()
    {
        if(takenTiles.Count>tilePositions.Count/2)
        {
            startDigitOfTile = UnityEngine.Random.Range(1, currentMaxNumber);
        }
    }

    public void RemoveTakenTilePosition(Vector3 tilePos)
    {
        if (takenTiles.Contains(tilePos))
        {
            takenTiles.Remove(tilePos);
        }
    }

    public void AddTilePosition(Vector3 tilePos)
    {
        if(!takenTiles.Contains(tilePos))
        {
            takenTiles.Add(tilePos);
        }
    }

    public void ChangeScore()
    {
        OnTileWereSummed();
    }

    public void SetMaxTileNumber(int number)
    {
        if(number>currentMaxNumber)
        {
            currentMaxNumber = number;
        }
    }
}
