using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    [SerializeField] private Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] private float period = 2f;

    // todo remove from inspector later
    [Range (0,1)] [SerializeField] private float movementFactor; // 0 for not moved, 1 for fully moved.

    private Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        NoNaN();

        startingPos = transform.position;
    }

    private void NoNaN()
    {
        // Since two floats can't be compared, Epsilon is the smallest float available.
        if (period <= Mathf.Epsilon) { period = 0.1f; } 
    }

    // Update is called once per frame
    void Update()
    {
        // todo protect against period is zero
        float cycles = Time.time / period; // grows continually from 0

        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
