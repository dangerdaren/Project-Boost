//using System;
//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] GameObject healthyRocket;
    [SerializeField] GameObject blownUpRocket;
    public GameObject[] bits;

    [SerializeField] private int currentLevel;

    [SerializeField] private float rcsThrust = 75f;
    [SerializeField] private float mainThrust = 50f;
    [SerializeField] private float levelLoadDelay = 2.5f;

    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip success;

    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private ParticleSystem successParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    private bool collisionsDisabled = false;


    enum State { Alive, Dying, Transcending };
    State state = State.Alive;


    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();

            if (Debug.isDebugBuild == true)
            {
                CheckDebugKeys();
            }
        }
    }

    private void CheckDebugKeys()
    {
        // Check for Next Level Debug Pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentLevel += 1;
            LoadNextLevel();
        }
        // Check for Collisions Off Debug Active
        else if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggle state (clever!)
            collisionsDisabled = !collisionsDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionsDisabled == true) { return; } // ignore collisions when dead

        audioSource.Stop();
        mainEngineParticles.Stop();

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        MakeRocketExplode();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void MakeRocketExplode()
    {
        blownUpRocket.SetActive(true);
        healthyRocket.SetActive(false);
        foreach (GameObject go in bits)
        {
            go.GetComponent<Rigidbody>().AddExplosionForce(mainThrust, transform.position, 50);
        }

    }

    private void ReloadLevel()
    {
        deathParticles.Stop();
        SceneManager.LoadScene(currentLevel);
    }

    private void LoadNextLevel()
    {
        successParticles.Stop();
        SceneManager.LoadScene(currentLevel);
    }

    private void RespondToThrustInput()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            ApplyThrust(thrustThisFrame);
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust(float thrustThisFrame)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);

        if (!audioSource.isPlaying) // keeps the audio clip from layering
        {
            audioSource.PlayOneShot(mainEngine);
        }

        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()
    {

        rigidBody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) // turn left
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) // or turn right
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

}
