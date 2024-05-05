using System.Diagnostics.CodeAnalysis;

namespace BaggageTrackerApi.Models.QrCode;

[method: SetsRequiredMembers]
public class QrCodeFile(string name, byte[] content)
{
    public required string Name { get; init; } = name;

    public required byte[] Content { get; init; } = content;
}