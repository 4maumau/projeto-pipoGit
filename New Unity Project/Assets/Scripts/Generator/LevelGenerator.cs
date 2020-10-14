using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float playerToSpawn_Distance = 20f;
    
    [SerializeField] private Transform testPlatform;
    public Transform firstPart;
    public List <Transform> levelPartEasyList;
    public List<Transform> levelPartMediumList;
    public List<Transform> levelPartHardList;

    private enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    private int partsSpawned;

    public Transform player;

    private Vector3 lastEndPosition;

    private void Awake() {
        lastEndPosition = firstPart.Find("EndPosition").position;
        
        if (testPlatform != null)
        {
            Debug.Log("Using testing platform");
        }
        for (int i = 0; i < 5; i++){
            SpawnLevelPart();
        }
        
    }

    private void Update() 
    {
        if (Vector3.Distance(player.position, lastEndPosition) < playerToSpawn_Distance)
        {
            // spawn another part
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform chosenPart;
        List<Transform> difficultyList;

        switch (GetDifficulty())
        {
            default:
            case Difficulty.Easy:       difficultyList = levelPartEasyList;     break;
            case Difficulty.Medium:     difficultyList = levelPartMediumList;   break;
            case Difficulty.Hard:       difficultyList = levelPartHardList;     break;
            
        }

        
        chosenPart = difficultyList[Random.Range(0, difficultyList.Count)];

        if (testPlatform != null) chosenPart = testPlatform;

        Transform lastPartPosition = SpawnLevelPart(chosenPart, lastEndPosition);
        lastEndPosition = lastPartPosition.Find("EndPosition").position;
        
        partsSpawned++;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    private Difficulty GetDifficulty()
    {
        if (partsSpawned > 20) return Difficulty.Hard;
        if (partsSpawned > 10) return Difficulty.Medium;
       return Difficulty.Easy;
    }
}
