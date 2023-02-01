
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance { get; private set; }

    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _spawnCount;
    [SerializeField] TMP_Text _timer;
    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
            _instance = this;
    }

    public void Timer(float timer) => _timer.text = ($"Time Remaining: {Mathf.RoundToInt(timer / 60)}:{Mathf.RoundToInt(timer % 60)}");
    public void SpawnCount(int count, int maxCount) => _spawnCount.text = ($"Enemies Left: {count}/{maxCount}");

    public void PlayerScore(int score) => _scoreText.text = ($"Score: {score.ToString()}");
    
}
