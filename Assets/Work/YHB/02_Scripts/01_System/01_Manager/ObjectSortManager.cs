using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSortManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _live;
    [SerializeField] private List<Transform> _unLive;

    private void Awake()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        Updating();
    }

    private void Updating()
    {
        foreach (Transform item in _live)
        {
            item.Find("Visual").GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(item.position.y * -10);
        }
    }

    private void Initialize()
    {
        foreach (Transform item in _unLive)
        {
            item.Find("Visual").GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(item.position.y * -10);
        }
    }
}
