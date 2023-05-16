using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColoring : MonoBehaviour
{
    private bool _active = false;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ToggleActive(Color color)
    {
        _active = !_active;
        if (_active)
        {
            _spriteRenderer.color = Color.blue;
        }
        else
        {
            _spriteRenderer.color = Color.clear;
        }
    }
}
