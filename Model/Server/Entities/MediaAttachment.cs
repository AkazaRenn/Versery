using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/MediaAttachment/">Mastodon API Documentation</see>
public sealed class MediaAttachment {
    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#type"/>
    [JsonPropertyName("type")]
    public MediaAttachmentType Type { get; set; } = MediaAttachmentType.Unknown;

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#preview_url"/>
    [JsonPropertyName("preview_url")]
    public Uri? PreviewUrl { get; set; } = null;

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#remote_url"/>
    [JsonPropertyName("remote_url")]
    public Uri? RemoteUrl { get; set; } = null;

    /// <summary>
    /// Version: 2.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#meta-focus"/>
    [JsonPropertyName("meta")]
    public MediaAttachmentMeta? Meta { get; set; } = null;

    /// <summary>
    /// Version: 2.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#description"/>
    [JsonPropertyName("description")]
    public string? Description { get; set; } = null;

    /// <summary>
    /// Version: 2.8.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#blurhash"/>
    [JsonPropertyName("blurhash")]
    public string? Blurhash { get; set; } = null;
}

public sealed class MediaAttachmentMeta {
    /// <summary>
    /// Version: 1.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#meta"/>
    [JsonPropertyName("original")]
    public MediaAttachmentMetaSize? Original { get; set; } = null;

    /// <summary>
    /// Version: 1.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#meta"/>
    [JsonPropertyName("small")]
    public MediaAttachmentMetaSize? Small { get; set; } = null;

    /// <summary>
    /// Version: 2.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#meta-focus"/>
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

/// <summary>
/// Version: 1.5.0
/// </summary>
/// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#meta"/>
public sealed class MediaAttachmentMetaSize {
    [JsonPropertyName("width")]
    public int? Width { get; set; } = null;

    [JsonPropertyName("height")]
    public int? Height { get; set; } = null;

    [JsonPropertyName("size")]
    public string? Size { get; set; } = null;

    [JsonPropertyName("aspect")]
    public double? Aspect { get; set; } = null;

    [JsonPropertyName("frame_rate")]
    public string? FrameRate { get; set; } = null;

    [JsonPropertyName("duration")]
    public double? Duration { get; set; } = null;

    [JsonPropertyName("bitrate")]
    public int? BitRate { get; set; } = null;
}

public sealed class MediaAttachmentMetaFocus {
    /// <summary>
    /// Version: 2.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#meta-focus-x"/>
    [JsonPropertyName("x")]
    public double? X { get; set; } = null;

    /// <summary>
    /// Version: 2.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/MediaAttachment/#meta-focus-y"/>
    [JsonPropertyName("y")]
    public double? Y { get; set; } = null;
}

public enum MediaAttachmentType {
    [JsonStringEnumMemberName("unknown")]
    Unknown,

    [JsonStringEnumMemberName("image")]
    Image,

    [JsonStringEnumMemberName("gifv")]
    Gifv,

    [JsonStringEnumMemberName("video")]
    Video,

    [JsonStringEnumMemberName("audio")]
    Audio,
}
