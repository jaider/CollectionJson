using System;

//http://amundsen.com/media-types/collection/format/
namespace CollectionJson
{
    public class CjCollection
    {
        [Required(AssumedValue = "1.0")]
        public string Version { get; set; }

        /// <summary>
        /// This URI SHOULD represent the address used to retrieve a representation of the document. This URI MAY be used to add a new record 
        /// </summary>
        [Required]
        public Uri Href { get; set; }

        /// <summary>
        /// The items array represents the list of records in the Collection+JSON document.
        /// </summary>
        [Optional]
        public RecordInfo[] Items { get; set; }

        /// <summary>
        /// The target IRI points to a resource that is a representation of a list of valid queries that can be executed against the data represented by the context IRI.
        /// </summary>
        [Optional]
        public QueryTemplateInfo[] Queries { get; set; }

        /// <summary>
        /// The template object contains all the input elements used to add or edit collection "records." 
        /// </summary>
        [Optional]
        public TemplateInfo Template { get; set; }

        /// <summary>
        /// The error object contains addiitional information on the latest error condition reported by the server. 
        /// </summary>
        [Optional]
        public ErrorInfo Error { get; set; }
    }

    public class RecordInfo
    {
        [Required]
        public Uri Href { get; set; }

        [Optional]
        public DataInfo[] Data { get; set; }

        [Optional]
        public LinkInfo[] Links { get; set; }
    }

    public class LinkInfo
    {
        [Required]
        public Uri Href { get; set; }

        [Required]
        public string Rel { get; set; }

        [Optional]
        public string Prompt { get; set; }

        [Optional(DefaultValue = "link")]
        public string Render { get; set; }
    }

    public class ErrorInfo
    {
        public string Title { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }

    public class TemplateInfo
    {
        public DataInfo[] Data { get; set; }
    }

    public class DataInfo
    {
        [Required]
        public string Prompt { get; set; }

        [Optional]
        public string Name { get; set; }

        [Optional]
        public object Value { get; set; }
    }

    public class QueryTemplateInfo
    {
        [Required]
        public Uri Href { get; set; }

        [Required]
        public string Rel { get; set; }

        [Optional]
        public string Prompt { get; set; }

        [Optional]
        public string Name { get; set; }

        [Optional]
        public QueryDataInfo[] Data { get; set; }
    }

    public class QueryDataInfo
    {
        public string Name { get; set; }

        public object Value { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class OptionalAttribute: Attribute
    {
        public string DefaultValue { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class RequiredAttribute : Attribute
    {
        public string AssumedValue { get; set; }
    }
}
