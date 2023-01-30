using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public int Score { get; private set; }

    private void Awake()
    {
        Cursor.visible = false;
    }
    public void PlayerScore(int value) 
    {
        Score += value;
        UIManager._instance.PlayerScore(Score);
    }

    private void Update()
    {
        Debug.Log(Score);
    }
}
