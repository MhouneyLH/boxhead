using UnityEngine;

namespace Boxhead.Presentation.InputSystem
{
    /// <summary>
    /// This class is responsible for managing the input actions of the new input system.
    /// Useful for switching off the active action map when e. g. typing in a text field to not trigger actions.
    /// </summary>
    public class InputSystemManager : MonoBehaviour
    {
        public static InputSystemManager Instance;
        public GameSceneActions GameSceneActions { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                GameSceneActions = new GameSceneActions();
                Instance = this;
            }
        }

        private void Start() => GameSceneActions.Enable();
        private void OnDisable() => DisableAllActions();
        private void DisableAllActions() => GameSceneActions.Disable();
    }
}