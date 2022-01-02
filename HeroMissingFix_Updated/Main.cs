using System;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace HeroMissingFix_Updated
{
    // Main inherits from MBSubMOduleBase
    public class HeroFixSubModule : MBSubModuleBase
    {
        public static readonly Color textColor = Color.FromUint(6750401U);

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            InformationManager.DisplayMessage(new InformationMessage("Successfully loaded HeroMissingFix_Updated.", textColor));
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            if (game.GameType is Campaign)
            {
                CampaignGameStarter gameInitializer = (CampaignGameStarter)gameStarterObject;
                try
                {
                    gameInitializer.AddBehavior(new ScheduledEvents());
                }
                catch (Exception ex)
                {
                    InformationManager.DisplayMessage(new InformationMessage($"Error while initialising HeroMissingFix_Updated:\n\n { ex.Message} \n\n { ex.StackTrace}", textColor));
                }
            }
        }


    }
}
