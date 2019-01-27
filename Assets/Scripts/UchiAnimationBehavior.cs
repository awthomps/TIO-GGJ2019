﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UchiAnimationBehavior : MonoBehaviour
{
    public float frameTime = 0.125f;
    public bool isGirl = false;
    public Sprite boy1;
    public Sprite boy2;
    public Sprite boy3;
    public Sprite boy4;
    public Sprite girl1;
    public Sprite girl2;
    public Sprite girl3;
    public Sprite girl4;


    private Rigidbody2D parentRigidBody;
    private SpriteRenderer sr;
    private Sprite[][] spriteMatrix;
    private int currentSprite;
    private float deltaTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        parentRigidBody = GetComponentInParent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();


        initializeSprites();
    }

    // Update is called once per frame
    void Update()
    {
        calculateFrame();


    }

    void initializeSprites()
    {
        int boy = 0;
        int girl = 1;
        currentSprite = 0;
        int numSpriteArrays = 2;
        spriteMatrix = new Sprite[numSpriteArrays][];
        for(int i = 0; i < numSpriteArrays; ++i)
        {
            spriteMatrix[i] = new Sprite[4];
        }

        spriteMatrix[boy][0] = boy1;
        spriteMatrix[boy][1] = boy2;
        spriteMatrix[boy][2] = boy3;
        spriteMatrix[boy][3] = boy4;
        spriteMatrix[girl][0] = girl1;
        spriteMatrix[girl][1] = girl2;
        spriteMatrix[girl][2] = girl3;
        spriteMatrix[girl][3] = girl4;
    }

    void calculateFrame()
    {
        Sprite[] sprites = spriteMatrix[isGirl ? 1 : 0];
        deltaTime += Time.deltaTime;

        // Loop to allow possible multiple frames to pass
        while(deltaTime >= frameTime)
        {
            deltaTime -= frameTime;
            currentSprite++;
            currentSprite %= sprites.Length;
        }

        // Set the sprite depending on the frame
        sr.sprite = sprites[currentSprite];

        determineDirection();

    }

    void determineDirection()
    {
        float xSpeed = parentRigidBody.velocity.x;
        if(xSpeed > 0.0f)
        {
            // Right
            transform.localScale = new Vector3(1, 1, 1);
        } else if(xSpeed < 0.0f)
        {
            // Left
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            // Neither, so no change
        }
    }
}
