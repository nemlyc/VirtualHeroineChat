# VirtualHeroineChat

## OverView
テキストと同時に感情を送信することができるリアルタイムチャットアプリです。送信された感情は、画面に表示する任意のVRMモデルに反映されます。

## Usage
1. Releaseよりzipファイルをダウンロードします。
2. ``` VirtualHeroineChat/VHChat-Server/VirtualHeroineChatServer.exe ``` を起動します（ローカルサーバーが立ち上がります）。
3. ``` VirtualHeroineChat/VHChat-Client/VirtualHeroineChatClient.exe ``` を起動します。
4. "名前" と "ルーム名（半角４文字）" を入力することでチャットルームへ入室します。
5. メッセージを入力し、「表情を追加」ドロップダウンから今の感情を選んで、送信ボタンを押すと、メッセージと一緒に、モデルへ感情が反映されます。
（一台のPCで動作確認をする場合は、お手数ですが、Clientフォルダを複製してからご確認ください。）

## Requirement
- Grpc
- MagicOnion
- MessagePack
- .net core 3.1

## Develop（任意の3Dモデルを設定するときなど）

### クライアントサイド
0. ``` git clone https://github.com/nemlyc/VirtualHeroineChat.git ``` または リポジトリからzipファイルをダウンロードし、Unityで```VitrualHeroineChatClient```を開きます。
1. grpc（grpc_unity_package.2.26.0）を入れるhttps://packages.grpc.io/archive/2019/12/a02d6b9be81cbadb60eed88b3b44498ba27bcba9-edd81ac6-e3d1-461a-a263-2b06ae913c3f/index.xml
2. magiconionclientの最新版（3.0.9）を入れる（インポート時に、Plugins以下の、System.Threading.Tasks.Extensions.dll以外のチェックを外す）https://github.com/Cysharp/MagicOnion/releases
3. messagePack-CSharpの最新版（MessagePack.Unity.2.1.90）を入れる（Plugin下はインポートしない。）https://github.com/neuecc/MessagePack-CSharp/releases
4. ProjectSettingsで "Api Compatibility Level" が ".Net 4.x" に、"Allow 'unsafe' code" にチェックが入っていることを確認。
5. UniVRMの最新版（0.55.0）を入れるhttps://github.com/vrm-c/UniVRM/releases
6. TextMeshProをインポート。（ライセンスに注意してフォントアセットを作成することで日本語を使用することができます。）
7. 任意のvrmモデルをインポート。
8. モデルを正面にうまく配置する（）。
9. yを180度反転、leftArm, rightArmを70くらい曲げると良い感じになります。
10. 画面サイズを（400 * 800）に設定する。
11. ビルドします。

## Licences
This software : MIT Licence

This software includes the work that is distributed in the Apache License 2.0
MessagePack C# : https://github.com/neuecc/MessagePack-CSharp/blob/master/LICENSE
MagicOnion : https://github.com/Cysharp/MagicOnion/blob/master/LICENSE
lz4net : https://github.com/MiloszKrajewski/lz4net/edit/master/LICENSE.md

## Auther
Motoki
