using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond = 15.0f;

    public float amplitudeMAX;
    public float amplitudeMIN;
    private float amplitude;

    public float frequencyMAX;
    public float frequencyMIN;
    private float frequency;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
        amplitude = Random.Range(amplitudeMAX, amplitudeMIN);
        frequency = Random.Range(frequencyMAX, frequencyMIN);
    }

    // Update is called once per frame
    void Update()
    {
        // Spin object around Y-Axis
        //transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    //public float floatStrengthMAX;
    //public float floatStrengthMIN;
    //public float randomStrength = 1f;

    //public float distanceThresholdMAX;
    //public float distanceThresholdMIN;
    //private float randomYDistanceThreshold;

    //[SerializeField] Vector3 startPos;
    //[SerializeField] Vector3 targetPos;

    //[SerializeField] bool isGoingDown = false;

    //private void Start()
    //{
    //    startPos = transform.position;
    //    RandomizeVars();
    //}

    //private void FixedUpdate()
    //{
    //    if (!isGoingDown)
    //    {
    //        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * randomStrength);

    //        if (Vector3.Distance(transform.position, targetPos) <= 0.05)
    //        {
    //            RandomizeVars();
    //            isGoingDown = true;
    //        }
    //    }
    //    else
    //    {
    //        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * randomStrength);

    //        if (Vector3.Distance(transform.position, targetPos) >= 0.05)
    //        {
    //            RandomizeVars();
    //            isGoingDown = false;
    //        }
    //    }
    //}

    //private void RandomizeVars()
    //{
    //    //randomStrength = Random.Range(floatStrengthMAX, floatStrengthMIN);
    //    randomYDistanceThreshold = Random.Range(distanceThresholdMAX, distanceThresholdMIN);
    //    randomYDistanceThreshold = Mathf.Round(randomYDistanceThreshold);

    //    if (isGoingDown)
    //    {
    //        targetPos = new Vector3(startPos.x, startPos.y - randomYDistanceThreshold, startPos.z);
    //    }
    //    else
    //    {
    //        targetPos = new Vector3(startPos.x, startPos.y + randomYDistanceThreshold, startPos.z);
    //    }
    //}


    //private void FixedUpdate()
    //{
    //    if (!isGoingDown)
    //    {
    //        transform.Translate(Vector3.up * randomStrength * Time.deltaTime, Space.World);

    //        if (transform.position.y > startPosition.y + randomDistanceThreshold)
    //        {
    //            isGoingDown = true;
    //            RandomizeVars();
    //        }
    //    }
    //    else
    //    {
    //        transform.Translate(Vector3.down * randomStrength * Time.deltaTime, Space.World);

    //        if (transform.position.y < startPosition.y - randomDistanceThreshold)
    //        {
    //            isGoingDown = false;
    //            RandomizeVars();
    //        }
    //    }
    //}

    //private void RandomizeVars()
    //{
    //    randomStrength = Random.Range(floatStrengthMAX, floatStrengthMIN);
    //    randomDistanceThreshold = Random.Range(distanceThresholdMAX, distanceThresholdMIN);
    //}
}
