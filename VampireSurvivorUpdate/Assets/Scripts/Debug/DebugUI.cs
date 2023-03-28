using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

/*
 * Debug only, no need to use this script int he game. It's only use in build testing for people to test the feature and changes parameters without the need to go in the editor and start Unity
 */

namespace Debug
{
    public class DebugUI : MonoBehaviour
    {
        [Header("Other")]
        [Tooltip("The mask to hide the debug menu")]
        public Mask uiMask;
        [Tooltip("The CinemachineVirtualCamera that follow the player")]
        public CinemachineVirtualCamera cmVCam;
        [Tooltip("The PlayerMovement script on the player")]
        public PlayerMovement playerMovement;
    
        [Header("Sliders")]
        [Tooltip("The slider for the player speed")]
        public Slider speedSlider;
        [Tooltip("The slider for the X Damping of the CinemachineVirtualCamera")]
        public Slider xDampingSlider;
        [Tooltip("The slider for the Y Damping of the CinemachineVirtualCamera")]
        public Slider yDampingSlider;
        [Tooltip("The slider for camera distance (ortho lens size) of the CinemachineVirtualCamera from the player")]
        public Slider cameraDistanceSlider;
    
        [Header("Toggles")]
        [Tooltip("The toggle to link the X and Y Damping value of the CinemachineVirtualCamera")]
        public Toggle linkDamping;
        [Tooltip("The toggle to hide the debug menu")]
        public Toggle hideToggle;
    
        [Header("Texts")]
        [Tooltip("The text UI for the value of the player speed")]
        public Text speedText;
        [Tooltip("The text UI for the value of the X Damping of the CinemachineVirtualCamera")]
        public Text xDampingText;
        [Tooltip("The text UI for the value of the Y Damping of the CinemachineVirtualCamera")]
        public Text yDampingText;
        [Tooltip("The text UI for the value of the camera distance (ortho lens size) of the CinemachineVirtualCamera from the player")]
        public Text cameraDistanceText;

        private void Awake()
        {
            //Adding Listener events to every slider and toggle so that when their values are changes it call the associated function
            speedSlider.onValueChanged.AddListener(SpeedSliderChange); 
            xDampingSlider.onValueChanged.AddListener(XDampingSliderChange);
            yDampingSlider.onValueChanged.AddListener(YDampingSliderChange);
            cameraDistanceSlider.onValueChanged.AddListener(CameraDistanceSliderChange);
            linkDamping.onValueChanged.AddListener(LinkDampingToggleChange);
            hideToggle.onValueChanged.AddListener(HideDebugMenu);

            //Setting the actual values of the parameters to the sliders
            speedSlider.value = playerMovement.speed;
            xDampingSlider.value = cmVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping;
            yDampingSlider.value = cmVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping;
            cameraDistanceSlider.value = cmVCam.m_Lens.OrthographicSize;
        
            //Setting the text next to the sliders to show the actual value and setting default values to Toggles
            speedText.text = speedSlider.value.ToString();
            xDampingText.text = xDampingSlider.value.ToString();
            yDampingText.text = yDampingSlider.value.ToString();
            cameraDistanceText.text = cameraDistanceSlider.value.ToString();
            linkDamping.isOn = false;
            hideToggle.isOn = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.None; //So we're able to move the mouse to the debug menu without having it to focus on the game
        }

        /// <summary>
        /// Every times the speed slider value change it's called to apply this value to the player speed
        /// </summary>
        /// <param name="value"></param>
        private void SpeedSliderChange(float value)
        {
            speedText.text = value.ToString();
            playerMovement.speed = value;
        }
    
        /// <summary>
        /// Every times the XDamping Slider value is changed, it apply this value to the x damping of the Cinemachine Virtual Camera | Also if the link Damping toggle is on it also change the value of the YDamping Slider 
        /// </summary>
        /// <param name="value"></param>
        private void XDampingSliderChange(float value)
        {
            xDampingText.text = value.ToString();
            cmVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = value;

            if (!linkDamping.isOn) //If the LinkDamping toggle is on then make the value of the YDamping Slider equal to the X one
                return;
        
            yDampingSlider.value = value;

        }
    
        /// <summary>
        /// Every times the YDamping Slider value is changed, it apply this value to the y damping of the Cinemachine Virtual Camera
        /// </summary>
        /// <param name="value"></param>
        private void YDampingSliderChange(float value)
        {
            yDampingText.text = value.ToString();
            cmVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = value;
        }

        /// <summary>
        /// Change the Camera distance based on the slider value. Updated each times the value change.
        /// </summary>
        /// <param name="value"></param>
        private void CameraDistanceSliderChange(float value)
        {
            cameraDistanceText.text = value.ToString();
            cmVCam.m_Lens.OrthographicSize = value;
        }
    
        /// <summary>
        /// If the value of the Toggle is changed 
        /// </summary>
        /// <param name="value"></param>
        private void LinkDampingToggleChange(bool value)
        {
            if (!linkDamping.isOn)
            {
                yDampingSlider.interactable = true;
                yDampingSlider.value = xDampingSlider.value;
            }
            else
            {
                yDampingSlider.interactable = false; //Make it no more interactable so that we touch only one slider
            }
        }

        private void HideDebugMenu(bool value)
        {
            uiMask.enabled = value; //Enable the mask to hide the Debug Menu
        }
    
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
