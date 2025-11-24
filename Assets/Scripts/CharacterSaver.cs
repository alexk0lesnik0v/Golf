using UnityEngine;

namespace Golf
{
    public class CharacterSaver : MonoBehaviour
    {
        public string m_characterName;
        public int m_characterID;

        public void saveCharacter()
        {
            PlayerPrefs.SetInt(m_characterName, m_characterID);
        }
    }
}