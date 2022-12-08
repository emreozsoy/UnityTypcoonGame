using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyText : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    CanvasGroup m_canvasgroup;
    float m_Speed;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_canvasgroup = GetComponent<CanvasGroup>();
        m_Speed = 0.5f;

    }
    private void Update()
    {
        m_Rigidbody.velocity = transform.up * m_Speed;
        m_canvasgroup.alpha -= 0.001f;

    }
}
