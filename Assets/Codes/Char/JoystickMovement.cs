using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JoystickMovement : MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed=0.04f;
    [SerializeField] LayerMask layerMaskOfObstackle;


    Vector3 movement;
    bool stopMovement=true;

    PlayerAnimatorScript playerAnimatorScript;
    Rigidbody rigidbodyOfPlayer;
    LayerMask layerMask;
    Vector3 StackSpawnPoint;

    public void NewLevel()
    {
        transform.position = new Vector3(0, 1, -120);
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        continueTheMovement();
    }

    void Start()
    {
        playerAnimatorScript = GetComponent<PlayerAnimatorScript>();
        rigidbodyOfPlayer = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!stopMovement && Joystick.Instance.Direction != Vector2.zero)
        {
            movement = transform.forward * Time.deltaTime * speed;

            if (rigidbodyOfPlayer.SweepTest(transform.forward, out RaycastHit raycastHit, maxDistance))
            {
                layerMask = raycastHit.transform.gameObject.layer;
            }
            else
            {
                layerMask = 0;
            }

            if (layerMask != 6)
            {
                transform.position += movement;
            }
            
            //transform.Translate(transform.forward * 6f * Time.deltaTime);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Mathf.Atan2(Joystick.Instance.Horizontal, Joystick.Instance.Vertical) * 180 / Mathf.PI, 0), turnSpeed);
            //transform.rotation = Quaternion.Euler(0, Mathf.Atan2(Joystick.Instance.Horizontal, Joystick.Instance.Vertical) * 180 / Mathf.PI, 0);

            playerAnimatorScript.RunTrue();
        }
        else
        {
            playerAnimatorScript.RunFalse();
        }
    }

    public void stopTheMovement()
    {
        stopMovement = true;
    }
    public void continueTheMovement()
    {
        stopMovement = false;
    }
}
