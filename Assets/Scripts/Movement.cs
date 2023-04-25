using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float verticalThrust = 1000f;
    [SerializeField] private float rotationThrust = 100f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        var deltaTime = Time.deltaTime;
        var frameIdptRotation = rotationThrust * deltaTime;
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(frameIdptRotation);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-frameIdptRotation);
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
        var deltaTime = Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * deltaTime * verticalThrust);
        }
    }
}
