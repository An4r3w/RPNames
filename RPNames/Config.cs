using Exiled.API.Interfaces;
using System.ComponentModel;

namespace RPPNames.Configs
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public bool ShowRealName { get; set; } = false;

        [Description("Should the plugin let the player know their name? (With a broadcast, if ShouldBeHint is set to false)")]
        public bool ShouldNotifyPlayer { get; set; } = true;

        [Description("Should the notification be a hint instead of a broadcast?")]
        public bool ShouldBeHint { get; set; } = false;
    }
}