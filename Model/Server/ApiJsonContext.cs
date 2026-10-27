using System.Text.Json.Serialization;

namespace Model.Server;

// Refitter's generated context is disabled: the spec's inline "Options" schema collides with JsonSerializerContext.Options.
[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(Entities.CreateAppRequest))]
[JsonSerializable(typeof(Entities.CredentialAccount))]
[JsonSerializable(typeof(Entities.CredentialApplication))]
[JsonSerializable(typeof(Entities.Error))]
[JsonSerializable(typeof(Entities.Instance))]
[JsonSerializable(typeof(Entities.PostOauthTokenRequest))]
[JsonSerializable(typeof(Entities.Token))]
[JsonSerializable(typeof(ICollection<Entities.Status>))]
internal sealed partial class ApiJsonContext: JsonSerializerContext;
