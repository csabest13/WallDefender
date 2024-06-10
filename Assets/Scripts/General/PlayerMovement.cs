using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player mozgás és Space gomb esetén lévõ hangeffekt

    [SerializeField] float velocity;
    [SerializeField] float jumpDistance;
    [SerializeField] AudioClip jumpSound;
    private Rigidbody2D Rigidbody2D;
    private AudioSource jumpAudioSource;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        jumpAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        bool presskey;

        presskey = Input.GetKeyDown(KeyCode.Space);

        if (presskey)
        {
            Rigidbody2D.velocity = Vector2.up * jumpDistance * velocity;
            if (jumpAudioSource != null && jumpSound != null)
            {
                jumpAudioSource.PlayOneShot(jumpSound);
            }
        }
    }
}