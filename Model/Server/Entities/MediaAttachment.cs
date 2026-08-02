using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/MediaAttachment/">Mastodon API Documentation</see>
public sealed class MediaAttachment {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public MediaAttachmentType Type { get; set; } = MediaAttachmentType.Unknown;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    [JsonPropertyName("preview_url")]
    public Uri? PreviewUrl { get; set; } = null;

    [JsonPropertyName("remote_url")]
    public Uri? RemoteUrl { get; set; } = null;

    [JsonPropertyName("meta")]
    public MediaAttachmentMeta? Meta { get; set; } = null;

    [JsonPropertyName("description")]
    public string? Description { get; set; } = null;

    [JsonPropertyName("blurhash")]
    public string? BlurHash { get; set; } = null;
}

/// <see href="https://docs.joinmastodon.org/entities/MediaAttachment/#meta">Mastodon API Documentation</see>
public sealed class MediaAttachmentMeta {
    [JsonPropertyName("original")]
    public MediaAttachmentMetaSize? Original { get; set; } = null;

    [JsonPropertyName("small")]
    public MediaAttachmentMetaSize? Small { get; set; } = null;

    [JsonPropertyName("focus")]
    public MediaAttachmentMetaFocus? Focus { get; set; } = null;

    [JsonPropertyName("length")]
    public string? Length { get; set; } = null;

    [JsonPropertyName("duration")]
    public double? Duration { get; set; } = null;

    [JsonPropertyName("fps")]
    public double? Fps { get; set; } = null;

    [JsonPropertyName("size")]
    public string? Size { get; set; } = null;

    [JsonPropertyName("width")]
    public int? Width { get; set; } = null;

    [JsonPropertyName("height")]
    public int? Height { get; set; } = null;

    [JsonPropertyName("aspect")]
    public double? Aspect { get; set; } = null;

    [JsonPropertyName("audio_encode")]
    public string? AudioEncode { get; set; } = null;

    [JsonPropertyName("audio_bitrate")]
    public string? AudioBitrate { get; set; } = null;

    [JsonPropertyName("audio_channels")]
    public string? AudioChannels { get; set; } = null;
}

public sealed class MediaAttachmentMetaSize {
    [JsonPropertyName("width")]
    public int? Width { get; set; }

    [JsonPropertyName("height")]
    public int? Height { get; set; }

    [JsonPropertyName("size")]
    public string? Size { get; set; }

    [JsonPropertyName("aspect")]
    public double? Aspect { get; set; }

    [JsonPropertyName("frame_rate")]
    public string? FrameRate { get; set; }

    [JsonPropertyName("duration")]
    public double? Duration { get; set; }

    [JsonPropertyName("bitrate")]
    public int? BitRate { get; set; }
}

public sealed class MediaAttachmentMetaFocus {
    [JsonPropertyName("x")]
    public double? X { get; set; }

    [JsonPropertyName("y")]
    public double? Y { get; set; }
}
