﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IListener
{
    PhysicsSystem physicsSystem;
    Renderer _renderer;
    MaterialPropertyBlock _propBlock;

    public TextMesh textMesh;

    public Vector3 force;
    public float speed;

    float localScale = 1.0f;

    [SerializeField] int score = 0;
    [SerializeField] int lives = 3;


    void OnEnable()
    {
        IEventManager.Instance.AddListener("Player::Reset", this);
    }

    void OnDisable()
    {
        IEventManager.Instance.RemoveListener("Player::Reset");
    }


    public void OnEvent(string name, object param)
    {
        int intValue = (int)param;
        string stringValue = intValue.ToString();

        switch (name)
        {
            case "Player::Reset":
                score = 0;
                lives = 3;
                break;
        }
    }


    void Start()
    {
        physicsSystem = GetComponent<PhysicsSystem>();

        localScale = transform.localScale.x;

        score = 0;
        lives = 3;

        IEventManager.Instance.PostNotification("UIManager::SetLives", lives);
        IEventManager.Instance.PostNotification("UIManager::SetScore", score);
    }

    // for physics Fxed update is called a constant 50 times a second
    void FixedUpdate()
    {
        force = Vector3.zero;

        // use keyboard keys AWSD or left analogue stick to add a force
        force.z = Input.GetAxis("Vertical") * speed;
        force.x = Input.GetAxis("Horizontal") * speed;

        physicsSystem.AddForce(force, Time.deltaTime);
        physicsSystem.UpdatePhysics(1.0f);

        transform.position = physicsSystem.GetPosition();
    }

    public int getScore() { return score; }
    public int getLives() { return lives; }
    public int loseLife()
    {
        lives--;
        IEventManager.Instance.PostNotification("UIManager::SetLives", score); 
        return lives;
    }

    public void AddPoints(int n)
    {
        score += n;
        IEventManager.Instance.PostNotification("UIManager::SetScore", score);
    }


    // using a propertyblock is twice as fast as setting a value in a material.
    // and sets a shader paramerter directly.

    public void SetColor(Color c)
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
        if (_propBlock == null)
            _propBlock = new MaterialPropertyBlock();

        if (_renderer != null)
        {
            _renderer.GetPropertyBlock(_propBlock);
                _propBlock.SetColor("_Color", c);
            _renderer.SetPropertyBlock(_propBlock);
        }
    }

    public void SetCaption(string caption)
    {
        if (textMesh != null)
            textMesh.text = caption;
    }

    public void SetHeight(float heightMultiplyer)
    {
        transform.localScale = new Vector3(1.0f, localScale * heightMultiplyer, 1.0f);
    }
}
