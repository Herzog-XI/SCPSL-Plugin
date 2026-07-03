using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using CustomPlayerClasses;

namespace AntiSpawnTK
{
    public class Config : Exiled.API.Interfaces.IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public float ProtectionDuration { get; set; } = 30f;
    }

    public class PluginInstance : Plugin<Config>
    {
        public override string Name => "Anti Spawn TK";
        public override string Author => "Community";
        public override Version Version => new Version(1, 0, 1);

        private DateTime lastSpawnTime = DateTime.MinValue;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Server.RespawningTeam += OnRespawningTeam;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Server.RespawningTeam -= OnRespawningTeam;
            base.OnDisabled();
        }

        private void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            lastSpawnTime = DateTime.Now;
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null || ev.Player == null) return;
            if (ev.Attacker == ev.Player) return;
            if ((DateTime.Now - lastSpawnTime).TotalSeconds >= Config.ProtectionDuration)
                return;

            if (IsMTFRole(ev.Attacker.Role.RoleTypeCode) && IsMTFRole(ev.Player.Role.RoleTypeCode))
            {
                ev.IsAllowed = false;
            }
        }

        private bool IsMTFRole(RoleTypeCode role)
        {
            return role == RoleTypeCode.NtfCaptain ||
                   role == RoleTypeCode.NtfSergeant ||
                   role == RoleTypeCode.NtfSpecialist ||
                   role == RoleTypeCode.NtfPrivate;
        }
    }
}
