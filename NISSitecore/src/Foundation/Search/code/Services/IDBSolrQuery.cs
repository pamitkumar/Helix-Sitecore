using System;
using System.Collections.Specialized;
using System.Diagnostics;

namespace NISSitecore.Foundation.Search.Services
{
    /// <summary>
    /// Maps to the sitecore item for defining a Solr IDB Query.
    /// Template: /sitecore/templates/Foundation/Solr Query
    /// </summary>
    public class IDBSolrQuery
    {
        /// <summary>
        /// Get/set the index to query. Leave null or empty to query the context database.
        /// </summary>
        [SitecoreField("Index Name", "{45C24474-866E-4A61-8FA7-89725BE4887E}")]
        public string IndexName { get; set; }

        /// <summary>
        /// Get/set the number of rows to return in the result set.
        /// </summary>
        [SitecoreField("rows", "{60A3A788-DEFD-42A1-B3F9-CB12CE1FD4C5}")]
        public int Rows { get; set; }

        /// <summary>
        /// Get/Set the sort (e.g. title_t desc). Note that this negates all document scores and relevancy, so its not recommended to set a manual sort, so use wisely.
        /// </summary>        /// 
        [SitecoreField("sort", "{14AADD0A-ED3E-4002-A1F6-3E76DAA66AE7}")]
        public string Sort { get; set; }

        /// <summary>
        /// Gets/sets the flag that enables query debugging.
        /// </summary>
        [SitecoreField("debug", "{312218D1-A738-4871-8F2A-4AD7C5BDA223}")]
        public bool DebugQuery { get; set; }

        /// <summary>
        /// Get/set the query locations for filtering. Note filters do not affect boosting scores / relevancy.
        /// </summary>
        [SitecoreField("Locations", "{68663432-9C2F-4569-B6AC-C1148D4E3718}")]
        public Guid[] Locations { get; set; }

        /// <summary>
        /// Get/set the query templates for filtering. Note filters do not affect boosting scores / relevancy.
        /// </summary>
        [SitecoreField("Templates", "{F5D03D39-D585-4455-A3C5-CEA20C8E6255}")]
        public Guid[] Templates { get; set; }

        /// <summary>
        /// The fields to query.
        /// </summary>
        /// </summary>
        [SitecoreField("qf", "{21CF9423-6736-4A79-9ECE-4E0BA3B9C114}")]
        public NameValueCollection QueryFields { get; set; }

        /// <summary>
        /// The tie parameter specifies a float value (which should be something much less than 1) to use as tiebreaker in DisMax / EDixMax queries.
        /// </summary>
        [SitecoreField("tie", "{BF9036A0-55F2-4A50-AF7D-694217D0C284}")]
        public float Tie { get; set; }

        /// <summary>
        /// Gets/sets the minimum match.
        /// </summary>
        [SitecoreField("mm", "{900EA9C8-57DE-4415-ABAD-9F2EE8C0D2B4}")]
        public string MinimumMatch { get; set; }

        /// <summary>
        /// Gets/sets the minimum match auto relax.
        /// </summary>
        [SitecoreField("mmAutoRelax", "{90C8D540-A5D2-4D38-AF5C-A9DF50036D11}")]
        public string MinimumMatchAutoRelax { get; set; }

        /// <summary>
        /// Gets/Sets the phrase fields.
        /// </summary>
        [SitecoreField("pf", "{DAE537A2-E458-4984-AF68-0163E6573522}")]
        public NameValueCollection PhraseFields { get; set; }

        /// <summary>
        /// Gets/Sets the phrase 2 fields.
        /// </summary>
        [SitecoreField("pf2", "{C974A67C-B433-441B-A705-1E8AEB98FABF}")]
        public NameValueCollection PhraseFields2 { get; set; }

        /// <summary>
        /// Gets/Sets the phrase 3 fields.
        /// </summary>
        [SitecoreField("pf3", "{835DBB35-ACDF-4F71-9EF0-EEE988CCDFA4}")]
        public NameValueCollection PhraseFields3 { get; set; }

        /// <summary>
        /// Get/Set the phrase slop
        /// </summary>
        [SitecoreField("ps", "{8E5C4B97-26B5-4C63-AF5A-2F3A9E03E953}")]
        public float PhraseSlop { get; set; }

        /// <summary>
        /// Gets/Sets the phrase slop 2 fields.
        /// </summary>
        [SitecoreField("ps2", "{FB5D8BC5-987D-4A5C-A70F-D443E648F031}")]
        public float PhraseSlop2 { get; set; }

        /// <summary>
        /// Gets/Sets the phrase slop 3 fields.
        /// </summary>
        [SitecoreField("ps3", "{4EEB15D7-D843-47DF-B94B-97CB8E134E69}")]
        public float PhraseSlop3 { get; set; }

        /// <summary>
        /// Gets/sets the boost field (usually a function, and different from the "BF" parameter).
        /// </summary>
        [SitecoreField("boost", "{F11E743C-DA7B-43DA-A193-6E4357A76FAD}")]
        public string Boost { get; set; }

        /// <summary>
        /// Gets/sets the boost function (Note this is different from the "boost" parameter).
        /// </summary>
        [SitecoreField("bf", "{23730D90-EE72-40F9-9B6B-694FA56B3C4C}")]
        public string BoostFunction { get; set; }

        /// <summary>
        /// Gets/sets the boost query.
        /// </summary>
        [SitecoreField("bq", "{33FD68D4-2573-49E8-B3A9-1ABEA0D54ADE}")]
        public string BoostQuery { get; set; }

        /// <summary>
        /// Gets/sets if facetting is on.
        /// </summary>
        [SitecoreField("facet", "{12DB0B37-5748-4B21-9A61-E903A3E4DE23}")]
        public bool Facet { get; set; }

        /// <summary>
        /// Gets/sets the facet field.
        /// </summary>
        [SitecoreField("facetField", "{00388865-8F56-45F1-8517-B46AAD6C714C}")]
        public string FacetField { get; set; }

        /// <summary>
        /// Gets/sets the facet field.
        /// </summary>
        [SitecoreField("facetQuery", "{83435273-5FFB-4A47-9DD1-5A5B571C774A}")]
        public string FacetQuery { get; set; }

        /// <summary>
        /// Gets/sets the facet prefix.
        /// </summary>
        [SitecoreField("facetPrefix", "{5CC9E9FE-4C3E-4E2C-BC93-FA0E4A437641}")]
        public string FacetPrefix { get; set; }

        /// <summary>
        /// Gets/sets if spellcheck is enabled.
        /// </summary>
        [SitecoreField("spellcheck", "{319A1FD7-2085-4163-B534-76ADF5FE2999}")]
        public bool Spellcheck { get; set; }

        /// <summary>
        /// Gets/sets the name of the spellcheck dictionary to use.
        /// </summary>
        [SitecoreField("spellcheckDictionary", "{BECF2713-6021-47E8-93F1-E76100FC1990}")]
        public string SpellcheckDictionary { get; set; }

        /// <summary>
        /// Gets/sets the spellcheck count.
        /// </summary>
        [SitecoreField("spellcheckCount", "{27A0A235-98C5-4EAF-8548-72753E7421F2}")]
        public int SpellcheckCount { get; set; }

        /// <summary>
        /// Gets/sets the spellcheck flag to only return more popular results.
        /// </summary>
        [SitecoreField("spellcheckOnlyMorePopular", "{8B2E6B25-A2B5-4A39-9CCC-EEA111A3877E}")]
        public bool SpellcheckOnlyMorePopular { get; set; }

        /// <summary>
        /// Gets/sets the spellcheck extended results flag.
        /// </summary>
        [SitecoreField("spellcheckExtendedResults", "{C946CA8F-9374-4905-88CD-FF6C721509C8}")]
        public bool SpellcheckExtendedResults { get; set; }

        /// <summary>
        /// Gets/sets the spellcheck collate.
        /// </summary>
        [SitecoreField("spellcheckCollate", "{6FF32C99-E3DD-406C-B74A-CD9C50998031}")]
        public bool SpellcheckCollate { get; set; }

        /// <summary>
        /// Gets/sets the spellcheck max collations.
        /// </summary>
        [SitecoreField("spellcheckMaxCollations", "{E499E8F2-1D27-4A76-A77B-64D5194785C1}")]
        public int SpellcheckMaxCollations { get; set; }

        /// <summary>
        /// Gets/sets the spellcheck max collations.
        /// </summary>
        [SitecoreField("spellcheckMaxCollationTries", "{44966AA3-6853-4A7C-A04C-9D18447C4A4D}")]
        public int SpellcheckMaxCollationTries { get; set; }

        /// <summary>
        /// Gets / Sets spellcheck collate extended results.
        /// </summary>
        [SitecoreField("spellcheckCollateExtendedResults", "{DCCBAE15-92CC-4C6A-B28F-FC9F57456103}")]
        public bool SpellcheckCollateExtendedResults { get; set; }

        /// <summary>
        /// Gets/sets the spellcheck accuracy.
        /// </summary>
        [SitecoreField("spellcheckAccuracy", "{A01D2478-7C1F-4639-880B-A640017341D9}")]
        public float SpellcheckAccuracy { get; set; }

        /// <summary>
        /// Gets/sets if highlighting is enabled.
        /// </summary>
        [SitecoreField("highlight", "{71160187-9048-438B-8B96-E12D6C0C3A8F}")]
        public bool Highlight { get; set; }

        /// <summary>
        /// The highlighting implementation to use. Acceptable values are: unified, original, fastVector, and postings. If blank, "original" wiis the default in solr.
        /// </summary>
        [SitecoreField("highlightMethod", "{356BECB3-37DF-47B8-9E08-EC530064A126}")]
        public string HighlightMethod { get; set; }

        /// <summary>
        /// Specifies the “tag” to use before a highlighted term. This can be any string, but is most often an HTML or XML tag.
        /// </summary>
        [SitecoreField("highlightPre", "{F9B1A948-C358-405B-B08F-732D507913E3}")]
        public string HighlightPre { get; set; }

        /// <summary>
        /// Specifies the “tag” to use after a highlighted term. This can be any string, but is most often an HTML or XML tag.
        /// </summary>
        [SitecoreField("highlightPost", "{88A5752E-2F01-43DE-A0A3-97545F368043}")]
        public string HighlightPost { get; set; }

        /// <summary>
        /// Gets/sets the highlight requireFieldMatch. 
        /// By default, false, all query terms will be highlighted for each field to be highlighted (hl.fl) no matter what fields the parsed query refer to.
        /// If set to true, only query terms aligning with the field being highlighted will in turn be highlighted.
        /// </summary>
        [SitecoreField("highlightRequireFieldMatch", "{B4374BD4-9128-4444-BA5B-5DF0636BC6FA}")]
        public bool HighlightRequireFieldMatch { get; set; }

        /// <summary>
        /// If set to true, Solr will highlight phrase queries (and other advanced position-sensitive queries) accurately – as phrases.
        /// If false, the parts of the phrase will be highlighted everywhere instead of only when it forms the given phrase.
        /// </summary>
        [SitecoreField("highlightUsePhraseHighlighter", "{47A9FE3B-F28E-41CD-B774-7764397B55A6}")]
        public bool HighlightUsePhaseHighlighter { get; set; }

        /// <summary>
        /// Specifies the approximate size, in characters, of fragments to consider for highlighting. 
        /// 0 indicates that no fragmenting should be considered and the whole field value should be used.
        /// </summary>
        [SitecoreField("highlightFragSize", "{C05C8119-4800-42F3-A2B0-BD370F77EA28}")]
        public int HighlightFragSize { get; set; }

        /// <summary>
        /// Specifies a list of fields to highlight. If left blank, the "qf" field is used automatically by solr.
        /// Accepts a comma- or space-delimited list of fields for which Solr should generate highlighted snippets.
        /// </summary>
        [SitecoreField("highlightFields", "{FA61BF4E-7136-49A9-926C-11B2B773979C}")]
        public string HighlightFields { get; set; }

        /// <summary>
        /// Specifies maximum number of highlighted snippets to generate per field.
        /// It is possible for any number of snippets from zero to this value to be generated.
        /// If left blank, Solr default of 1 will be used.
        /// </summary>
        [SitecoreField("highlightSnippets", "{E0B438E4-17A3-4B4F-9185-84700F642F47}")]
        public int HighlightSnippets { get; set; }
    }
    [DebuggerDisplay("FieldName = {FieldName}, BoostValue = {BoostValue}")]
    public class Field
    {
        public string FieldName { get; set; }

        public float BoostValue { get; set; }
    }
}