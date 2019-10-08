namespace CyberOdyssey.Systems
{
    using UnityEngine;
    using Unity.Collections;
    using Unity.Entities;
    using CyberOdyssey.Components;
    using CyberOdyssey.Helpers;

    public sealed class PlayerMovementSystem : ComponentSystem
    {
        private struct Data
        {
            public readonly int Length;

            [ReadOnly]
            public ComponentArray<Rigidbody2D> Rigidbodies;

            [ReadOnly]
            public ComponentDataArray<UserInput> UserInputs;
        }

        [Inject]
        private Data _data;

        protected override void OnUpdate()
        {
            for (int i = 0; i < _data.Length; i++)
            {
                var delta = new Vector2(_data.UserInputs[i].Horizontal, _data.UserInputs[i].Vertical);
                _data.Rigidbodies[i].MovePosition(delta * Constants.Movement.Scale + _data.Rigidbodies[i].position);
            }
        }
    }
}
