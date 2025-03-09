using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System;
using MEC;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;

namespace RPPNames
{
    public class EventsHandler
    {
        public void OnPlayerChangeRole(ChangingRoleEventArgs ev)
        {
            switch (ev.NewRole)
            {
                default:
                    Timing.CallDelayed(1, () =>
                    {
                        if (ev.Player.GetCustomRoles().Count > 0 && CustomRole.Get((uint)ev.Player.Id).Role.GetFaction() != Faction.SCP)
                        {
                            var customRole = CustomRole.Get((uint)ev.Player.Id);
                            string randomNameRole = Plugin.Instance.Translation.HumanNames[random.Next(Plugin.Instance.Translation.HumanNames.Count)];
                            string customInfo = customRole.CustomInfo;
                            ev.Player.DisplayNickname = $"{customInfo} {randomNameRole}";
                            Log.Debug($"Custom role info for player {ev.Player.Nickname} set to {ev.Player.DisplayNickname}");
                        }
                        else
                        {
                            string randomName = Plugin.Instance.Translation.HumanNames[random.Next(Plugin.Instance.Translation.HumanNames.Count)];
                            string nicknamedisplay = Plugin.Instance.Translation.ClassTitles[ev.NewRole] + randomName;
                            if (Plugin.Instance.Config.ShowRealName && ev.Player.Role != RoleTypeId.Spectator && ev.Player.Role != RoleTypeId.Tutorial)
                            {
                                ev.Player.DisplayNickname = $"{nicknamedisplay} \n({ev.Player.Nickname})";
                                Log.Debug($"Nickname of the player {ev.Player.Nickname} changed successfully.");
                            }
                            else
                                ev.Player.DisplayNickname = nicknamedisplay;
                            Log.Debug($"Nickname of player {ev.Player.Nickname} changed successfully to {randomName}");
                        }
                    });
                    break;

                case RoleTypeId.ClassD:
                    Timing.CallDelayed(1, () =>
                    {
                        if (ev.Player.GetCustomRoles().Count > 0)
                        {
                            var customRole = CustomRole.Get((uint)ev.Player.Id);
                            string randomNameRole = Plugin.Instance.Translation.HumanNames[random.Next(Plugin.Instance.Translation.HumanNames.Count)];
                            string customInfo = customRole.CustomInfo;
                            ev.Player.DisplayNickname = customInfo + randomNameRole;
                            Log.Debug($"Custom role info for player {ev.Player.Nickname} set to {ev.Player.DisplayNickname}");
                        }
                        else
                        {
                            int num2 = random.Next(1000, 9999);
                            string nicknamedisplayy = "D-" + num2.ToString();
                            if (Plugin.Instance.Config.ShowRealName && ev.Player.Role != RoleTypeId.Spectator && ev.Player.Role != RoleTypeId.Tutorial)
                            {
                                ev.Player.DisplayNickname = $"{nicknamedisplayy} \n({ev.Player.Nickname})";
                                Log.Debug($"Nickname of the player {ev.Player.Nickname} changed successfully.");
                            }
                            else
                                ev.Player.DisplayNickname = nicknamedisplayy;
                        }
                    });
                    break;

                case RoleTypeId.Tutorial:
                case RoleTypeId.Spectator:
                    ev.Player.DisplayNickname = null;
                    Log.Debug($"{ev.Player.Nickname}'s displayed nickname changed back to it's original.");
                    break;

                case RoleTypeId.None:
                    ev.Player.DisplayNickname = null;
                    break;

            }
            if (ev.Player.IsScp)
            {
                ev.Player.DisplayNickname = null;
            }
            if (ev.Player.DisplayNickname != null && Plugin.Instance.Config.ShouldNotifyPlayer)
            {
                Timing.CallDelayed(1, () =>
                {
                    if (!Plugin.Instance.Config.ShouldBeHint)
                    {
                        ev.Player.ClearBroadcasts();
                        ev.Player.Broadcast(message: $"{Plugin.Instance.Translation.YouAre}\n{ev.Player.DisplayNickname}", duration: 5);
                    }
                    else
                    {
                        ev.Player.ShowHint($"\n\n\n\n{Plugin.Instance.Translation.YouAre} {ev.Player.DisplayNickname}", 5);
                    }
                    Log.Debug($"Message showed to player {ev.Player.Nickname}");
                });
            }
        }

        private readonly Random random = new();
    }
}