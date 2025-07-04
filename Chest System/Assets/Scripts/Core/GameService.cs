using ChestSystem.Chest;
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
        public static GameService Instance;

        private SoundService soundService;
        private ChestService chestService;
        private PlayerService playerService;
        [SerializeField] private UIService UIService;

        private CommandInvoker commandInvoker;

        [Header("CHEST-DATA")]
        [SerializeField] ChestSO chestSO;
        [SerializeField] ChestView chestPrefab;

        [Header("AUDIO-DATA")]
        [SerializeField] SoundClipsSO soundClips;
        [SerializeField] AudioSource audioSourceBGM;
        [SerializeField] AudioSource audioSourceSFX;

        private void Awake() => Instance = this;
        private void Start()
        {
            InitializeSevices();
            InitializeData();
        }

        private void InitializeData()
        {
            commandInvoker = new CommandInvoker();
            GetPopUpService().SetCommandInvoker(commandInvoker);
        }

        private void InitializeSevices()
        {
            playerService = new PlayerService(3, 3, UIService);
            chestService = new ChestService(chestSO, chestPrefab);
            soundService = new SoundService(audioSourceBGM, audioSourceSFX, soundClips);

            UIService.InitializeSevices(this);
            chestService.InitializeSevices(this);
        }

        private void Update() => chestService.Update();
        public void UndoCommand() => commandInvoker.UndoCommand();
        public void ClearCommandHistory() => commandInvoker.ClearHistory();

        public UIService GetUIService() => UIService;
        public SoundService GetSoundService() => soundService;
        public ChestService GetChestService() => chestService;
        public PlayerService GetPlayerService() => playerService;
        public SlotService GetSlotService() => UIService.GetSlotService();
        public PopUpService GetPopUpService() => UIService.GetPopUpService();
    }
}
