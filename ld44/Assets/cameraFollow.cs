using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    Transform playerTransform;
    float speed = .2f;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<playerController>().transform;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position, 
            new Vector3
                (Mathf.Clamp(playerTransform.position.x, -1.1f, 1.1f), 
                Mathf.Clamp(playerTransform.position.y,- 3, 8),
                -10),
            speed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
