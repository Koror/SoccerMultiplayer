using Mirror;
using Network.Enums;

namespace Network.Data
{
    public struct CreateCharacterMessage : NetworkMessage
    {
        public CharacterColorType Color;
    }
    
    public struct SelectCharacterColorClientMessage : NetworkMessage
    {
        public CharacterColorType ColorType;
    }
    
    public struct SelectedCharacterColorServerMessage : NetworkMessage
    {
        public CharacterColorType ColorType;
    }

    public struct LobbyStateMessage : NetworkMessage
    {
        public int[] SelectedColors;
    }
    
    public struct GetLobbyStateMessage : NetworkMessage
    {
    }
}