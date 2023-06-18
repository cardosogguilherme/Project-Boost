using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelDelay = 1f;
    [SerializeField] private AudioClip collisionAudio;
    [SerializeField] private AudioClip successAudio;

    [SerializeField] private ParticleSystem collisionParticles;
    [SerializeField] private ParticleSystem successParticles;

    private AudioSource audioSource;
    private bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;

        Debug.Log(other.gameObject.tag);
        
        switch (other.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Fuel":
                break;
            case "Finish":
                StartCoroutine(StartSuccessSequence());
                break;
            default:
                StartCoroutine(CrashSequence());
                break;
        }
    }

    private IEnumerator StartSuccessSequence()
    {
        isTransitioning = true;

        DisableControls();

        successParticles.Play();

        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);

        Physics.autoSimulation = false;
        
        yield return new WaitForSeconds(levelDelay); // Invoke("LoadNextLevel", levelDelay);
        
        Physics.autoSimulation = true;
        LoadNextLevel();
    }

    private IEnumerator CrashSequence()
    {
        isTransitioning = true;

        DisableControls();

        collisionParticles.Play();

        audioSource.Stop();
        audioSource.PlayOneShot(collisionAudio);
        
        yield return new WaitForSeconds(levelDelay);

        ReloadLevel();
    }

    private void DisableControls()
    {
        GetComponent<Movement>().enabled = false;
    }

    private void LoadNextLevel()
    {
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        var nextIndex = nextSceneIndex == SceneManager.sceneCountInBuildSettings ? 0 : nextSceneIndex;
        
        SceneManager.LoadScene(nextIndex);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
