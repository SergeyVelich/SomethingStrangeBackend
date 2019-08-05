using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Mongo.Entities
{
    [ExcludeFromCodeCoverage]
    [ModelMetadataType(typeof(PostMetadata))]
    public partial class Post
    {
        internal sealed class PostMetadata
        {
            [BsonDateTimeOptions]
            public DateTime Date { get; set; }
        }
    }
}
