using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    bool _isGameOver = false;
    [SerializeField] GameObject _gameOverText;

    public static GameState Instance;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && _isGameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void InitiateGameOver()
    {
        _isGameOver = true;
        _gameOverText.SetActive(true);
    }
}
