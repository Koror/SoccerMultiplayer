using Mirror;
using Network.Data;
using Network.Enums;
using Network.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Network.Views
{
    public class LobbyView : MonoBehaviour
    {
        [SerializeField] private NetworkManagerSoccer _networkManager;
        [SerializeField] private GameObject _hostMenu;
        [SerializeField] private GameObject _selectLobbyMenu;
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _clientButton;
        
        [SerializeField] private Button _playButton;

        [Header("Colors Button")] 
        [SerializeField] private Button _greenButton;
        [SerializeField] private Button _orangeButton;
        [SerializeField] private Button _blueButton;
        [SerializeField] private Button _purpleButton;
        [SerializeField] private GameObject _colorButtonLayout;


        private CharacterColorType _selectedColor;
        
        private int[] SelectedColorsArray = new int[4]{0,0,0,0};

        
        private void Start()
        {
            _hostMenu.SetActive(true);
            _selectLobbyMenu.SetActive(false);
            _playButton.gameObject.SetActive(false);
            _colorButtonLayout.SetActive(true);
            
            
            _hostButton.onClick.AddListener(() =>
            {
                _networkManager.StartHost();
            });
            _clientButton.onClick.AddListener(() =>
            {
                _networkManager.StartClient();
            });

            _greenButton.onClick.AddListener(() =>OnColorSelect(CharacterColorType.Green));
            _orangeButton.onClick.AddListener(() =>OnColorSelect(CharacterColorType.Orange));
            _blueButton.onClick.AddListener(() =>OnColorSelect(CharacterColorType.Blue));
            _purpleButton.onClick.AddListener(() =>OnColorSelect(CharacterColorType.Purple));

            _playButton.onClick.AddListener(() => OnPlay());
            
            _networkManager.OnConnected += ActivateSelectMenu;
            _networkManager.OnStartServerAction += OnStartServer;
            _networkManager.OnStartClientAction += OnStartClient;
        }

        [ServerCallback]
        private void OnStartServer()
        {
            NetworkServer.RegisterHandler<SelectCharacterColorClientMessage>(OnServerColorSelect);
            NetworkServer.RegisterHandler<GetLobbyStateMessage>(OnClientRequestLobbyState);

        }
        [ClientCallback]
        private void OnStartClient()
        {
            NetworkClient.RegisterHandler<SelectedCharacterColorServerMessage>(OnClientColorSelect);
            NetworkClient.RegisterHandler<LobbyStateMessage>(UpdateLobbyState);
        }

        [ServerCallback]
        private void OnServerColorSelect(NetworkConnectionToClient conn, SelectCharacterColorClientMessage msg)
        {
            var index = (int)msg.ColorType;
            SelectedColorsArray[index] = 1;
            NetworkServer.SendToAll(new SelectedCharacterColorServerMessage(){ColorType = msg.ColorType});
        }
        
        [ClientCallback]
        private void OnClientColorSelect(SelectedCharacterColorServerMessage msg)
        {
            if (msg.ColorType == CharacterColorType.Green)
            {
                _greenButton.gameObject.SetActive(false);
            }
            if (msg.ColorType == CharacterColorType.Orange)
            {
                _orangeButton.gameObject.SetActive(false);
            }
            if (msg.ColorType == CharacterColorType.Blue)
            {
                _blueButton.gameObject.SetActive(false);
            }
            if (msg.ColorType == CharacterColorType.Purple)
            {
                _purpleButton.gameObject.SetActive(false);
            }
        }
        
        [ClientCallback]
        private void UpdateLobbyState(LobbyStateMessage msg)
        {
            var colors = msg.SelectedColors;
            if (colors.Length != 4)
            {
                Debug.Log("LobbyView: Incorrect LobbyState Message");
                return;
            }
            if (colors[0] != 0)
            {
                _greenButton.gameObject.SetActive(false);
            }
            if (colors[1] != 0)
            {
                _orangeButton.gameObject.SetActive(false);
            }
            if (colors[2] != 0)
            {
                _blueButton.gameObject.SetActive(false);
            }
            if (colors[3] != 0)
            {
                _purpleButton.gameObject.SetActive(false);
            }
        }
        
        [ServerCallback]
        private void OnClientRequestLobbyState(NetworkConnectionToClient conn, GetLobbyStateMessage msg)
        {
            conn.Send(new LobbyStateMessage() {SelectedColors = SelectedColorsArray});
        }
        
        private void OnColorSelect(CharacterColorType colorType)
        {
            _colorButtonLayout.gameObject.SetActive(false);
            _selectedColor = colorType;
            _playButton.gameObject.SetActive(true);
            SelectCharacterColorClientMessage clientMessage = new SelectCharacterColorClientMessage() { ColorType = colorType};
            NetworkClient.Send(clientMessage);
        }

        private void OnPlay()
        {
            CreateCharacterMessage message = new CreateCharacterMessage() { Color = _selectedColor};
            NetworkClient.Send(message);
            gameObject.SetActive(false);
        }

        private void ActivateSelectMenu()
        {
            NetworkClient.Send(new GetLobbyStateMessage());
            _hostMenu.SetActive(false);
            _selectLobbyMenu.SetActive(true);
        }

        private void OnDisable()
        {
            _networkManager.OnConnected -= ActivateSelectMenu;
            _networkManager.OnStartServerAction -= OnStartServer;
            _networkManager.OnStartClientAction -= OnStartServer;
        }
    }
}