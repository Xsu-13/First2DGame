using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float strength = 0.1f;
    [SerializeField] bool disableVerticalParallax;
    Vector3 targetPreviousPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (!followingTarget)
            followingTarget = Camera.main.transform;
        targetPreviousPosition = followingTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        var delta = followingTarget.position - targetPreviousPosition;
        if (disableVerticalParallax)
            delta.y = 0;
        targetPreviousPosition = followingTarget.position;
        transform.position -= delta * strength;
    }
}
