using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.Utils;

// caption and color is attached to the collider sphere or box

public class ColliderSphere : MonoBehaviour, IListener
{
    public string   caption;
    public Color    color;
    public float    heightMultiplyer;
    public int      points;
    public bool     intersected = false;

    float radius;

    [HideInInspector]
    public bool     collected = false;

    GameObject          player;
    PlayerController    playerControl;
    AudioSource         audioData;


    void OnEnable()
    {
        IEventManager.Instance.AddListener("Collider::reset", this);
    }

    void OnDisable()
    {
        IEventManager.Instance.RemoveListener("Collider::reset");
    }


    public void OnEvent(string name, object param)
    {
        switch (name)
        {
            case "Collider::reset":     collected = false;       break;
        }
    }

    void Start()
    {
        player = TM_Utils.GetGameObject("player");
        if (player != null)
            playerControl = player.GetComponent<PlayerController>();

        audioData = GetComponent<AudioSource>();

        // calculate sphere radius: based on mesh bounds. asuming localScale x,y,z are the same, and localScale does not change
        Mesh    mesh    = GetComponent<MeshFilter>().mesh;
        Bounds  bounds  = mesh.bounds;

        radius = bounds.extents.x * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            // is player position inside sphere.
            if (distance < radius)
            {
                // best to use a message system, but as we have the player we can use it.
                // SimpleEventManager.TriggerEvent("Enter Sphere");

                if (intersected == false)
                {
                    intersected = true;
                    playerControl.SetColor(color);
                    playerControl.SetCaption(caption);
                    playerControl.SetHeight(heightMultiplyer);
                    playerControl.AddPoints(points);

                    if (audioData != null)
                        audioData.Play(0);
                }
            }
            else
            {
                intersected = false;
            }
        }
    }
}
