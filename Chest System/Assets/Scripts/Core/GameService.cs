using ChestSystem.Chest;
using ChestSystem.Events;
using ChestSystem.Player;
using ChestSystem.Sound;
using ChestSystem.UI;
using ChestSystem.UI.PopUp;
using ChestSystem.UI.Slot;
using UnityEngine;

namespace ChestSystem.Core
{
    public class GameService : MonoBehaviour
    {
        private EventService eventService;
        private SoundService soundService;
        private ChestService chestService;
        private PlayerService playerService;
        private CommandInvoker commandInvoker;

        [SerializeField] private UIService uiService;

        [Header("Chest Data")]
        [SerializeField] private ChestSO chestSO;
        [SerializeField] private ChestView chestPrefab;

        [Header("Audio Data")]
        [SerializeField] private SoundClipsSO soundClips;
        [SerializeField] private AudioSource audioSourceBGM;
        [SerializeField] private AudioSource audioSourceSFX;

        private void Start()
        {
            InitializeServices();
            InitializeData();
        }

        private void InitializeData()
        {
            commandInvoker = new CommandInvoker(eventService);
            GetPopUpService().SetCommandInvoker(commandInvoker);
        }

        private void InitializeServices()
        {
            eventService = new EventService();

            uiService.CreateSlotService(eventService);
            playerService = new PlayerService(3, 3, eventService);
            chestService = new ChestService(chestSO, chestPrefab, this);
            soundService = new SoundService(audioSourceBGM, audioSourceSFX, soundClips);

            uiService.InitializeServices(this);
        }

        private void Update()
        {
            chestService.Update();
        }

        private void OnDisable()
        {
            playerService.RemoveEventListeners();
            commandInvoker.RemoveEventListeners();
        }

        //Chest Unlock Limit Logic
        public bool CanUnlockChest() => chestService.CanUnlockChest();
        public void SetIsChestUnlocking(bool value) => chestService.SetIsChestUnlocking(value);

        //Service Getters
        public SoundService GetSoundService() => soundService;
        public ChestService GetChestService() => chestService;
        public EventService GetEventService() => eventService;
        public PlayerService GetPlayerService() => playerService;
        public SlotService GetSlotService() => uiService.GetSlotService();
        public PopUpService GetPopUpService() => uiService.GetPopUpService();
    }
}
