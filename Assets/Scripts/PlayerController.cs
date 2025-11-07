using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FreeCamera m_camera;
    [SerializeField] private GameObject m_uiPanel;
    [SerializeField] private CloudController m_cloudController;
    void Update()
    {

        if (m_uiPanel.activeSelf)
        {
            return;
        }

        m_camera.Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_cloudController.MoveNext();
        }
    }
}
