using ProjectTeam01.datalayer;
using ProjectTeam01.domain.generation;
using ProjectTeam01.domain.Session;

namespace ProjectTeam01.presentation.Controllers
{
    internal class StartFromSave : IGameSession
    {
        private GameController _controller;
        private GameSession _game;
        private char[,] _map;
        public StartFromSave(string save)
        {
            _game = GameDataService.LoadGame(save);
            _controller = new GameController(_game);
            _map = new char[GenerationConstants.MAP_HEIGHT, GenerationConstants.MAP_WIDTH];
        }

        public bool IsGameRunning(int key)
        {
            return _controller.HandleInput((char)key);
        }

        public void RenderGameScreen(nint stdscr)
        {
            for (int y = 0; y < GenerationConstants.MAP_HEIGHT; y++)
            {
                for (int x = 0; x < GenerationConstants.MAP_WIDTH; x++)
                {
                    _map[y, x] = ' ';
                }
            }

            GameStateRenderer.RenderHandler(_controller.GetGameStateViewModel(), stdscr, _controller, _map);
        }

    }

}

