using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 2f;
    public float rotateSpeed = 180f;

    public Rigidbody2D rb;

    Vector2 pastPos;
    Vector2 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (GameManager.lost == false)
        {
            //movement and rotation
            Quaternion rot = transform.rotation;

            float z = rot.eulerAngles.z;
            z -= Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
            rot = Quaternion.Euler(0, 0, z);
            transform.rotation = rot;

            Vector3 pos = transform.position;
            Vector3 velocity = new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime, 0);
            pos += rot * velocity;
            transform.position = pos;

            currentPos = pastPos * velocity;

            Vector2 lookDir = currentPos - pastPos;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            //rb.rotation = angle;
        }
    }
}
