using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Golf
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Character m_character;
        
        [SerializeField] private Stick m_stick;

        [SerializeField] private EventTrigger m_hitButton;
        
        [SerializeField] private EventTrigger m_RightButton;
        
        [SerializeField] private EventTrigger m_LeftButton;
        
        private bool m_isDown;
        
        private bool m_isRight;
        
        private bool m_isLeft;

        private bool m_isStop;

        private void Start()
        {
            var entryDown = new EventTrigger.Entry();
            entryDown.eventID = EventTriggerType.PointerDown;
            
            var entryUp = new EventTrigger.Entry();
            entryUp.eventID = EventTriggerType.PointerUp;
            
            entryUp.callback.AddListener(OnPointerUp);
            entryDown.callback.AddListener(OnPointerDown);
            
            m_hitButton.triggers.Add(entryDown);
            m_hitButton.triggers.Add(entryUp);
            
            /* Right
            var entryLeft = new EventTrigger.Entry();
            entryLeft.eventID = EventTriggerType.PointerDown;
            
            var entryRight = new EventTrigger.Entry();
            entryRight.eventID = EventTriggerType.PointerUp;
            
            entryRight.callback.AddListener(OnPointerUp);
            entryLeft.callback.AddListener(OnPointerDown);
            
            m_LeftButton.triggers.Add(entryDown);
            m_RightButton.triggers.Add(entryUp);
            */
            
            // Right
            var entryRight = new EventTrigger.Entry();
            entryRight.eventID = EventTriggerType.PointerDown;
            
            var entryStop = new EventTrigger.Entry();
            entryStop.eventID = EventTriggerType.PointerUp;
            
            entryRight.callback.AddListener(OnPointerRight);
            entryStop.callback.AddListener(OnStop);
            
            m_RightButton.triggers.Add(entryRight);
            m_RightButton.triggers.Add(entryStop);
            
            
            // Left
            var entryLeft = new EventTrigger.Entry();
            entryLeft.eventID = EventTriggerType.PointerDown;
            
            entryLeft.callback.AddListener(OnPointerLeft);
            
            m_LeftButton.triggers.Add(entryLeft);
            m_LeftButton.triggers.Add(entryStop);
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

            if (m_isRight)
            {
                m_character.ToRight();
            }
            else if (m_isLeft)
            {
                m_character.ToLeft();
            }
            else if (m_isStop)
            {
                m_character.ToStop();
            }
        }

        private void Down()
        {
            m_isDown = true;
        }

        private void Up()
        {
            m_isDown = false;
        }
        
        private void Left()
        {
           m_isLeft = true;
           m_isRight = false;
           m_isStop =  false;
        }

        private void Right()
        {
            m_isLeft = false;
            m_isRight = true;
            m_isStop =  false;
        }
        
        private void Stop()
        {
            m_isLeft = false;
            m_isRight = false;
            m_isStop =  true;
        }

        private void OnPointerDown(BaseEventData arg0) => Down();

        private void OnPointerUp(BaseEventData arg0) => Up();
        
        private void OnPointerLeft(BaseEventData arg0)
        {
            Left();
        }

        private void OnPointerRight(BaseEventData arg0)
        {
            Right();
        }
        
        private void OnStop(BaseEventData arg0)
        {
            Stop();
        }
    }
}