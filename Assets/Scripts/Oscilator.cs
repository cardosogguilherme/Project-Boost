using UnityEngine;

public class Oscilator : MonoBehaviour
{
    private Vector3 startingPosition;

    const float tau = Mathf.PI * 2; // constant value of 6.283

    [SerializeField] private Vector3 movingVector;
    private float movingFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;   
    }

    void Update()
    {
        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(tau * cycles); // going from -1 to 1

        // recalculated to go from 0 to 1 so it's cleaner
        movingFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movingVector * movingFactor;
        transform.position = startingPosition + offset;
    }
}
