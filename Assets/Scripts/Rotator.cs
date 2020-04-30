using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public Vector3 rotateVector;
    void Update()
    {
        transform.Rotate(rotateVector * Time.deltaTime);
    }
}