namespace Y.Chat.Application.Hubs
{
    public class ChatConst
    {
        public const string Online = "Online";

        public const string UserOnlineList = "OnlineList";

        public const string Group = "Group";

        public const string Recive = "ReciveMessage";

        public const string WithDraw = "WithDrawMessage";

        public const string Notice = "Notifiy";

        public static string GetOnlineKey(string param) => $"{Online}_{param}";

        public static string GroupKey(string param) => $"{Group}_{param}";

        public static string OnlineListKey(string param) => $"{UserOnlineList}_{param}";
    }
}
