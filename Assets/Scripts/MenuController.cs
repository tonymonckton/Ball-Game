using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    public void LoadScene(string sceneName)
    {
        //        SceneManager.LoadScene(sceneName);

        //        if (audioData != null)
        //            audioData.Play(0);

        StartCoroutine(WaitForSound(sceneName));
    }

    private IEnumerator WaitForSound(string sceneName)
    {
        audioData.Play(0);

        while (audioData.isPlaying)
        {
            yield return null;
        }

        IEventManager.Instance.PostNotification("GameManager::SetLevel", 1);
        IEventManager.Instance.PostNotification("Player::Reset", 0);

        SceneManager.LoadScene(sceneName);
        Debug.Log("sceneName: " + sceneName);
    }
}
