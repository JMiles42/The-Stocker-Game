using System.Collections.Generic;
using JMiles42.ScriptableObjects;
using UnityEngine;

namespace JMiles42.Systems.InputManager
{
    [CreateAssetMenu(fileName = "Input Axis Setup", menuName = "JMiles42/Input/Axis Setup", order = 0)]
    public class InputAxisSetupSO: JMilesScriptableObject
    {
        public List<InputAxis> inputsToUse =
                new List<InputAxis> {PlayerInputValues.Horizontal, PlayerInputValues.Vertical, "Mouse Scroll", PlayerInputValues.Jump, "Fire1"};
    }
}