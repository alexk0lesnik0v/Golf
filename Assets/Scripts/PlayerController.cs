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
            
            // Right
            var entryRight = new EventTrigger.Entry();
            entryRight.eventID = EventTriggerType.PointerDown;
            
            var entryLeft = new EventTrigger.Entry();
            entryLeft.eventID = EventTriggerType.PointerUp;
            
            entryUp.callback.AddListener(OnPointerUp);
            entryDown.callback.AddListener(OnPointerDown);
            
            m_RightButton.triggers.Add(entryDown);
            m_LeftButton.triggers.Add(entryUp);
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
        }

        private void Down()
        {
            m_isDown = true;
            m_isRight = true;
            m_isLeft = false;
        }

        private void Up()
        {
            m_isDown = false;
            m_isRight = false;
            m_isLeft = true;
        }

        private void OnPointerDown(BaseEventData arg0) => Down();

        private void OnPointerUp(BaseEventData arg0) => Up();
    }
}