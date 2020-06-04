using UnityEngine;

public interface IControllable
{
    void Move();
    void Stop();
    Transform GetTransform { get; }
}
