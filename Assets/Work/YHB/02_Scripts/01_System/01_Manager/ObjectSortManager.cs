using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSortManager : MonoBehaviour
{
    public static ObjectSortManager instance = null;

    [SerializeField] private List<SpriteRenderer> _live;
    [SerializeField] private List<SpriteRenderer> _unLive;

    private void Awake()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        Updating();
    }

    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (SpriteRenderer item in _unLive)
        {
            item.sortingOrder = Mathf.RoundToInt(item.transform.parent.position.y * -100);
        }
    }

    private void Updating()
    {
        foreach (SpriteRenderer item in _live)
        {
            item.sortingOrder = Mathf.RoundToInt(item.transform.parent.position.y * -100);
        }
    }
}
