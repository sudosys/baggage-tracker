using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Entities;

[Table("bt_users")]
[method: SetsRequiredMembers]
public sealed class User(UserRole role, string username, string fullName, string password)
{
    [SetsRequiredMembers]
    public User(long id, UserRole role, string username, string fullName, string password) 
        : this(role, username, fullName, password)
    {
        Id = id;
        Username = username;
        FullName = fullName;
        Password = password;
        Role = role;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; init; }

    [StringLength(50)]
    public required string Username { get; init; } = username;

    [StringLength(150)]
    public required string FullName { get; init; } = fullName;

    [StringLength(256)]
    [JsonIgnore]
    public string Password { get; init; } = password;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required UserRole Role { get; init; } = role;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Flight? ActiveFlight { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Baggage>? Baggages { get; init; }
}