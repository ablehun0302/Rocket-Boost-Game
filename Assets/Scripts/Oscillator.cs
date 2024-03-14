using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;                   //기존 위치
    [SerializeField] Vector3 movementVector;    //기존 위치에서 얼만큼 이동할지
    [SerializeField] float period = 2;  //period초당 1번 주기로 왔다갔다
    float movementFactor;               //이동한 정도

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period;  //period초당 1번 주기로 돈다
        const float tau = Mathf.PI * 2;     //360도 == 2파이 == 1타우

        float rawSinWave = Mathf.Sin(cycles * tau); //-1 부터 1까지 움직이는 싸인파

        movementFactor = (rawSinWave + 1) / 2;      // 0부터 1까지 움직이는 싸인파를 이동정도에 대입
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
