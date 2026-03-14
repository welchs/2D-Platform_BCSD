using System.Collections;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] float duration = 2f;

    [SerializeField] float xMoveAmount = 0.15f;
    [SerializeField] float xMoveSpeed = 10f;

    [SerializeField] float xScaleAmount = 0.2f;
    [SerializeField] float scaleSpeed = 12f;

    [SerializeField] AudioClip loopClip;
    [SerializeField] AudioSource audioSource;

    Vector3 originPos;
    Vector3 originScale;

    Coroutine laserRoutine;

    void Awake()
    {
        originScale = transform.localScale;
    }

    public void Fire( Vector3 startPos)
    {
        gameObject.SetActive(true);
        transform.position = startPos;

        originPos = startPos;

        if (laserRoutine != null )
        {
            StopCoroutine( laserRoutine );
        }

        audioSource.clip = loopClip;
        audioSource.loop = true;
        audioSource.volume = Random.Range( 0.85f, 1.0f );
        audioSource.Play();

        laserRoutine = StartCoroutine(LaserRoutine());
    }

    IEnumerator LaserRoutine()
    {
        float timer = 0f;

        while ( timer < duration)
        {
            timer += Time.deltaTime;

            //x위치의 흔들림
            float xOffset = Mathf.Sin(Time.time * xMoveSpeed) * xMoveAmount;
            transform.position = originPos + new Vector3(xOffset, 0, 0);

            //x 스케일 요동치기
            float xScale = originScale.x + Mathf.Sin(Time.time * scaleSpeed) * xScaleAmount;
            transform.localScale = new Vector3(xScale, originScale.y, originScale.z);
            audioSource.pitch = 1f + Mathf.Sin(Time.time * 2f) * 0.03f;

            yield return null;
        } 

        transform.localScale = originScale;
        gameObject.SetActive(false);
    }
}
