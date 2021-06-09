
/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class rss
{

    private rssChannel channelField;

    private decimal versionField;

    /// <remarks/>
    public rssChannel channel
    {
        get
        {
            return this.channelField;
        }
        set
        {
            this.channelField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class rssChannel
{

    private string titleField;

    private string linkField;

    private string descriptionField;

    private string copyrightField;

    private string generatorField;

    private link link1Field;

    private rssChannelItem[] itemField;

    /// <remarks/>
    public string title
    {
        get
        {
            return this.titleField;
        }
        set
        {
            this.titleField = value;
        }
    }

    /// <remarks/>
    public string link
    {
        get
        {
            return this.linkField;
        }
        set
        {
            this.linkField = value;
        }
    }

    /// <remarks/>
    public string description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public string copyright
    {
        get
        {
            return this.copyrightField;
        }
        set
        {
            this.copyrightField = value;
        }
    }

    /// <remarks/>
    public string generator
    {
        get
        {
            return this.generatorField;
        }
        set
        {
            this.generatorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("link", Namespace = "http://www.w3.org/2005/Atom")]
    public link link1
    {
        get
        {
            return this.link1Field;
        }
        set
        {
            this.link1Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("item")]
    public rssChannelItem[] item
    {
        get
        {
            return this.itemField;
        }
        set
        {
            this.itemField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2005/Atom", IsNullable = false)]
public partial class link
{

    private string relField;

    private string typeField;

    private string hrefField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string rel
    {
        get
        {
            return this.relField;
        }
        set
        {
            this.relField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string href
    {
        get
        {
            return this.hrefField;
        }
        set
        {
            this.hrefField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class rssChannelItem
{

    private rssChannelItemGuid guidField;

    private string[] categoryField;

    private string titleField;

    private string descriptionField;

    private string pubDateField;

    private string linkField;

    private string partnerIdField;

    private uint publishIdField;

    private object itemIdField;

    private string feedIdField;

    private object sourceFeedIdField;

    private string isFeaturedField;

    private string isMicrosoftOwnedField;

    private object sourceNameField;

    private object contentTypeField;

    private byte mediaTypeField;

    private byte subMediaTypeField;

    private string relativeTimeField;

    private object twitterIdField;

    /// <remarks/>
    public rssChannelItemGuid guid
    {
        get
        {
            return this.guidField;
        }
        set
        {
            this.guidField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("category")]
    public string[] category
    {
        get
        {
            return this.categoryField;
        }
        set
        {
            this.categoryField = value;
        }
    }

    /// <remarks/>
    public string title
    {
        get
        {
            return this.titleField;
        }
        set
        {
            this.titleField = value;
        }
    }

    /// <remarks/>
    public string description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public string pubDate
    {
        get
        {
            return this.pubDateField;
        }
        set
        {
            this.pubDateField = value;
        }
    }

    /// <remarks/>
    public string link
    {
        get
        {
            return this.linkField;
        }
        set
        {
            this.linkField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public string PartnerId
    {
        get
        {
            return this.partnerIdField;
        }
        set
        {
            this.partnerIdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public uint PublishId
    {
        get
        {
            return this.publishIdField;
        }
        set
        {
            this.publishIdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public object ItemId
    {
        get
        {
            return this.itemIdField;
        }
        set
        {
            this.itemIdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public string FeedId
    {
        get
        {
            return this.feedIdField;
        }
        set
        {
            this.feedIdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public object SourceFeedId
    {
        get
        {
            return this.sourceFeedIdField;
        }
        set
        {
            this.sourceFeedIdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public string IsFeatured
    {
        get
        {
            return this.isFeaturedField;
        }
        set
        {
            this.isFeaturedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public string IsMicrosoftOwned
    {
        get
        {
            return this.isMicrosoftOwnedField;
        }
        set
        {
            this.isMicrosoftOwnedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public object SourceName
    {
        get
        {
            return this.sourceNameField;
        }
        set
        {
            this.sourceNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public object ContentType
    {
        get
        {
            return this.contentTypeField;
        }
        set
        {
            this.contentTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public byte MediaType
    {
        get
        {
            return this.mediaTypeField;
        }
        set
        {
            this.mediaTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public byte SubMediaType
    {
        get
        {
            return this.subMediaTypeField;
        }
        set
        {
            this.subMediaTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public string RelativeTime
    {
        get
        {
            return this.relativeTimeField;
        }
        set
        {
            this.relativeTimeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://sxpdata.microsoft.com/sxp")]
    public object TwitterId
    {
        get
        {
            return this.twitterIdField;
        }
        set
        {
            this.twitterIdField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class rssChannelItemGuid
{

    private bool isPermaLinkField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool isPermaLink
    {
        get
        {
            return this.isPermaLinkField;
        }
        set
        {
            this.isPermaLinkField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

