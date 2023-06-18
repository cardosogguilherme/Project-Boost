using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float verticalThrust = 1000f;
    [SerializeField] private float rotationThrust = 100f;
    [SerializeField] private AudioClip mainEngineAudio;

    [SerializeField] private ParticleSystem thrustParticles;
    [SerializeField] private ParticleSystem leftThrustParticles;
    [SerializeField] private ParticleSystem rightThrustParticles;

    private Rigidbody rb;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        var frameIdptRotation = rotationThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (!leftThrustParticles.isPlaying) leftThrustParticles.Play();
            rightThrustParticles.Stop();
            
            ApplyRotation(frameIdptRotation);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            leftThrustParticles.Stop();
            if (!rightThrustParticles.isPlaying) rightThrustParticles.Play();
            
            ApplyRotation(-frameIdptRotation);
        } else
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing roation so we can manually rotate

        transform.Rotate(Vector3.forward * rotationThisFrame);

        rb.freezeRotation = false;
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!thrustParticles.isPlaying) thrustParticles.Play();

            rb.AddRelativeForce(Vector3.up * Time.deltaTime * verticalThrust);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineAudio);
            }
        } else {
            audioSource.Stop();
            thrustParticles.Stop();
        }
    }
}
