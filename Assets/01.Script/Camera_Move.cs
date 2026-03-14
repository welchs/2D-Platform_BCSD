using UnityEditor;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothing = 0.2f;
    [SerializeField] Vector2 mincameraboundary;
    [SerializeField] Vector2 maxcameraboundary;

    [SerializeField] bool isBossFightActive = false; // 현재 보스전 중인지 여부를 나타내는 플래그
    [SerializeField] float bossFightOrthographicSize = 10f; // 보스전 중 카메라 크기 (Orthographic Size)
    [SerializeField] Vector3 bossFightFixedPosition = new Vector3(0, 0, -10f); // 보스전 중 카메라가 고정될 위치
    [SerializeField] float cameraTransitionSpeed = 1f;

    private Camera mainCamera;
    private float originalOrthographicSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mainCamera = GetComponent<Camera>();

        if (mainCamera != null)
        {
            originalOrthographicSize = mainCamera.orthographicSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBossFightActive)
        {
            if (mainCamera != null)
            {
                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, bossFightOrthographicSize, Time.deltaTime * cameraTransitionSpeed);
            }
            transform.position = Vector3.Lerp(transform.position, bossFightFixedPosition, Time.deltaTime * cameraTransitionSpeed);
        }
        else // 보스전이 아니라면? 원래 플레이어 따라가는 로직을 그대로 사용.
        {
            // 카메라의 Orthographic Size를 원래 크기로 부드럽게 되돌리기.
            if (mainCamera != null)
            {
                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originalOrthographicSize, Time.deltaTime * cameraTransitionSpeed);
            }

            Vector3 targetPos = new Vector3(player.position.x, player.position.y + 3, transform.position.z);


            //Clamp로 최소 최대 범위를 설정해놓기.
            targetPos.x = Mathf.Clamp(targetPos.x, mincameraboundary.x, maxcameraboundary.x);
            targetPos.y = Mathf.Clamp(targetPos.y, mincameraboundary.y, maxcameraboundary.y);

            // Lerp을 이용해서 움직임을 부드럽게 작동시킨다.
            //                                 ( 현재 위치, 카메라가 이동할 방향, 움직임의 부드러움 수치)
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);

        }
    }
    public void StartBossFightCamera(Vector3 fixedPos, float bossSize)
    {
        isBossFightActive = true; // 보스전 시작 플래그 ON!
        bossFightFixedPosition = new Vector3(fixedPos.x, fixedPos.y, transform.position.z); // 보스전 고정 위치 설정 (Z축은 유지)
        bossFightOrthographicSize = bossSize; // 보스전 카메라 크기 설정
    }

    // 외부에서 보스전 종료를 알릴 때 호출할 함수
    public void EndBossFightCamera()
    {
        isBossFightActive = false; // 보스전 종료 플래그 OFF!
    }
}
