using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Very simple script used in case a Gameobject needs to stay on the same position of another Gameobject
public class TrackPosition : MonoBehaviour
{
    public Transform toTrack;

    private void Update()
    {
        gameObject.transform.position = toTrack.position;
    }
}
