using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents a file or media attachment that can be added to a status.
/// Version: 3.5.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/MediaAttachment/">Mastodon API Documentation</see>
public sealed class MediaAttachment {
    /// <summary>
    /// The ID of the attachment in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The type of the attachment.
    /// </summary>
    [JsonPropertyName("type")]
    public MediaAttachmentType Type { get; set; } = MediaAttachmentType.Unknown;

    /// <summary>
    /// The location of the original full-size attachment. Url may be null if the file is still being processed. See POST /api/v2/media.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// The location of a scaled-down preview of the attachment.
    /// </summary>
    [JsonPropertyName("preview_url")]
    public Uri? PreviewUrl { get; set; } = null;

    /// <summary>
    /// The location of the full-size original attachment on the remote website.
    /// </summary>
    [JsonPropertyName("remote_url")]
    public Uri? RemoteUrl { get; set; } = null;

    /// <summary>
    /// Metadata returned by Paperclip.
    /// </summary>
    [JsonPropertyName("meta")]
    public MediaAttachmentMeta? Meta { get; set; } = null;

    /// <summary>
    /// Alternate text that describes what is in the media attachment, to be used for the visually impaired or when media attachments do not load.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; } = null;

    /// <summary>
    /// A hash computed by the BlurHash algorithm, for generating colorful preview thumbnails when media has not been downloaded yet.
    /// </summary>
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
