using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;









public class ScreenShake :Singleton<ScreenShake>
{

    public float TimeStopSpeed;
    public bool isTimeScale;

    void Start()
    {
        isTimeScale = false;
    }

    // Update is called once per frame
    void Update()
    {
        HitStop();
    }

    public void StopTime(float changeTime, int resetSpeed, float delay)
    {

        TimeStopSpeed = resetSpeed;
        Time.timeScale = delay;
        if (delay > 0)
        {
            StopCoroutine(StartTime(delay));
            StartCoroutine(StartTime(delay));

        }
        else
        {
            isTimeScale = true;
        }
        Time.timeScale = changeTime;
    }

    public void HitStop()
    {
        if (isTimeScale)
        {
            if (Time.timeScale < 1f)
            {
                Time.timeScale += TimeStopSpeed * Time.deltaTime;
            }
            else
            {
                Time.timeScale = 1f;
                isTimeScale = false;
            }
        }
    }

    IEnumerator StartTime(float _amt)
    {

        isTimeScale = true;
        yield return new WaitForSecondsRealtime(_amt);

    }

    public void CameraShake(float shakeTime, Vector2 shakeOffset, int shakeStrenge)
    {

        // 执行相机震动
        transform.DOShakePosition(shakeTime, shakeOffset, shakeStrenge, 90, false,true)
            .OnComplete(() =>
            {
                // 在震动完成后，将相机位置恢复到原来的位置
                 transform.position = new Vector3(0, 0, -10);
            });
    }
}
