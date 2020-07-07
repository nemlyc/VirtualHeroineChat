using MagicOnion;
using VHChat.Share.MessagePackObjects;
using System.Threading.Tasks;

namespace VHChat.Share.Hubs
{
    /// <summary>
    /// クライアントが使用するサーバーAPI
    /// </summary>
    public interface IChathub : IStreamingHub<IChathub, IChatHubReciever>
    {
        /// <summary>
        /// チャットルームを作成・入室し、情報をサーバーへ伝える
        /// </summary>
        /// <param name="host">ルームの作成元</param>
        /// <param name="opponentId">呼び出された相手</param>
        /// <returns></returns>
        Task JoinRoomAsync(User user, string roomId);

        /// <summary>
        /// チャットルームから退室することをサーバーへ伝える
        /// </summary>
        /// <returns></returns>
        Task LeaveRoomAsync();

        /// <summary>
        /// チャットルームにいる人へのメッセージ情報をサーバーへ伝える
        /// </summary>
        /// <param name="message">メッセージの内容</param>
        /// <param name="emotion">添付する表情データ</param>
        /// <returns></returns>
        Task SendMessageAsync(string message, string emotion);

    }

    /// <summary>
    /// サーバーが使用するクライアントAPI
    /// </summary>
    public interface IChatHubReciever
    {
        /// <summary>
        /// 相手の情報をクライアントに伝える
        /// </summary>
        /// <param name="name">相手の名前</param>
        void OnJoinRoom(string name);

        /// <summary>
        /// 相手が退室したことをクライアントに伝える
        /// </summary>
        /// <param name="name"></param>
        void OnLeaveRoom(string name);

        /// <summary>
        /// 相手がメッセージを送信したことをクライアントに伝える
        /// </summary>
        /// <param name="message">メッセージの内容</param>
        /// <param name="emotion">添付する表情データ</param>
        void OnSendMessage(string name, string message, string emotion);
    }
}