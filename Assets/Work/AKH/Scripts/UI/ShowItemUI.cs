using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemUI : MonoBehaviour
{
    private Image _image;
    [SerializeField] private ItemSO _item;
    [SerializeField] private TextMeshProUGUI _text;
    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _item.sprite;
    }
    private void Update()
    {
        _text.text = _item.cnt.Value.ToString();
    }
}
