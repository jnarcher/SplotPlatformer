using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    private Vector2 _respawnPoint;
    private bool _isActive = false;

    private void Start()
    {
        _respawnPoint = gameObject.transform.GetChild(0).position;
    }

    private void OnTriggerEnter2D(Collider2D collision) => Activate();

    public void Activate() => _isActive = true;
    public void Deactivate() => _isActive = false;
    public bool IsActive() => _isActive;
    public Vector2 RespawnPoint() => _respawnPoint;
}
