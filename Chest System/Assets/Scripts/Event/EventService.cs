namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController<ChestView> OnChestSelected { get; private set; }

        public EventService()
        {
            OnChestSelected = new EventController<ChestView>();
        }
    }
}
