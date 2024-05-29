using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThirdPerson_Cam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform Orientation;
    [SerializeField] private Transform playerMesh;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float rotationSpeed;
    public Transform combatLookAt;
    public GameObject explorationCam;
    ///public GameObject combatCam;
    public GameObject tacticalCam;

    public CameraStyle currentCam;
    public enum CameraStyle
    {
        Exploration,
        ///Combat,
        Tactical
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q)) SwitchCamStyle(CameraStyle.Exploration);
        //if (Input.GetKeyUp(KeyCode.E)) SwitchCamStyle(CameraStyle.Combat);
        if (Input.GetKeyUp(KeyCode.T)) SwitchCamStyle(CameraStyle.Tactical);
       

        Vector3 playerDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        Orientation.forward = playerDir.normalized;

        if(currentCam == CameraStyle.Exploration || currentCam == CameraStyle.Tactical)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 inputDir = Orientation.forward * verticalInput + Orientation.right * horizontalInput;
            if (inputDir != Vector3.zero)
            {
                playerMesh.forward = Vector3.Lerp(playerMesh.forward, inputDir, Time.deltaTime * rotationSpeed);
            }
        }

        //else if( currentCam == CameraStyle.Combat)
        {
            //Vector3 combatLookAt_Dir = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            //Orientation.forward = combatLookAt_Dir.normalized;
            //playerMesh.forward = combatLookAt_Dir.normalized;
        }
    }

    private void SwitchCamStyle(CameraStyle newCamPOV)
    {
        //combatCam.SetActive(false);
        tacticalCam.SetActive(false);
        explorationCam.SetActive(false);

        if (newCamPOV == CameraStyle.Exploration) explorationCam.SetActive(true);
        if (newCamPOV == CameraStyle.Tactical) tacticalCam.SetActive(true);
        //if (newCamPOV == CameraStyle.Combat) combatCam.SetActive(true);

        currentCam = newCamPOV;
    }
}
