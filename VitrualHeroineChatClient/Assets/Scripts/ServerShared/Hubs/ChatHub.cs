

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
        Task JoinRoomAsync(User host, string opponentId);

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

        /// <summary>
        /// ユーザーidをランダム生成する
        /// </summary>
        /// <returns></returns>
        Task GenerateIdAsync();
        /*
        /// <summary>
        /// 登録したいユーザー情報をサーバーへ伝える
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task RegisterUser(User user);

        /// <summary>
        /// 登録する際に設定したいidがすでに存在しないかをチェックする
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task CheckMultiplyID(string id);
        */
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
        /// <param name="bio">相手の自己紹介</param>
        void OnJoinRoom(string name, string bio);

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