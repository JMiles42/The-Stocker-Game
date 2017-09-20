using JMiles42.UnityInterfaces;
using UnityEngine;

namespace JMiles42.Components {
    public class TurnOnDepthBuffer: JMilesBehavior, IStart {
        public void Start() { Camera.main.depthTextureMode = DepthTextureMode.Depth; }
    }
}