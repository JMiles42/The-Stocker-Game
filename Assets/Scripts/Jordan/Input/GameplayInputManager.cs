using JMiles42.Generics;
using JMiles42.Systems.InputManager;
using JMiles42.UnityInterfaces;

public class GameplayInputManager: Singleton<GameplayInputManager>, IEventListening
{
    public InputAxis PrimaryClick = "Fire1";
    public InputAxis SecondaryClick = "Fire2";

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }
}