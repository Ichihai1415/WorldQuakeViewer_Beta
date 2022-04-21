using System;
using System.Collections.Generic;

namespace USGSQuakeClass
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Metadata
    {
        public long Generated { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Api { get; set; }
        public int Count { get; set; }
    }

    public class Properties
    {
        public double Mag { get; set; }
        public string Place { get; set; }
        public object Time { get; set; }
        public object Updated { get; set; }
        public object Tz { get; set; }
        public string Url { get; set; }
        public string Detail { get; set; }
        public int? Felt { get; set; }
        public double? Cdi { get; set; }
        public double? Mmi { get; set; }
        public string Alert { get; set; }
        public string Status { get; set; }
        public int Tsunami { get; set; }
        public int Sig { get; set; }
        public string Net { get; set; }
        public string Code { get; set; }
        public string Ids { get; set; }
        public string Sources { get; set; }
        public string Types { get; set; }
        public object Nst { get; set; }
        public double Dmin { get; set; }
        public double Rms { get; set; }
        public int Gap { get; set; }
        public string MagType { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
    }
    public class Geometry
    {
        public string Type { get; set; }
        public List<double> Coordinates { get; set; }
    }
    public class Feature
    {
        public string Type { get; set; }
        public Properties Properties { get; set; }
        public Geometry Geometry { get; set; }
        public string Id { get; set; }
    }
    public class USGSQuake
    {
        public string Type { get; set; }
        public Metadata Metadata { get; set; }
        public List<Feature> Features { get; set; }
        public List<double> Bbox { get; set; }
    }
}
namespace USGSFERegionsClass
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Metadata
    {
        public string Request { get; set; }
        public DateTime Submitted { get; set; }
        public List<string> Types { get; set; }
        public string Version { get; set; }
    }

    public class Properties
    {
        public int Number { get; set; }
        public string Name { get; set; }
    }

    public class Feature
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public object Geometry { get; set; }
        public Properties Properties { get; set; }
    }

    public class Fe
    {
        public string Type { get; set; }
        public int Count { get; set; }
        public List<Feature> Features { get; set; }
    }

    public class USGSFERegions
    {
        public Metadata Metadata { get; set; }
        public Fe Fe { get; set; }
    }
}
namespace USGSFERegionsClass2
{
    public class Properties
    {
        public string Name { get; set; }
    }
    public class Feature
    {
        public Properties Properties { get; set; }
    }
    public class Fe
    {
        public List<Feature> Features { get; set; }
    }
    public class USGSFERegions2
    {
        public Fe Fe { get; set; }
    }

}
namespace WorldQuakeViewer
{
    public class Tokens_JSON
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessSecret { get; set; }
    }
}