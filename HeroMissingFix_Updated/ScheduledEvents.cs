using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace HeroMissingFix_Updated
{
    internal class ScheduledEvents : CampaignBehaviorBase
	{
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, DailyPrisonerCheck);
		}

        public override void SyncData(IDataStore dataStore)
        {
        }

        private void DailyPrisonerCheck()
		{
			InformationManager.DisplayMessage(new InformationMessage("Checking for Missing Heroes..."));


			// Every day check if any Heros are alive but disabled and it belongs to a clan.
			// Pretty sure I could remove the tutorial and storymode NPCs (they have that prefixed in name) since I'm checking clan,
			// but leaving there since it doesn't hurt anything and was an edge case I hit before adding clan check.
			foreach (Hero hero in Hero.FindAll(hero => !hero.IsHumanPlayerCharacter && hero.IsAlive && hero.IsDisabled && hero.Clan != null 
						&& !hero.GetName().Contains("tutorial") && !hero.GetName().Contains("storymode")))
			{
				string clanName = hero.Clan != null ? hero.Clan.FullName.ToString() : "";

				// Log some info in chat about hero fixing to get reactivated
				InformationManager.DisplayMessage(new InformationMessage("========"));
				InformationManager.DisplayMessage(new InformationMessage($"Missing: {hero.GetName()}, {hero.ToString()}"));
				InformationManager.DisplayMessage(new InformationMessage($"Missing IsAlive: {hero.IsAlive}"));
				InformationManager.DisplayMessage(new InformationMessage($"Missing State: {hero.HeroState}"));
				InformationManager.DisplayMessage(new InformationMessage($"Missing IsPartyLeader: {hero.IsPartyLeader}, CanLead: {hero.CanLeadParty()}"));
				InformationManager.DisplayMessage(new InformationMessage($"Missing Clan: {clanName}"));
				InformationManager.DisplayMessage(new InformationMessage($"Missing Age: {hero.Age}"));
				InformationManager.DisplayMessage(new InformationMessage($"Missing LastSeen: {hero.LastSeenTime.ElapsedDaysUntilNow}"));

				// Fix hero
				hero.ChangeState(Hero.CharacterStates.Released);
			}
         }
	}
}
