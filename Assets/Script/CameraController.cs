using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform follow_target;
    [SerializeField] float distance = 5f;

    //Maximum et minimum de angle sur la rotation vertical
    [SerializeField] float maxVerticalAngle = 45f;
    [SerializeField] float minVerticalAngle = -45f;

    //Gestion de la vitesse de la camera
    [SerializeField] float rotationSpeed = -2f;

    //Pour ajuster la vue de la camera sur le joueur  
    [SerializeField] Vector2 framingOffset;

    //invertion des axes 
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    float invertXVal;
    float invertYVal;

    //Pour la rotaion de la camera 
    float rotationY;
    float rotationX;

    // Start is called before the first frame update
    void Start()
    {
        //Pour faire apparaitre et disparaitre le curseur dans la fenêtre de jeu 
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Invertion de rotation 
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        //définition des angle de rotation de la camera 
        rotationX += Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += Input.GetAxis("Mouse X") * invertXVal * rotationSpeed;

        //Defintion de la rotation 
        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0.0f);

        //ajustement de la position 
        var focusPosition = follow_target.position + new Vector3(framingOffset.x, framingOffset.y);

        //Position et rotation de la camera 
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;

        //Position du joueur 
        //follow_target.rotation = targetRotation;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.E))
        {
            Cursor.visible = true;
        }
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0.0f, rotationY, 0.0f);
}
