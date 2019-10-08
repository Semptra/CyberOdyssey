namespace CyberOdyssey.Components
{
    using System;
    using Unity.Entities;
    using UnityEngine;

    [Serializable]
    public struct UserInput : IComponentData
    {
        public float Horizontal;
        public float Vertical;
    }

    public class UserInputComponent : ComponentDataProxy<UserInput> {}
}
