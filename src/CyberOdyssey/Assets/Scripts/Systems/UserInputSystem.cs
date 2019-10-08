﻿namespace CyberOdyssey.Systems
{
    using UnityEngine;
    using Unity.Entities;
    using CyberOdyssey.Components;
    using CyberOdyssey.Helpers;

    public sealed class UserInputSystem : ComponentSystem
    {
        private struct Data
        {
            public readonly int Length;
            public ComponentDataArray<UserInput> UserInputs;
        }

        [Inject]
        private Data _data;

        protected override void OnUpdate()
        {   
            float horizontalInput = Input.GetAxis(Constants.Axis.Horizontal);
            float verticalInput = Input.GetAxis(Constants.Axis.Vertical);

            for (int i = 0; i < _data.Length; i++)
            {
                _data.UserInputs[i] = new UserInput 
                { 
                    Horizontal = horizontalInput, 
                    Vertical = verticalInput 
                };
            }
        }
    }
}
