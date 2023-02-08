
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _enemiesLeft;
    private int _enemiesTotal;
  [SerializeField]  public int _enemiesGoal { get; set; } = 0;

    private void Start()
    {
        _enemiesTotal = SpawnManager._instance._spawnLimit;
    }

    private void Update()
    {
        _enemiesLeft = SpawnManager._instance._spawnCount;
        UIManager._instance.EnemiesEscaped(_enemiesGoal);
        Win();
        GameOver();
    }
    private void GameOver() 
    {
        if (_enemiesGoal >= Mathf.RoundToInt(_enemiesTotal * .5f)) 
        {
            Cursor.visible = true;
            SceneManager.LoadScene(2);
        }
    }
    
    private void Win() 
    {
        if (_enemiesLeft == 0)
        {
            Cursor.visible = true;
            SceneManager.LoadScene(1);
        }
    }

    public void RestartGame() 
    {
        Cursor.visible = false;
        SceneManager.LoadScene(0);
    }

}
