using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;

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
            // Reset the protection window every time a wave spawns
            lastSpawnTime = DateTime.Now;
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null || ev.Player == null) return;

            // EXCEPTION: If the player hurts themselves (grenades, etc.), ALLOW it
            if (ev.Attacker == ev.Player) return;

            // Check if we are within the protection window
            if ((DateTime.Now - lastSpawnTime).TotalSeconds >= Config.ProtectionDuration)
                return; // Protection expired, allow all damage

            // Block MTF-to-MTF teamkills only
            // MTF team includes: NtfCaptain, NtfSergeant, NtfSpecialist, NtfPrivate
            if (IsMTFRole(ev.Attacker.Role.RoleTypeCode) && IsMTFRole(ev.Player.Role.RoleTypeCode))
            {
                ev.IsAllowed = false; // Block MTF-to-MTF damage
            }
        }

        /// <summary>
        /// Check if a role belongs to the MTF faction
        /// </summary>
        private bool IsMTFRole(RoleTypeCode role)
        {
            return role == RoleTypeCode.NtfCaptain ||
                   role == RoleTypeCode.NtfSergeant ||
                   role == RoleTypeCode.NtfSpecialist ||
                   role == RoleTypeCode.NtfPrivate;
        }
    }
}

Please confirm you want Copilot to push 3 files to branch main in Herzog-XI/SCPSL-Plugin.

 Herzog-XI accepted the action
I don't have permission to push directly to your repo. But don't worry! You can do it easily through GitHub's web interface:

Here's what to do (Super Easy):
File 1: AntiSpawnTK.csproj
Go to https://github.com/Herzog-XI/SCPSL-Plugin
Click "Add file" → "Create new file"
Type this in the filename box: AntiSpawnTK/AntiSpawnTK.csproj
Copy and paste this code:
XML
<?xml version="1.0" encoding="utf-8"?>
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
    <AssemblyName>AntiSpawnTK</AssemblyName>
    <RootNamespace>AntiSpawnTK</RootNamespace>
    <Version>1.0.1</Version>
    <Authors>Community</Authors>
    <Description>Anti Spawn Team Kill plugin for SCP: Secret Laboratory</Description>
    <OutputPath>bin\$(Configuration)\</OutputPath>
  </PropertyGroup>

  <!-- NuGet Package References (Dependencies) -->
  <ItemGroup>
    <PackageReference Include="EXILED" Version="8.10.1" />
  </ItemGroup>

</Project>
Click "Commit changes"
File 2: build.yml
Go to https://github.com/Herzog-XI/SCPSL-Plugin
Click "Add file" → "Create new file"
Type this in the filename box: .github/workflows/build.yml
Copy and paste this code:
YAML
name: Build and Release

on:
  push:
    branches: [ main ]
  workflow_dispatch:

jobs:
  build:
    runs-on: windows-latest

    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '4.8'
    
    - name: Restore dependencies
      run: dotnet restore AntiSpawnTK/AntiSpawnTK.csproj
    
    - name: Build
      run: dotnet build AntiSpawnTK/AntiSpawnTK.csproj --configuration Release --no-restore
    
    - name: Create Release
      uses: actions/create-release@v1
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
      with:
        tag_name: v${{ github.run_number }}
        release_name: Release v${{ github.run_number }}
        body: |
          Automated build from commit ${{ github.sha }}
          
          Download the DLL below and place it in your plugins folder.
        draft: false
        prerelease: false
    
    - name: Upload DLL to Release
      uses: actions/upload-release-asset@v1
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
      with:
        upload_url: ${{ steps.create_release.outputs.upload_url }}
        asset_path: ./AntiSpawnTK/bin/Release/AntiSpawnTK.dll
        asset_name: AntiSpawnTK.dll
        asset_content_type: application/octet-stream
Click "Commit changes"
File 3: AntiSpawnTK.cs
Go to https://github.com/Herzog-XI/SCPSL-Plugin
Click "Add file" → "Create new file"
Type this in the filename box: AntiSpawnTK/AntiSpawnTK.cs
Copy and paste this code:
C#
using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;

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
            // Reset the protection window every time a wave spawns
            lastSpawnTime = DateTime.Now;
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null || ev.Player == null) return;

            // EXCEPTION: If the player hurts themselves (grenades, etc.), ALLOW it
            if (ev.Attacker == ev.Player) return;

            // Check if we are within the protection window
            if ((DateTime.Now - lastSpawnTime).TotalSeconds >= Config.ProtectionDuration)
                return; // Protection expired, allow all damage

            // Block MTF-to-MTF teamkills only
            // MTF team includes: NtfCaptain, NtfSergeant, NtfSpecialist, NtfPrivate
            if (IsMTFRole(ev.Attacker.Role.RoleTypeCode) && IsMTFRole(ev.Player.Role.RoleTypeCode))
            {
                ev.IsAllowed = false; // Block MTF-to-MTF damage
            }
        }

        /// <summary>
        /// Check if a role belongs to the MTF faction
        /// </summary>
        private bool IsMTFRole(RoleTypeCode role)
        {
            return role == RoleTypeCode.NtfCaptain ||
                   role == RoleTypeCode.NtfSergeant ||
                   role == RoleTypeCode.NtfSpecialist ||
                   role == RoleTypeCode.NtfPrivate;
        }
    }
}
