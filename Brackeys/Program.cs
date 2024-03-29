﻿using Brackeys.Manager;
using Brackeys.States;
using System;
using Unity;

namespace Brackeys
{
    public static class Program
    {
        public static IUnityContainer UnityContainer = new UnityContainer();

        [STAThread]
        static void Main()
        {
            Register();

            using JamGame game = UnityContainer.Resolve<JamGame>();
            game.Run();
        }

        static void Register()
        {
            RegisterStates();
            RegisterManager();

            UnityContainer.RegisterSingleton<JamGame>();
        }

        static void RegisterManager()
        {
            UnityContainer.RegisterSingleton<StateManager>();
            UnityContainer.RegisterType<ParticleManager>();
            UnityContainer.RegisterType<AudioManager>();
            UnityContainer.RegisterType<ContentManager>();
            UnityContainer.RegisterType<AnimationManager>();
        }

        static void RegisterStates()
        {
            //UnityContainer.RegisterType<MenuState>(MenuState.Name);
            UnityContainer.RegisterType<GameState>(GameState.Name);
        }
    }
}
