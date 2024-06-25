using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // 플레이어 이동 속도
    public float rotationSpeed = 100f;  // 플레이어 회전 속도

    void Update()
    {
        // 이동 처리
        HandleMovement();

        // 회전 처리
        HandleRotation();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 플레이어가 바라보는 방향 기준으로 이동 방향을 계산
        Vector3 moveDirection = CalculateMoveDirection(horizontalInput, verticalInput);

        // 플레이어를 이동 방향으로 이동시킴
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    void HandleRotation()
    {
        // Q 키를 누르면 플레이어를 반시계 방향으로 회전
        if (Input.GetKey(KeyCode.Q))
        {
            RotatePlayer(-rotationSpeed * Time.deltaTime);
        }

        // E 키를 누르면 플레이어를 시계 방향으로 회전
        if (Input.GetKey(KeyCode.E))
        {
            RotatePlayer(rotationSpeed * Time.deltaTime);
        }
    }

    Vector3 CalculateMoveDirection(float horizontalInput, float verticalInput)
    {
        Transform cameraTransform = Camera.main.transform;  // 메인 카메라의 Transform 가져오기
        Vector3 forward = cameraTransform.forward;  // 카메라가 바라보는 방향
        forward.y = 0;  // y 축 성분을 0으로 만들어 수평 방향으로만 이동하도록 함
        forward.Normalize();  // 방향 벡터를 정규화하여 길이를 1로 만듦

        Vector3 right = cameraTransform.right;  // 카메라의 오른쪽 방향 (좌우 이동)

        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;  // 입력에 따라 이동 방향 계산

        return moveDirection.normalized;  // 정규화된 이동 방향 반환
    }

    void RotatePlayer(float rotateAmount)
    {
        transform.Rotate(Vector3.up, rotateAmount);
    }
}
