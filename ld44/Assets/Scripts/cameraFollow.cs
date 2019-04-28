using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    Transform playerTransform;
    float speed = .2f;

    public float minX = 0;
    public float minY = 0;

    public float maxX = 1;
    public float maxY = 1;

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
                (Mathf.Clamp(playerTransform.position.x, minX, maxX), 
                Mathf.Clamp(playerTransform.position.y, minY, maxY),
                -10),
            speed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
