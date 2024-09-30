using DG.Tweening;
using TMPro;
using UnityEngine;

public class StartText : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;

    [SerializeField] private float _fadeTime;

    private void Awake()
    {
        _textMeshPro = transform.GetComponent<TextMeshProUGUI>();
        Fade();
    }

    private void Fade()
    {
        _textMeshPro.DOFade(0, _fadeTime).SetDelay(_fadeTime).OnComplete(() => _textMeshPro.DOFade(1, _fadeTime).SetDelay(_fadeTime).OnComplete(() => Fade()));
    }
}
