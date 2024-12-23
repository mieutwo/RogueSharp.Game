﻿using RLNET;

namespace RogueSharp.Game.Console
{
    public class Game
    {
        // The screen height and width are in number of tiles
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;
        private static RLRootConsole _rootConsole;
        
        // The map console takes up most of the screen and is where the map will be drawn
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;
        private static RLConsole _mapConsole;
 
        // Below the map console is the message console which displays attack rolls and other information
        private static readonly int _messageWidth = 80;
        private static readonly int _messageHeight = 11;
        private static RLConsole _messageConsole;
 
        // The stat console is to the right of the map and display player and monster stats
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;
        private static RLConsole _statConsole;
 
        // Above the map is the inventory console which shows the players equipment, abilities, and items
        private static readonly int _inventoryWidth = 80;
        private static readonly int _inventoryHeight = 11;
        private static RLConsole _inventoryConsole;

        public static void Main()
        {
            // This must be the exact name of the bitmap font file we are using or it will error.
            string fontFileName = "terminal8x8.png";
            // The title will appear at the top of the console window
            string consoleTitle = "RougeSharp V3 Tutorial - Level 1";
            // Tell RLNet to use the bitmap font that we specified and that each tile is 8 x 8 pixels
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
                8, 8, 1f, consoleTitle);
            
            _mapConsole = new RLConsole( _mapWidth, _mapHeight );
            _messageConsole = new RLConsole( _messageWidth, _messageHeight );
            _statConsole = new RLConsole( _statWidth, _statHeight );
            _inventoryConsole = new RLConsole( _inventoryWidth, _inventoryHeight );
            
            // Set up a handler for RLNET's Update event
            _rootConsole.Update += OnRootConsoleUpdate;
            // Set up a handler for RLNET's Render event
            _rootConsole.Render += OnRootConsoleRender;
            // Begin RLNET's game loop
            _rootConsole.Run();
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            // Set background color and text for each console 
            // so that we can verify they are in the correct positions
            _mapConsole.SetBackColor( 0, 0, _mapWidth, _mapHeight, RLColor.Black );
            _mapConsole.Print( 1, 1, "Map", RLColor.White );  
 
            _messageConsole.SetBackColor( 0, 0, _messageWidth, _messageHeight, RLColor.Gray );
            _messageConsole.Print( 1, 1, "Messages", RLColor.White );
 
            _statConsole.SetBackColor( 0, 0, _statWidth, _statHeight, RLColor.Brown );
            _statConsole.Print( 1, 1, "Stats", RLColor.White );
 
            _inventoryConsole.SetBackColor( 0, 0, _inventoryWidth, _inventoryHeight, RLColor.Cyan );
            _inventoryConsole.Print( 1, 1, "Inventory", RLColor.White );
        }

        // Event handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            // Blit the sub consoles to the root console in the correct locations
            RLConsole.Blit( _mapConsole, 0, 0, _mapWidth, _mapHeight, 
                _rootConsole, 0, _inventoryHeight );
            RLConsole.Blit( _statConsole, 0, 0, _statWidth, _statHeight, 
                _rootConsole, _mapWidth, 0 );
            RLConsole.Blit( _messageConsole, 0, 0, _messageWidth, _messageHeight, 
                _rootConsole, 0, _screenHeight - _messageHeight );
            RLConsole.Blit( _inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight, 
                _rootConsole, 0, 0 );
            
            // Tell RLNET to draw the console that we set
            _rootConsole.Draw();
        }
    }
}