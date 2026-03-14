using System.Collections;
using UnityEngine;

public class lightning : MonoBehaviour
{
    [SerializeField] GameObject lightningPrefab;

    [SerializeField] Vector2 center; // 중심 좌표
    [SerializeField] Vector2 range; //범위 (x,y)
    
    [SerializeField] float duration = 5f; // 스킬 지속 시간
    [SerializeField] float minDelay = 0.2f; // 최소 간격
    [SerializeField] float maxDealy = 0.6f; // 최대 간격

    Coroutine lightningCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartLightning();
        }
    }

    public void StartLightning()
    {
        if (lightningCoroutine != null)
            StopCoroutine(lightningCoroutine);

        //lightningCoroutine = StartCoroutine(LightningRoutine());
    }

    //IEnumerator LightningRoutine()
    //{

    //}
}
