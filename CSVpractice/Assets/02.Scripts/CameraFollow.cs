using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // 플레이어의 Transform을 저장할 변수
    public float smoothSpeed = 0.125f;  // 카메라 이동 속도
    public Vector3 offset;          // 카메라와 플레이어 사이의 거리 조정

    void LateUpdate()
    {
        if (target != null)
        {
            // 플레이어의 위치에 offset을 더해 카메라의 새로운 위치를 계산
            Vector3 desiredPosition = target.position + offset;
            // 부드러운 이동을 위해 SmoothDamp 함수를 사용하여 현재 위치에서 새로운 위치로 이동
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // 플레이어를 바라보도록 카메라 회전 설정 (선택 사항)
            // transform.LookAt(target);
        }
    }
}
