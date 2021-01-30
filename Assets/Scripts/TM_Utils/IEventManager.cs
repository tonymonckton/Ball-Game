using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
    example usage:

    class player MonoBehaviour, IListener {

        public void OnEvent(string name, object param) {
            switch (name) {
                case "health":
                    Debug.Log("health: "+(int) Param);
                    break;
            }
        }
    }

    void OnEnable()     {   IEventManager.Instance.AddListener("joyStick", this);   }
    void OnDisable()    {   IEventManager.Instance.RemoveListener("joyStick");      }
*/


public class IEventManager : MonoBehaviour {

    public Dictionary<string, IListener> iListeners;
    private static IEventManager eventManagerParms;

	public static IEventManager Instance {
		get 
        {
            if (!eventManagerParms) {
                eventManagerParms = FindObjectOfType (typeof (IEventManager)) as IEventManager;

                if (!eventManagerParms) {
                    Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
                } else
                {
                    eventManagerParms.Init();
                }
            }
            return eventManagerParms;
        }
	}

    void Init()
    {
        if (iListeners == null)
            iListeners = new Dictionary<string, IListener>();
    }

    public void AddListener(string name, IListener Listener) {

		if(iListeners.ContainsKey(name))
			return;

		iListeners.Add(name, Listener);
	}

	public void PostNotification(string name, object Param = null) {

		IListener Listener = null;
		if(!iListeners.TryGetValue(name, out Listener))
			return;

        Listener.OnEvent(name, Param);
	}

	public void RemoveListener(string name) {
		iListeners.Remove(name);
	}
}
