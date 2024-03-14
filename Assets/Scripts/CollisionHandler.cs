using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private float delayTime = 2.0f;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip successClip;

    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private ParticleSystem successParticle;

    private bool isTransitioning = false;
    private bool isCollisionActive = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GetCommandKey();
    }

    private void GetCommandKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionActive = !isCollisionActive;
            Debug.Log("isCollisionActive:" + isCollisionActive);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || !isCollisionActive) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }
    private void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successClip);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }

    private void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);
        deathParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
