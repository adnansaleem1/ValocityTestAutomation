using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityTestCases.Entity
{
    class JsonDesealized
    {
    }
    public class ProductSubmitError
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> AdditionalInfo { get; set; }
    }
    class ApiLoginResponse
    {

        public string AccessToken { get; set; }
        public DateTime TokenExpirationTime { get; set; }
    }

    public class Inventory
    {
        public string InventoryLink { get; set; }
        public string InventoryStatus { get; set; }
        public string InventoryQuantity { get; set; }
    }

    public class AvailableVariation
    {
        public List<string> ParentValue { get; set; }
        public List<string> ChildValue { get; set; }
    }

    public class Availability
    {
        public string ParentCriteria { get; set; }
        public string ChildCriteria { get; set; }
        public List<AvailableVariation> AvailableVariations { get; set; }
    }

    public class Image
    {
        public string ImageURL { get; set; }
        public int Rank { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class PriceUnit
    {
        public string ItemsPerUnit { get; set; }
    }

    public class Price
    {
        public int Sequence { get; set; }
        public int Qty { get; set; }
        [JsonProperty("Price")]
        public string Price1 { get; set; }
        public string DiscountCode { get; set; }
        public PriceUnit PriceUnit { get; set; }
    }

    public class PriceConfiguration
    {
        public string Criteria { get; set; }
        public List<string> Value { get; set; }
    }

    public class PriceGrid
    {
        public bool IsBasePrice { get; set; }
        public bool IsQUR { get; set; }
        public string Description { get; set; }
        public string PriceIncludes { get; set; }
        public int Sequence { get; set; }
        public string Currency { get; set; }
        public List<Price> Prices { get; set; }
        public List<PriceConfiguration> PriceConfigurations { get; set; }
        public string UpchargeType { get; set; }
        public string UpchargeUsageType { get; set; }
    }

    public class Inventory2
    {
        public string InventoryLink { get; set; }
        public string InventoryStatus { get; set; }
        public string InventoryQuantity { get; set; }
    }

    public class Configuration
    {
        public string Criteria { get; set; }
        public List<string> Value { get; set; }
    }

    public class ProductSKU
    {
        public string SKU { get; set; }
        public Inventory2 Inventory { get; set; }
        public List<Configuration> Configurations { get; set; }
    }

    public class Value
    {
        public string Name { get; set; }
    }

    public class ImprintColors
    {
        public string Type { get; set; }
        public List<Value> Values { get; set; }
    }

    public class Samples
    {
        public bool SpecSampleAvailable { get; set; }
        public string SpecInfo { get; set; }
        public bool ProductSampleAvailable { get; set; }
        public string ProductSampleInfo { get; set; }
    }

    public class Color
    {
        public string Name { get; set; }
        public string Alias { get; set; }
    }

    public class Material
    {
        public string Name { get; set; }
        public string Alias { get; set; }
    }

    public class Value3
    {
        public string Attribute { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
    }

    public class Value2
    {
        public List<Value3> Value { get; set; }
    }

    public class Dimension
    {
        public List<Value2> Values { get; set; }
    }

    public class Sizes
    {
        public Dimension Dimension { get; set; }
    }

    public class Shape
    {
        public string Name { get; set; }
    }

    public class Artwork
    {
        public string Value { get; set; }
        public string Comments { get; set; }
    }

    public class Personalization
    {
        public string Type { get; set; }
        public string Alias { get; set; }
    }

    public class ProductionTime
    {
        public string BusinessDays { get; set; }
        public string Details { get; set; }
    }

    public class SameDayRush
    {
        public bool Available { get; set; }
        public string Details { get; set; }
    }

    public class Value4
    {
        public string BusinessDays { get; set; }
        public string Details { get; set; }
    }

    public class RushTime
    {
        public bool Available { get; set; }
        public List<Value4> Values { get; set; }
    }

    public class ImprintSizeLocation
    {
        public string Size { get; set; }
        public string Location { get; set; }
    }

    public class ProductConfigurations
    {
        public ImprintColors ImprintColors { get; set; }
        public Samples Samples { get; set; }
        public List<Color> Colors { get; set; }
        public List<Material> Materials { get; set; }
        public Sizes Sizes { get; set; }
        public List<Shape> Shapes { get; set; }
        public List<string> Themes { get; set; }
        public List<string> Origins { get; set; }
        public List<string> TradeNames { get; set; }
        public List<Artwork> Artwork { get; set; }
        public List<Personalization> Personalization { get; set; }
        public List<ProductionTime> ProductionTime { get; set; }
        public SameDayRush SameDayRush { get; set; }
        public RushTime RushTime { get; set; }
        public List<string> AdditionalColors { get; set; }
        public List<string> AdditionalLocations { get; set; }
        public List<ImprintSizeLocation> ImprintSizeLocations { get; set; }
    }

    public class ProductObject
    {
        public string ExternalProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string AsiProdNo { get; set; }
        public string SKU { get; set; }
        public Inventory Inventory { get; set; }
        public bool BreakoutByPrice { get; set; }
        public bool CanShipInPlainBox { get; set; }
        public bool SEOFlag { get; set; }
        public bool DistributorViewOnly { get; set; }
        public List<string> LineNames { get; set; }
        public string DistributorOnlyComments { get; set; }
        public string ProductDisclaimer { get; set; }
        public string AdditionalProductInfo { get; set; }
        public string PriceConfirmedThru { get; set; }
        public bool CanOrderLessThanMinimum { get; set; }
        public List<Availability> Availability { get; set; }
        public List<string> FOBPoints { get; set; }
        public List<string> ProductKeywords { get; set; }
        public List<string> Categories { get; set; }
        public List<string> ComplianceCerts { get; set; }
        public List<Image> Images { get; set; }
        public List<PriceGrid> PriceGrids { get; set; }
        public List<ProductSKU> ProductSKUs { get; set; }
        public string PriceType { get; set; }
        public ProductConfigurations ProductConfigurations { get; set; }
    }
}
