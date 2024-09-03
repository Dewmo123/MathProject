using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiffucultEnum
{
    Easy,
    Medium,
    Hard
}
[CreateAssetMenu(menuName = "SO/Problem")]
public class ProblemSO : ScriptableObject
{
    public DiffucultEnum diffucult;
    public Sprite Question;
    public string Answer;
    public int Width;
    public int Height;
}
