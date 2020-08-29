using LienPhatERP.Entities;
using LienPhatERP.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class EProductModel
    {
        public EProductModel()
        {
            EContactForm = new HashSet<EContactForm>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public bool IsPublished { get; set; }
        public DateTimeOffset? PublishedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedById { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string UpdatedById { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? SpecialPrice { get; set; }

        public DateTimeOffset? SpecialPriceStart
        {
            get
            {
                if (!string.IsNullOrEmpty(StrSpecialPriceStart))
                {
                    return DateTimeOffset.ParseExact(StrSpecialPriceStart, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                return null;
            } set { }
        }

        public DateTimeOffset? SpecialPriceEnd
        {
            get
            {
                if (!string.IsNullOrEmpty(StrSpecialPriceStart))
                {
                    return DateTimeOffset.ParseExact(StrSpecialPriceEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture); 
                }
                return null;
                
            } set { }
        }
        public bool HasOptions { get; set; }
        public bool IsVisibleIndividually { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsCallForPricing { get; set; }
        public bool IsAllowToOrder { get; set; }
        public int StockQuantity { get; set; }
        public string Sku { get; set; }
        public string Gtin { get; set; }
        public string NormalizedName { get; set; }
        public int DisplayOrder { get; set; }
        public long? VendorId { get; set; }
        public string ThumbnailImage { get; set; }
        public int ReviewsCount { get; set; }
        public double? RatingAverage { get; set; }
        public long? BrandId { get; set; }
        public long? TaxClassId { get; set; }      
        public string StrSpecialPriceStart { get; set; }
        public string StrSpecialPriceEnd { get; set; }

        public ICollection<EContactForm> EContactForm { get; set; }

        public void Update(EProduct rs)
        {
            rs.Name = Name;         
            rs.Sku = Sku;
            rs.MetaTitle = MetaTitle;
            rs.MetaDescription =MetaDescription;
            rs.MetaKeywords = MetaKeywords;
            if (!string.IsNullOrEmpty(rs.Name))
            {
                rs.Slug = StringUtils.CreateUrl(rs.Name);
            }
            rs.Description = Description;
            rs.Price = Price;
            rs.OldPrice = OldPrice;
            rs.SpecialPrice = SpecialPrice;
            rs.SpecialPriceStart =SpecialPriceStart;
            rs.SpecialPriceEnd = SpecialPriceEnd;
            rs.IsPublished = IsPublished;
            if (!string.IsNullOrEmpty(ThumbnailImage))
            {
                rs.ThumbnailImage= ThumbnailImage;
            }
        }
    }
}
