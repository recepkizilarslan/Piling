using System.Reflection;
using Piling.Helper;
using Piling.Model;
using Xunit;

namespace Pilling.Test.Helper
{
    public class ParameterValidatorFixture
    {
        private readonly ParameterValidator _validator = new ParameterValidator();

        private  string mainPath= Assembly.GetExecutingAssembly().Location;
       
        
        [Fact]
        public void Validate_to_valid_ip_address()
        {
            var validIp = _validator.IsValidAddress("192.168.2.1");
            Assert.True(validIp == AddressType.Ip);
        }

        [Fact]
        public void Validate_to_valid_domain()
        {
            var validIp = _validator.IsValidAddress("google.com");
            Assert.True(validIp == AddressType.Domain);
        }

        [Fact]
        public void Validate_to_unvalid_domain()
        {
            var validIp = _validator.IsValidAddress("google");
            Assert.True(validIp == AddressType.Domain);
        }

        [Fact]
        public void Validate_to_unValid_ip_address()
        {
            var validIp = _validator.IsValidAddress("1.1.1.1234.32423.23432");
            Assert.True(validIp == AddressType.Undefined);
        }

        [Fact]
        public void Validate_to_null_path()
        {
            var validIp = _validator.IsValidType("");
            Assert.Equal(OutputFormat.Undefined, validIp);
        }

        [Fact]
        public void Validate_to_null_fileName()
        {
            var nullFileName = _validator.IsValidType(@$"{mainPath}\");
            Assert.Equal(OutputFormat.Undefined, nullFileName);
        }

        [Fact]
        public void Validate_to_unsupported_extension_1()
        {
            var nullFileName = _validator.IsValidType(@$"{mainPath}\result.pdf");
            Assert.Equal(OutputFormat.Undefined, nullFileName);
        }


        [Fact]
        public void Validate_to_supported_extension_txt()
        {
            var nullFileName = _validator.IsValidType(@$"{mainPath}\result.txt");
            Assert.Equal(OutputFormat.Txt, nullFileName);
        }

        [Fact]
        public void Validate_to_supported_extension_csv()
        {
            var nullFileName = _validator.IsValidType(@$"{mainPath}\result.csv");
            Assert.Equal(OutputFormat.Csv, nullFileName);
        }
    }
}
