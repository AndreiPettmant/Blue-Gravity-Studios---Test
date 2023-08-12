using UnityEngine;

public enum GameStatus
{
    Playing,
    Dialogue
}

public class GameManager : MonoBehaviour
{
    public static GameStatus CurrentGameStatus { get; private set; } = GameStatus.Playing;

    public static void SetGameStatus(GameStatus newStatus)
    {
        CurrentGameStatus = newStatus;
    }
}