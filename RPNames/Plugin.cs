using System;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Player = Exiled.Events.Handlers.Player;

namespace RPPNames
{
    public class Plugin : Plugin<Configs.Config, Configs.Translations>
    {
        public static Plugin Instance;
        private EventsHandler events;
        public override string Name { get; } = "RPNames";
        public override string Author { get; } = "An4r3w";
        public override string Prefix { get; } = "RPNames";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 5, 0);

        public override void OnEnabled()
        {
            base.OnEnabled();
            events = new EventsHandler();
            Instance = this;
            Player.ChangingRole += events.OnPlayerChangeRole;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            Player.ChangingRole -= events.OnPlayerChangeRole;

            events = null;
        }
    }
}

//bababooey