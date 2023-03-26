using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTranslateAtStartMono : MonoBehaviour
{

    public Transform m_whatToMove;
    public Vector3 m_axisAffinity = new Vector3(1, 0, 1);
    public float m_range = 1;
    public float m_scaleMin = 1;
    public float m_scaleMax = 1.6f;

    void Start()
    {
        Vector3 move = new Vector3();
        move.x = UnityEngine.Random.value * m_range * m_axisAffinity.x;
        move.y = UnityEngine.Random.value * m_range * m_axisAffinity.y;
        move.z = UnityEngine.Random.value * m_range * m_axisAffinity.z;
        m_whatToMove.localScale = new Vector3(UnityEngine.Random.Range(m_scaleMin, m_scaleMax), UnityEngine.Random.Range(m_scaleMin, m_scaleMax), UnityEngine.Random.Range(m_scaleMin, m_scaleMax));
        m_whatToMove.Translate(move, Space.World);
    }

    void Reset()
    {
        m_whatToMove = this.transform;
    }
}
