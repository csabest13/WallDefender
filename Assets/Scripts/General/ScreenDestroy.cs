using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDestroy : MonoBehaviour
{
    //objektum, ami a kamera látószögét elhagyja megsemmisûl 

    SpriteRenderer spriteRenderer;
    new Collider2D collider2D;
    Camera mainCamera;
    float cameraAspect;
    float cameraOrthographicSize;
    bool isVisible = false;

    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();

        cameraAspect = mainCamera.aspect;
        cameraOrthographicSize = mainCamera.orthographicSize;

    }

    void Update()
    {
        Vector2 cameraCenter = mainCamera.transform.position;
        Vector2 cameraSize = new Vector2(cameraOrthographicSize * cameraAspect, cameraOrthographicSize);
        Bounds cameraBounds = new Bounds(cameraCenter, cameraSize * 2);

        Bounds objectBounds = collider2D.bounds;

        if (!isVisible && cameraBounds.Intersects(objectBounds))
        {
            isVisible = true;
        }

        if (isVisible && !cameraBounds.Intersects(objectBounds))
        {
            Destroy(gameObject);
        }
    }
}