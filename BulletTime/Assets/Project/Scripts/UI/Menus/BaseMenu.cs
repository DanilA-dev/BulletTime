using UnityEngine;

namespace UI
{
    public enum MenuType
    {
        None = 0,
        Menu = 1,
        Core = 2,
        Lose = 3,
        Win = 4,
        Pause = 5
    }
    
    public abstract class BaseMenu : MonoBehaviour
    {
        public abstract MenuType Type { get; }
    }
}