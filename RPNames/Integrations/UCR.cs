using Exiled.API.Features;
using Exiled.Loader;
using System;
using System.Linq;
using System.Reflection;

namespace RPNames.Integrations
{
    internal class UCR
    {
        public Assembly Assembly => Loader.Plugins.FirstOrDefault(p => p.Name == "UncomplicatedCustomRoles")?.Assembly;

        public Type SummonedCustomRole => Assembly?.GetType("UncomplicatedCustomRoles.API.Features.SummonedCustomRole");

        public Type CustomRole => Assembly?.GetType("UncomplicatedCustomRoles.API.Features.ICustomRole");

        public MethodInfo GetSummonedCustomRole => SummonedCustomRole?.GetMethod("Get", new Type[] { typeof(Player) });

        public bool IsEnabled => GetSummonedCustomRole is not null && CustomRole is not null;

        public bool HasUcrCustomName(Player player)
        {
            if (!IsEnabled)
                return false;

            object summonedRole = GetSummonedCustomRole.Invoke(null, new object[] { player });
            if (summonedRole is null)
                return false;

            object customRole = SummonedCustomRole.GetProperty("Role")?.GetValue(summonedRole);
            if (customRole is null)
                return false;

            if (CustomRole.GetProperty("Nickname")?.GetValue(customRole) is not string displayName)
                return false;

            if (player.DisplayNickname == displayName)
                return true;

            return false;
        }
    }
}
