using System;

#nullable enable
namespace DutchAndBold.MoneybirdCli.Configurations
{
    public class MoneybirdApiConfiguration
    {
        public string? ClientId { get; set; } = null;

        public string? ClientSecret { get; set; } = null;
        
        public Uri EndpointUrl { get; set; } = new Uri("https://moneybird.com/api/");

        public Uri AuthorityUrl { get; set; } = new Uri("https://moneybird.com/oauth/");
    }
}