namespace Y.Chat.EntityCore.Domain.ChatDomain.Shared
{
    public enum MessageType
    {
        /// <summary>
        /// 图片
        /// </summary>
        Img,
        /// <summary>
        /// 文本
        /// </summary>
        Text,
        /// <summary>
        /// 表情
        /// </summary>
        Emojis,
        /// <summary>
        /// 视频
        /// </summary>
        Video,
        /// <summary>
        /// 语音
        /// </summary>
        Audio
    }

    public class ChangeEnum
    {
        public static MessageType Change(string type)
        {
            if(MessageType.Img.ToString() == type)
            {
                return MessageType.Img;
            }
            if (MessageType.Text.ToString() == type)
            {
                return MessageType.Img;
            }
            if (MessageType.Emojis.ToString() == type)
            {
                return MessageType.Emojis;
            }
            if (MessageType.Video.ToString() == type)
            {
                return MessageType.Video;
            }
            if (MessageType.Audio.ToString() == type)
            {
                return MessageType.Audio;
            }
            return MessageType.Text;
        }
    }
}
