namespace Y.Chat.EntityCore.Hubs
{
    public class UserStatus
    {
        public Guid Id { get;private set; }
        public OnlineType OnlineType { get;private set; }

        public DateTime OnLineTime { get;private set; }

        public DateTime? LeaveTime { get;private set; }

        public UserStatus(Guid id)      
        {
            Id = id;
            OnlineType = OnlineType.OnLine;
            OnLineTime = DateTime.Now;
        }

        public void SetLeave()
        {
            OnlineType = OnlineType.Leave;
            LeaveTime = DateTime.Now;
        }
    }
}
