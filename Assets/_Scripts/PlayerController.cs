using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public float speed = 1.3f;          // 이동 속도
    public float gravity = -9.81f;    // 중력 값
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
    }

    void Update()
    {
        // 1. 바닥 착지 확인
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 2. WASD 입력 받기
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        // 3. 애니메이터에 이동 여부 전달 (IsMoving)
        bool isMoving = (move != Vector3.zero);
        animator.SetBool("IsMoving", isMoving);

        // 콘솔창에서 값이 바뀌는지 확인용 디버그
        Debug.Log("isMoving 값: " + isMoving);

        // 4. 수평 이동과 중력을 합쳐서 이동 계산
        Vector3 finalMove = (move * speed) + new Vector3(0, velocity.y, 0);
        controller.Move(finalMove * Time.deltaTime);

        // 5. 중력 누적
        velocity.y += gravity * Time.deltaTime;

        // 6. 이동 방향으로 캐릭터 회전
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }
    }
}