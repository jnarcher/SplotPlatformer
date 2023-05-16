using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollision : MonoBehaviour
{
    private GameObject _player;
    private GameObject _coloredEdge;
    private TileColoring _tileColoring;

    private void Start()
    {
        _coloredEdge = gameObject.transform.GetChild(0).gameObject;
        _tileColoring = _coloredEdge.GetComponent<TileColoring>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
            _tileColoring.ToggleActive(Color.blue);
    }
}
