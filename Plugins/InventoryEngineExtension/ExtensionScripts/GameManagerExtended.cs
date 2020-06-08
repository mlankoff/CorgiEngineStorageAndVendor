using MoreMountains.Feedbacks;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.CorgiEngine
{
    public class GameManagerExtended : GameManager
	{

		public override void Pause(PauseMethods pauseMethod = PauseMethods.PauseMenu){}

		public override void UnPause(PauseMethods pauseMethod = PauseMethods.PauseMenu){}

		/// <summary>
		/// Pauses the game
		/// </summary>
		public virtual void PauseGame()
		{
			if (_inventoryOpen)
			{
				return;
			}
			MMTimeScaleEvent.Trigger(MMTimeScaleMethods.For, 0f, 0f, false, 0f, true);
			Instance.Paused = true;
			if (GUIManager.Instance != null)
			{
				GUIManager.Instance.SetPause(true);
				_pauseMenuOpen = true;
				SetActiveInventoryInputManager(false);
			}
			LevelManager.Instance.ToggleCharacterPause();
		}

		/// <summary>
		/// Pause game when in inventory
		/// </summary>
		public virtual void PauseGameInventory()
		{
			//MMTimeScaleEvent.Trigger(MMTimeScaleMethods.For, 0f, 0f, false, 0f, true);
			Instance.Paused = true;
			_inventoryOpen = true;
			LevelManager.Instance.Players[0].CharacterAnimator.speed = 0;
			LevelManager.Instance.Players[0].Freeze();
			LevelManager.Instance.ToggleCharacterPause();
		}

		/// <summary>
		/// Unpauses the game
		/// </summary>
		public virtual void UnPauseGame()
		{
			MMTimeScaleEvent.Trigger(MMTimeScaleMethods.Unfreeze, 1f, 0f, false, 0f, false);
			Instance.Paused = false;
			if (GUIManager.Instance != null)
			{
				GUIManager.Instance.SetPause(false);
				_pauseMenuOpen = false;
				SetActiveInventoryInputManager(true);
			}
			LevelManager.Instance.ToggleCharacterPause();
		}

		/// <summary>
		/// Unpauses the game when in inventory
		/// </summary>
		public virtual void UnPauseGameInventory()
		{
			//MMTimeScaleEvent.Trigger(MMTimeScaleMethods.Unfreeze, 1f, 0f, false, 0f, false);
			Instance.Paused = false;
			_inventoryOpen = false;
			LevelManager.Instance.ToggleCharacterPause();
			LevelManager.Instance.Players[0].UnFreeze();
			LevelManager.Instance.Players[0].CharacterAnimator.speed = 1;
		}

		/// <summary>
		/// Catches MMGameEvents and acts on them, playing the corresponding sounds
		/// </summary>
		/// <param name="gameEvent">MMGameEvent event.</param>
		public override void OnMMEvent(MMGameEvent gameEvent)
		{
			switch (gameEvent.EventName)
			{
				case "inventoryOpens":
					PauseGameInventory();
					break;

				case "inventoryCloses":
					UnPauseGameInventory();
					break;
			}
		}

		/// <summary>
		/// Catches CorgiEngineEvents and acts on them, playing the corresponding sounds
		/// </summary>
		/// <param name="engineEvent">CorgiEngineEvent event.</param>
		public override void OnMMEvent(CorgiEngineEvent engineEvent)
		{
			switch (engineEvent.EventType)
			{
				case CorgiEngineEventTypes.TogglePause:
					if (Paused)
					{
						CorgiEngineEvent.Trigger(CorgiEngineEventTypes.UnPause);
					}
					else
					{
						CorgiEngineEvent.Trigger(CorgiEngineEventTypes.Pause);
					}
					break;

				case CorgiEngineEventTypes.Pause:
					PauseGame();
					break;

				case CorgiEngineEventTypes.UnPause:
					UnPauseGame();
					break;
			}
		}

	}

}
