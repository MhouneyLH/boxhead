using UnityEngine;

namespace Boxhead.Presentation.Game.Interfaces
{
    /// <summary>
    /// Represents an object that can be launched. (e. g. bullet)
    /// </summary>
    public interface IProjectile
    {
        void Launch(Vector2 direction);
    }
}