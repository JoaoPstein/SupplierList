using FluentAssertions;
using Supplier.Application.DTOs.Companys;
using Supplier.Domain.Enum;
using Supplier.Tests.Integration.Utils;
using Supplier.Web;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Supplier.Integration.Test.Integration.CompanyTests
{
    [Collection("Non-Parallel")]
    public class CompanyTests : IntegrationTestBase
    {
        private CompanySetup _setup;
        string endpointController = "Company";

        public CompanyTests(CustomWebAppFactory<Startup> appFactory) : base(appFactory)
        {
            _setup = CompanySetup.GetSetup(_context);
        }

        [Fact]
        public async Task Should_Create_Company()
        {
            string suffix = "scc";

            var company = CreateCompanyRequest(suffix, "12345678912345", UfEnum.AL);

            var response = await CreateEntity(company, endpointController);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private CompanyRequestDto CreateCompanyRequest(string suffix, string cnpj, UfEnum uf)
        {
            return new CompanyRequestDto()
            {
                Active = true,
                FantasyName = $"Company{suffix}",
                Cnpj = cnpj,
                UF = uf
            };
        }
    }
}
