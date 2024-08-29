using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSortManager : MonoBehaviour
{
    public static ObjectSortManager instance = null;

    [SerializeField] private List<SpriteRenderer> _live;
    [SerializeField] public List<SpriteRenderer> _unLive;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        Updating();
    }

    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
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
