namespace UI.Signals
{
    public class MenuSignals
    {
        public class Toggle
        {
            public MenuType Type { get; private set; }
            public Toggle(MenuType type)
            {
                Type = type;
            }
        }
        
        public class OpenOnTop
        {
            public MenuType Type { get; private set; }
            public OpenOnTop(MenuType type)
            {
                Type = type;
            }
        }
    }
}