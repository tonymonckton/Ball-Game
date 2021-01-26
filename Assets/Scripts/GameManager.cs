using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TM.Utils;
using UnityEngine.UI;

/*
    Game Manager responsibilities:

    Reset Game  : Post reset message to scripts, when starting new game. eg player fails game (3 lives), and starts again
    Game Over   : detect game over, when player falls off arena, or timeout. and lose a life.
    Next Level  : start next level, when play completes all tasks

*/

public class GameManager : MonoBehaviour, IListener
{
    public PlayerController player;
    public AudioSource audioStartNextLevel;
    public AudioSource audioGameOver;

    // we want to beable to continue another time
    // for debugging these are public right now.

    [SerializeField] int level = 0;
    [SerializeField] Dictionary<int, string> gameLevels = new Dictionary<int, string>();

    void OnEnable()
    {
        IEventManager.Instance.AddListener("GameManager::SetLevel", this);
    }

    void OnDisable()
    {
        IEventManager.Instance.RemoveListener("GameManager::SetLevel");
    }

    public void OnEvent(string name, object param)
    {
        int intValue = (int)param;

        switch (name)
        {
            case "GameManager::SetLevel":   level = intValue; break;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // reset the colliders, at start of new game
    void Start()
    {
        GameObject playerObject = TM_Utils.GetGameObject("player");
        if (playerObject != null)
            player = playerObject.GetComponent<PlayerController>();

        // Remember to add levels to unity build settings
        gameLevels.Clear();
        gameLevels.Add(0, "StartGame");
        gameLevels.Add(1, "Level 1");
        gameLevels.Add(2, "Level 2");

        ResetGame();
        UpDateUI();
    }

    // TODO: use a state machine

    void Update()
    {
        if (player != null)
        {
            // player fails
            if (player.transform.position.y < -10.0f) // player fell off floor
            {
                int lives = player.loseLife();

                // game over?
                if (lives == 0)
                    GameOver();
                else
                    TryAgain();
            }

            // Complete Level?
            if (LevelCompleted())
            {
                level++;
                NextLevel();
            }

            // cheat keys
            if (Input.GetKeyDown(KeyCode.Alpha0) && level != 0)
            {
                level = 0;
                NextLevel();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && level != 1)
            {
                level = 1;
                NextLevel();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && level != 2)
            {
                level = 2;
                NextLevel();
            }
        }
    }


    void UpDateUI()
    {
        IEventManager.Instance.PostNotification("UIManager::SetLevel", level);
    }

    bool LevelCompleted()
    {
        return false;
    }

    void ResetGame()
    {
        IEventManager.Instance.PostNotification("Collider::reset");
    }

    void TryAgain()
    {
        IEventManager.Instance.PostNotification("Collider::reset");
    }

    void GameOver()
    {
        // TODO: display game over

        level = 1;

        IEventManager.Instance.PostNotification("Player::Reset", 0);

        GotoStartGame(gameLevels[0]);
    }

    IEnumerator GotoStartGame(string sceneName)
    {
        if (audioGameOver != null)
        {
            audioGameOver.Play(0);

            while (audioGameOver.isPlaying)
                yield return null;
        }

        SceneManager.LoadScene(sceneName);
        Debug.Log("sceneName: " + sceneName);
    }

    void NextLevel()
    {
        StartNextLevel(gameLevels[level]);
    }

    IEnumerator StartNextLevel(string sceneName)
    {
        if (audioStartNextLevel != null)
        {
            audioStartNextLevel.Play(0);

            while (audioStartNextLevel.isPlaying)
                yield return null;
        }

        SceneManager.LoadScene(sceneName);
        Debug.Log("sceneName: " + sceneName);
    }
}
