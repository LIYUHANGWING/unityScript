using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


//新的分支
public class PlayerControl : MonoBehaviour
{
    private CharacterController player;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 10.0f;
    private float jumpHeight = 0.5f;
    private float gravityValue = -201f;
    private NavMeshAgent playerAgent;
    void Start()
    {
        player = GetComponent<CharacterController>();
        playerAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        groundedPlayer = player.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // 获取水平和垂直轴的输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 创建一个方向向量
        Vector3 move = new Vector3(horizontal, 0, vertical);

        // 移动角色

        //转向
        if (move != Vector3.zero)
        {
         transform.rotation= Quaternion.LookRotation(move);
        player.Move(move * Time.deltaTime * playerSpeed);
        }
        

        // 跳跃逻辑
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // 应用重力
        playerVelocity.y += gravityValue * Time.deltaTime;
        player.Move(playerVelocity * Time.deltaTime);
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            bool isCollide = Physics.Raycast(ray, out hit);
            if (isCollide)
            {
                if (hit.collider.tag == "Ground")
                {
                    playerAgent.stoppingDistance = 0;
                    playerAgent.SetDestination(hit.point);
                }
                else if (hit.collider.tag == "Interactable")
                {
                    hit.collider.GetComponent<InteractableObject>().OnClick(playerAgent);
                }

            }
        }


    }
}
