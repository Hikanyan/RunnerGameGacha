using UnityEngine;
namespace Hikanyan.Runner.Player
{
    public interface IMovable
    {
        void Move(Vector3 direction, float speed);
        void Rotate(Vector3 axis, float angle);
    }
}