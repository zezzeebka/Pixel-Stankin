using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraBeahaviour : MonoBehaviour
{
    [Header("GameObject")]
    public Transform _gameObject;
    [Header("Camera position restrictions")]
    public float minY;
    public float maxY;
    public float minX;
    public float maxX;

    void Update()
    {
        UpdateCameraPosition();
    }

    // Изменяем позицию камеры на экране
  void UpdateCameraPosition()
    {
        try
        {
            transform.position = new Vector3(
                // Положение игрового объекта, за которым мы двигаемся
                Mathf.Clamp(_gameObject.position.x, minX, maxX),
                Mathf.Clamp(_gameObject.position.y, minY, maxY),
                // Положение камеры z должно оставать неизменным 
                transform.position.z // (если камеры куда-то проваливается, заменить на, например, -10)
              );
        }
        catch (Exception error)
        {
           
            Debug.LogError(error);
        }
    }
}