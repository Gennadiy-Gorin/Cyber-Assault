using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty {Easy,Medium,Hard }

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data", order = 55)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private string levelName;

    [SerializeField]
    private string levelDescription;

    [SerializeField]
    private int reward;

    [SerializeField]
    private Sprite image;

    [SerializeField]
    private bool isComplete;

    [SerializeField]
    private Difficulty difficulty;

    public string LevelName { get => levelName; }
    public string LevelDescription { get => levelDescription;  }
    public int Reward { get => reward;  }
    public Sprite Image { get => image;}
    public bool IsComplete { get => isComplete; set => isComplete = value; }
    public Difficulty Difficulty { get => difficulty; }
}
