using System.Text;
using BaggageTrackerApi.Models.PasswordGeneration;

namespace BaggageTrackerApi.Services;

public class PasswordGenerator
{
    private readonly PasswordGenerationSettings _passwordGenerationSettings = new();

    private readonly Random _randomEngine = new();

    private readonly List<string> _passwordGenerationPool = [];

    public PasswordGenerator()
    {
        SetEnabledPools();
    }

    public string GeneratePassword()
    {
        var pwd = new StringBuilder();

        for (var i = 0; i < _passwordGenerationSettings.Length; i++)
        {
            var nextTokenPool = SelectTokenPool();

            var token = SelectTokenFromPool(nextTokenPool);

            pwd.Append(token);
        }

        return pwd.ToString();
    }

    private string SelectTokenPool()
    {
        var poolIndex = _randomEngine.Next(0, _passwordGenerationPool.Count);

        return _passwordGenerationPool[poolIndex];
    }

    private char SelectTokenFromPool(string pool)
    {
        var tokenIndex = _randomEngine.Next(0, pool.Length);

        return pool[tokenIndex];
    }

    private void SetEnabledPools()
    {
        if (_passwordGenerationSettings.IncludeNumbers)
        {
            _passwordGenerationPool.Add(PasswordGenerationPool.Numbers);
        }
        if (_passwordGenerationSettings.IncludeSymbols)
        {
            _passwordGenerationPool.Add(PasswordGenerationPool.Symbols);
        }
        if (_passwordGenerationSettings.IncludeLowerCase)
        {
            _passwordGenerationPool.Add(PasswordGenerationPool.LowerCase);
        }
        if (_passwordGenerationSettings.IncludeUpperCase)
        {
            _passwordGenerationPool.Add(PasswordGenerationPool.UpperCase);
        }
    }
}