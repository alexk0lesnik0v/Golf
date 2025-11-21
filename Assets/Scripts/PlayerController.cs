using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Golf
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Stick m_stick;
        
        [SerializeField] private EventTrigger m_hitButton;

        private bool m_isDown;

        private void Start()
        {
            var entryDown = new EventTrigger.Entry();
            entryDown.eventID = EventTriggerType.PointerDown;
            
            var entryUp = new EventTrigger.Entry();
            entryUp.eventID = EventTriggerType.PointerUp;
            
            //entryUp.callback.AddListener(OnPointerUp);
        }

        private void Update()
        {
           //if (Input.GetKey(KeyCode.RightArrow))
            if (m_isDown)
            {
                m_stick.Down();
            }
            else
            {
                m_stick.Up();
            }
        }

        private void OnEnable()
        {
            
        }
        
        private void Down()
        {
            m_isDown = true;
        }

        private void Up()
        {
            m_isDown = false;
        }
    }
}