using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // 따라갈 캐릭터 (SmolFauna)

    // [중요] Z값을 양수(+)로 주어 카메라가 캐릭터 '앞쪽'에 위치하게 설정했습니다. (얼굴이 잘 보이게 됨)
    // 원하시는 구도에 따라 Y(높이)와 Z(앞뒤 거리) 값을 인스펙터 창에서 미세조정할 수 있습니다.
    public Vector3 offset = new Vector3(0, 4f, 3.5f);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // 캐릭터 기준 카메라 목표 위치 계산
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 항상 캐릭터의 얼굴 쪽을 바라보도록 시선 고정
        transform.LookAt(target.position + Vector3.up * 0.8f);
    }
}