using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rigid;
    private AudioSource audioSource;

    [SerializeField] private float thrustPower;
    [SerializeField] private float turnSpeed;
    [SerializeField] private AudioClip mainEngine;

    [SerializeField] private ParticleSystem mainThrustParticle;
    [SerializeField] private ParticleSystem leftThrustParticle;
    [SerializeField] private ParticleSystem rightThrustParticle;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        //스페이스바를 누를 시:
        if (Input.GetKey(KeyCode.Space))
        {
            //로켓 소리, 파티클 재생
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainThrustParticle.isPlaying)
            {
                mainThrustParticle.Play();
            }

            //위로 추진
            rigid.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        }
        //스페이스바를 떼면 소리, 파티클 정지
        else
        {
            audioSource.Stop();
            mainThrustParticle.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!leftThrustParticle.isPlaying) { leftThrustParticle.Play(); }
            ApplyRotation(turnSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!rightThrustParticle.isPlaying) { rightThrustParticle.Play(); }
            ApplyRotation(-turnSpeed);
        }
        else
        {
            leftThrustParticle.Stop();
            rightThrustParticle.Stop();
        }
    }

    private void ApplyRotation(float turnDirection)
    {
        rigid.freezeRotation = true;
        transform.Rotate(Vector3.forward * turnDirection * Time.deltaTime);
        rigid.freezeRotation = false;
    }
}
