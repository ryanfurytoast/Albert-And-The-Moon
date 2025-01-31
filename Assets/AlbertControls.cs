﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlbertControls : MonoBehaviour
{
    Rigidbody AlbertRB;
    [SerializeField] AudioClip thrustSound;
    AudioSource jetpackSound;
    [SerializeField] AudioClip winningSFX;
    [SerializeField] AudioClip losingSFX;

    [SerializeField] float thrustSpeed;
    [SerializeField] float rotationSpeed;

    [SerializeField] ParticleSystem winningFX;
    [SerializeField] ParticleSystem losingFX;

    void Start()
    {
        AlbertRB = GetComponent<Rigidbody>();
        jetpackSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotation();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            AlbertRB.AddRelativeForce(Vector3.up * thrustSpeed);
            if (!jetpackSound.isPlaying)
            {
                jetpackSound.PlayOneShot(thrustSound);
            }
        }
    }

    void Rotation()
    {
        AlbertRB.freezeRotation = true;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * rotationSpeed);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }

        AlbertRB.freezeRotation = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("No Problem");
                break;

            case "Finish":
                winningFX.Play();
                jetpackSound.PlayOneShot(winningSFX);
                Invoke("LoadNextScene", 2);
                break;
            default:
                losingFX.Play();
                jetpackSound.PlayOneShot(losingSFX);
                Invoke("LoadPreviousScene", 2);
                SceneManager.LoadScene(0);
                break;
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int previousSceneIndex = currentSceneIndex - 1;
        if (previousSceneIndex == -1)
        {
            currentSceneIndex = 0;
        }
    }
}