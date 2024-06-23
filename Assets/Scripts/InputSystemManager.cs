using UnityEngine;

namespace Boxhead
{
    /// <summary>
    /// This class is responsible for managing the input actions of the new input system.
    /// Useful for switching off the active action map when e. g. typing in a text field to not trigger actions.
    /// </summary>
    public class InputSystemManager : MonoBehaviour
    {
        public static InputSystemManager Instance;

        public GameSceneActions GameSceneActions;

        void Awake()
        {
            if (Instance == null)
            {
                GameSceneActions = new GameSceneActions();
                Instance = this;
            }
        }

        void Start()
        {
            GameSceneActions.Enable();
        }

        void OnDisable()
        {
            DisableAllActions();
        }

        void DisableAllActions()
        {
            GameSceneActions.Disable();
        }

        public void test123()
        {
            foreach (var action in GameSceneActions.Player.Shoot.bindings)
            {
                Debug.Log(action.ToString());
                Debug.Log(action.action.ToString());
            }
        }
    }
}