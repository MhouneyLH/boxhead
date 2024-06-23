using UnityEngine;

namespace Boxhead
{
    /// <summary>
    /// This class is responsible for managing the input actions of the new input system.
    /// Useful for switching off the active action map when e. g. typing in a text field to not trigger actions.
    /// </summary>
    public class InputSystemManager : MonoBehaviour
    {
        public static GameSceneActions GameSceneActions;

        void Awake()
        {
            GameSceneActions = new GameSceneActions();
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
    }
}