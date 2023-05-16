using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitionManager : MonoBehaviour
{
    [SerializeField] GameObject CMvcam1;
    [SerializeField] GameObject CMvcam2;

    [SerializeField] bool _open = false;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            _open = !_open;
            Debug.Log("Switch");
        }
    }

    private void Update() {
        SwapCams();    
    }

    private void SwapCams() {
        CMvcam1.SetActive(!_open);
        CMvcam2.SetActive(_open);
    }
}
