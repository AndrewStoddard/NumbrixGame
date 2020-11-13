using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace NumbrixGame.Model
{
    public class CsvReader
    {
        private const int BoardSizeColumn = 0;
        private const char DefaultDelimiter = ',';
        private const char BoardSizeDelimiter = 'x';

        public async Task<NumbrixGameBoard> LoadPuzzle(StorageFile puzzleFile)
        {
            NumbrixGameBoard gameBoard = new NumbrixGameBoard();
            var buffer = await FileIO.ReadBufferAsync(puzzleFile);

            using (var dataReader = DataReader.FromBuffer(buffer))
            {
                var content = dataReader.ReadString(buffer.Length);
                var data = content.Split(Environment.NewLine);

                var row = 0;
                var puzzleRowNumber = 0;
                foreach (var line in data)
                {
                    if (true)
                    {
                        if (row == 0)
                        {
                            var settings = line.Split(DefaultDelimiter);

                            var widthAsString = settings[0].Trim();
                            var heightAsString = settings[1].Trim();

                            gameBoard.BoardWidth = (string.IsNullOrEmpty(widthAsString)) ? -1 : int.Parse(widthAsString);
                            gameBoard.BoardHeight = (string.IsNullOrEmpty(heightAsString)) ? -1 : int.Parse(heightAsString);
                        }
                        else
                        {
                            var cellRow = line.Split(DefaultDelimiter);

                            gameBoard.AddCell(
                                new NumbrixGameBoardCell(int.Parse(cellRow[0]), int.Parse(cellRow[1]))
                                {
                                    NumbrixValue = int.Parse(cellRow[2]), 
                                    DefaultValue = bool.Parse(cellRow[3])
                                });

                            puzzleRowNumber++;
                        }

                        row++;
                    }
                }
            }

            var allCells = gameBoard.NumbrixGameBoardCells;
            return gameBoard;
        }

        private int getBoardWidth(string data)
        {
            var widthAsString = data.ToLower().Trim().Split(BoardSizeDelimiter)[0];
            var width = int.Parse(widthAsString);
            return width;
        }

        private int getBoardHeight(string data)
        {
            var heightAsString = data.ToLower().Trim().Split(BoardSizeDelimiter)[1];
            var height = int.Parse(heightAsString);
            return height;
        }

        private IList<NumbrixGameBoardCell> createBoardCells(int row, string[] data)
        {
            IList<NumbrixGameBoardCell> cells = new List<NumbrixGameBoardCell>();
            var column = 0;
            foreach (var currentCellValue in data)
            {

                int value = (string.IsNullOrEmpty(currentCellValue)) ? -1 : int.Parse(currentCellValue);
                var newCell = new NumbrixGameBoardCell(column + 1, row + 1) { NumbrixValue = value};
                cells.Add(newCell);
                column++;
            }

            return cells;
        }


    }
}
