using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool IsActive = false;

    private void OnTriggerEnter2D(Collider2D collision) => Activate();

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}
