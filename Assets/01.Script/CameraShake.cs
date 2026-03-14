using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        originPos = transform.localPosition;
    }

    public void Shake(float power, float duration)
    {
        originPos = transform.localPosition; // ? 지금 위치 저장
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(power, duration));
    }

    IEnumerator ShakeCoroutine(float power, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            transform.localPosition = originPos + (Vector3)Random.insideUnitCircle * power;
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localPosition = originPos;
    }
}
