namespace SlotMachineApi.Application.Services;

public static class WinCalculator
{
    public static decimal Calculate(int[][] matrix, List<Domain.WinLine> winLines, decimal bet)
    {
        decimal totalWin = 0;

        foreach (var winLine in winLines)
        {
            int[] line = ExtractLine(matrix, winLine);
            totalWin += CalculateLineWin(line, bet);
        }

        return totalWin;
    }

    private static int[] ExtractLine(int[][] matrix, Domain.WinLine winLine)
    {
        int height = matrix.Length;
        int width = matrix[0].Length;

        if (winLine.Type == "StraightRow")
        {
            return matrix[winLine.StartRow];
        }
        else
        {
            return GetZigzagLine(matrix, winLine.StartRow, width, height);
        }
    }

    private static int[] GetZigzagLine(int[][] matrix, int startRow, int width, int height)
    {
        var line = new int[width];
        int row = startRow;
        int direction = 1;

        for (int col = 0; col < width; col++)
        {
            line[col] = matrix[row][col];

            if (row + direction >= height || row + direction < 0)
                direction *= -1;

            row += direction;
        }

        return line;
    }

    public static decimal CalculateLineWin(int[] line, decimal bet)
    {
        if (line.Length < 3) return 0;

        int first = line[0];
        int count = 1;

        for (int i = 1; i < line.Length; i++)
        {
            if (line[i] == first)
                count++;
            else
                break;
        }

        if (count < 3) return 0;

        return bet * (first * count);
    }
}