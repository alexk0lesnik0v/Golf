using System;
using UnityEngine;

namespace Golf
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Stick m_stick;
        private void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                m_stick.Down();
            }
            else
            {
                m_stick.Up();
            }
        }
    }
}