using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Snake", menuName = "ScriptableObjects/SnakeGamemode")]
public class Gamemode : ScriptableObject
{
    public string _name;
    [TextArea]
    public string _description;
    public int _difficulty;
    public int _scoreMultiplier;
}
