using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartGameButton()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(0);
    }
}
