using UnityEngine;

namespace Entity
{
    public interface IPositioning
    {
        Transform CurrentTransform { get; }
    }
}