using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] GameObject StartCheckpoint;
    [SerializeField] GameObject PlayerPrefab;

    [SerializeField] float RespawnDelay;

    private GameObject[] _allCheckpoints;
    private bool _isRespawning;
    private Vector2 _respawnPosition;

    private void Start()
    {
        _allCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    private GameObject GetNearestActiveCheckpointToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject nearest = _allCheckpoints[0];
        Vector2 playerPos = player.transform.position;
        float minDistance = Vector2.Distance(playerPos, _allCheckpoints[0].transform.position);

        for (int i = 0; i < _allCheckpoints.Length; i++)
        {
            if (_allCheckpoints[i].GetComponent<Checkpoint>().IsActive)
            {
                float distance = Vector2.Distance(playerPos, _allCheckpoints[i].transform.position);
                if (distance < minDistance)
                {
                    nearest = _allCheckpoints[i];
                    minDistance = distance;
                }
            }
        }

        return nearest;
    }

    private void DestroyPlayer()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

    public void Respawn()
    {
        if (_isRespawning)
            return;
        _isRespawning = true;

        GameObject checkpoint = GetNearestActiveCheckpointToPlayer();
        _respawnPosition = checkpoint.transform.position;
        DestroyPlayer();

        Invoke("ResetSpawner", RespawnDelay);
        Invoke("SpawnPlayer", RespawnDelay);
    }

    private void SpawnPlayer() => Instantiate(PlayerPrefab, _respawnPosition, new Quaternion(0, 0, 0, 0));
    private void ResetSpawner() => _isRespawning = false;
}
