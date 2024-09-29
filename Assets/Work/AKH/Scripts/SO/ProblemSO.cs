using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultEnum
{
    Easy,
    Medium,
    Hard
}
[CreateAssetMenu(menuName = "SO/Problem")]
public class ProblemSO : ScriptableObject
{
    public DifficultEnum diffucult;
    public Sprite Image;
    public string Answer;
    public int Width;
    public int Height;
}
