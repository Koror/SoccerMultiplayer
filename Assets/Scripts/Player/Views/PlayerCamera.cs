using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player.Views
{
    public class PlayerCamera : NetworkBehaviour
    {
        private Camera _mainCam;
        private Vector3 _camOffset = new Vector3(0f, 3.3f, -3.75f);
        private Vector3 _camRotation = new Vector3(6f, 0f, 0f);
        
        private void Awake()
        {
            _mainCam = Camera.main;
        }
        
        public override void OnStartLocalPlayer()
        {
            if (_mainCam != null)
            {
                _mainCam.transform.SetParent(transform);
                _mainCam.transform.localPosition = _camOffset;
                _mainCam.transform.localEulerAngles = _camRotation;
            }
            else
                Debug.LogWarning("Player: Could not find a camera in scene");
        }

        public override void OnStopLocalPlayer()
        {
            if (_mainCam != null && _mainCam.transform.parent == transform)
            {
                _mainCam.transform.SetParent(null);
                SceneManager.MoveGameObjectToScene(_mainCam.gameObject, SceneManager.GetActiveScene());
            }
        }
    }
}