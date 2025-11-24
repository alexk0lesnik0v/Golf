using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class CharacterMenuState : MonoBehaviour
    {
        public List<GameObject> m_characters;
        public Vector3 m_characterPosition;
        
        private int m_characterID;

        private void Awake()
        {
            m_characterID = PlayerPrefs.GetInt("character");
            Instantiate(m_characters[m_characterID], m_characterPosition, Quaternion.identity);
        }
    }
}