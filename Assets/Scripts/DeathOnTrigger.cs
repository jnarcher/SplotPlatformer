using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnTrigger : MonoBehaviour
{
    private DeathManager deathManager;

    private void Start()
    {
        deathManager = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<DeathManager>();
    }

    private void OnTriggerEnter2D(Collider2D _) => deathManager.Respawn();
}
