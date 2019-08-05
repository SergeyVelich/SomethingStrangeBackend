using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class PostViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        [BindRequired]
        public string Title { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Display(Name = "Language ID")]
        public int LanguageId { get; set; }
        [Display(Name = "Language")]
        public string LanguageName { get; set; }
        [Display(Name = "Description")]
        public int AuthorId { get; set; }
        [Display(Name = "Language")]
        public string AuthorName { get; set; }
        [Display(Name = "Description")]
        public string PreviewText { get; set; }
        public string FullText { get; set; }
    }
}
