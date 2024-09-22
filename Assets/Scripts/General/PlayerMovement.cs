using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Player mozgás és Space gomb esetén lévõ hangeffekt

    [SerializeField] float velocity;
    [SerializeField] float jumpDistance;
    [SerializeField] AudioClip jumpSound;
    private Rigidbody2D rb2D;
    private AudioSource jumpAudioSource;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        jumpAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        bool pressKey = Input.GetKeyDown(KeyCode.Space) || TouchInputDetected();

        if (pressKey)
        {
            rb2D.velocity = Vector2.up * jumpDistance * velocity;
            if (jumpAudioSource != null && jumpSound != null)
            {
                jumpAudioSource.PlayOneShot(jumpSound);
            }
        }
    }

    private bool TouchInputDetected()
    {
        if (Touchscreen.current == null) return false;

        foreach (var touch in Touchscreen.current.touches)
        {
            if (touch.press.wasPressedThisFrame)
            {
                return true;
            }
        }

        return false;
    }

}