using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSortSet : MonoBehaviour
{
    private void Awake()
    {
        transform.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.parent.position.y * -100);
    }
}
