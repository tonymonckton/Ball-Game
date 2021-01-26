using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IListener
{
    [SerializeField] Text UI_Lives;
    [SerializeField] Text UI_Score;
    [SerializeField] Text UI_Level;

    void OnEnable()
    {
        IEventManager.Instance.AddListener("UIManager::SetLives", this);
        IEventManager.Instance.AddListener("UIManager::SetScore", this);
        IEventManager.Instance.AddListener("UIManager::SetLevel", this);
    }

    public void OnEvent(string name, object param)
    {
        int data = (int)param;
        string value = data.ToString();

        Debug.Log("UIManager::OnEvent "+name+ " data: "+data+ " value: "+ value);

        switch (name)
        {
            case "UIManager::SetLives":
                UI_Lives.text = value;
                //UI_Lives.SetAllDirty();
                break;
            case "UIManager::SetScore":
                UI_Score.text = value;
                //UI_Score.SetAllDirty();
                break;
            case "UIManager::SetLevel":
                UI_Level.text = value;
                //UI_Score.SetAllDirty();
                break;
        }
    }
}
