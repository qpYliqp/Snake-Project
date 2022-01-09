using UnityEngine;

[CreateAssetMenu(fileName = "Snake", menuName = "ScriptableObjects/SnakeGamemode")]
public class Gamemode : ScriptableObject
{
    public string _name;
    [TextArea]
    public string _description;
}
