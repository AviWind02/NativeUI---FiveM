using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using NativeUI.Client.Menu;
using static CitizenFX.Core.Native.API;

namespace NativeUI.Client
{
    public class ClientMain : BaseScript
    {
        private UIHandler UI = new UIHandler();

        public ClientMain()
        {
            Debug.WriteLine("Client Side Init");

        }

        [Tick]
        public Task OnTick()
        {
            Controls.ControlTick();
            UI.Title();
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.SetOption("Test", "Test", "Test");
            UI.End();

            return Task.FromResult(0);
        }
    }
}
