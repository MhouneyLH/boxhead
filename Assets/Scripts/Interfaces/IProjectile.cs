using UnityEngine;

namespace Boxhead.Interfaces
{
    /// <summary>
    /// Represents an object that can be launched. (e. g. bullet)
    /// </summary>
    public interface IProjectile
    {
        void Launch(Vector3 direction);
    }
}